using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NaturalDisasterAPI.Data;
using NaturalDisasterAPI.DTO;
using NaturalDisasterAPI.Models;

namespace NaturalDisasterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public RelatorioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("usuario")]
        public async Task<ActionResult<RelatorioResponse>> PostRelatorio([FromBody] RelatorioUsuarioRequest relatorioUsuarioRequest)
        {
            var usuario = await _context.Usuarios.FindAsync(relatorioUsuarioRequest.UsuarioId);
            if (usuario == null)
            {
                return BadRequest("Usuário não encontrado");
            }

            var relatorio = new Relatorio
            {
                Usuario = usuario,
                Descricao = relatorioUsuarioRequest.Descricao,
                Data = DateTime.Now
            };
            
            _context.Relatorios.Add(relatorio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRelatorio), new { Id = relatorio.Id }, relatorio);
        }
        
        [HttpPost("drone")]
        public async Task<ActionResult<RelatorioResponse>> PostRelatorio([FromBody] RelatorioDroneRequest relatorioDroneRequest)
        {
            var drone = await _context.Drones.FindAsync(relatorioDroneRequest.DroneId);
            if (drone == null)
            {
                return BadRequest("Drone não encontrado");
            }

            var relatorio = new Relatorio
            {
                Drone = drone,
                Descricao = relatorioDroneRequest.Descricao,
                Data = DateTime.Now
            };
            
            _context.Relatorios.Add(relatorio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRelatorio), new { Id = relatorio.Id }, relatorio);
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RelatorioResponse>>> GetRelatorios()
        {
            var relatorios = await _context.Relatorios
                .Include(r => r.Drone)
                .Include(r => r.Usuario)
                .Select(u => new RelatorioResponse
                {
                    Id = u.Id,
                    Descricao = u.Descricao,
                    Data = u.Data,
                    DroneModelo = u.Drone.Modelo,
                    UsuarioNome = u.Usuario.Nome
                })
                .ToListAsync();

            return Ok(relatorios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RelatorioResponse>> GetRelatorio(long id)
        {
            var relatorio = await _context.Relatorios
                .Include(r => r.Drone)
                .Include(r => r.Usuario)
                .Where(r => r.Id == id)
                .Select(u => new RelatorioResponse
                {
                    Id = u.Id,
                    Descricao = u.Descricao,
                    Data = u.Data,
                    DroneModelo = u.Drone.Modelo,
                    UsuarioNome = u.Usuario.Nome
                })
                .FirstOrDefaultAsync();

            if (relatorio == null)
            {
                return NotFound("Relatório não encontrado");
            }
            
            return Ok(relatorio);
        }
        
        [HttpGet("cidade/{cidade}")]
        public async Task<ActionResult<IEnumerable<RelatorioResponse>>> GetRelatoriosByCidade(string cidade)
        {
            var relatorios = await _context.Relatorios
                .Include(r => r.Drone)
                .Include(r => r.Usuario)
                .Where(r => r.Usuario.Cidade.Equals(cidade) || r.Drone.Cidade.Equals(cidade))
                .Select(u => new RelatorioResponse
                {
                    Id = u.Id,
                    Descricao = u.Descricao,
                    Data = u.Data,
                    DroneModelo = u.Drone.Modelo,
                    UsuarioNome = u.Usuario.Nome
                })
                .ToListAsync();
            
            return Ok(relatorios);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var relatorio = await _context.Relatorios.FindAsync(id);
            if (relatorio == null)
            {
                return NotFound("Relatório não encontrado");
            }
            
            _context.Relatorios.Remove(relatorio);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}
