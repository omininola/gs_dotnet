using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NaturalDisasterAPI.Data;
using NaturalDisasterAPI.DTO.RelatorioDTO;
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
        public async Task<ActionResult<RelatorioResponse>> PostRelatorioUsuario([FromBody] RelatorioUsuarioRequest relatorioUsuarioRequest)
        {
            return await PostRelatorio(relatorioUsuarioRequest);
        }
        
        [HttpPost("drone")]
        public async Task<ActionResult<RelatorioResponse>> PostRelatorioDrone([FromBody] RelatorioDroneRequest relatorioDroneRequest)
        {
            return await PostRelatorio(relatorioDroneRequest);
        }
        
        private async Task<ActionResult<RelatorioResponse>> PostRelatorio([FromBody] RelatorioRequest relatorioRequest)
        {
            var cidade = await  _context.Cidades.FindAsync(relatorioRequest.CidadeId);
            if (cidade == null)
            {
                return NotFound("Cidade não encontrada");
            }
            
            var relatorio = new Relatorio
            {
                Cidade = cidade,
                Descricao = relatorioRequest.Descricao,
                Data = DateTime.Now
            };

            if (relatorioRequest.GetType() == typeof(RelatorioUsuarioRequest))
            {
                var relatorioUsuarioRequest = (RelatorioUsuarioRequest)relatorioRequest;
                var usuario = await _context.Usuarios.FindAsync(relatorioUsuarioRequest.UsuarioId);
                if (usuario == null)
                {
                    return BadRequest("Usuário não encontrado");
                }
                
                relatorio.Usuario = usuario;
            }
            else if (relatorioRequest.GetType() == typeof(RelatorioDroneRequest))
            {
                var relatorioDroneRequest = (RelatorioDroneRequest)relatorioRequest;
                var drone = await _context.Drones.FindAsync(relatorioDroneRequest.DroneId);
                if (drone == null)
                {
                    return BadRequest("Drone não encontrado");
                }
                
                relatorio.Drone = drone;
            }
            else
            {
                return BadRequest("Id do usuário e do drone não informados");
            }
            
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
                .Include(r => r.Cidade)
                .Select(u => new RelatorioResponse
                {
                    Id = u.Id,
                    Descricao = u.Descricao,
                    Data = u.Data,
                    DroneModelo = u.Drone.Modelo,
                    UsuarioNome = u.Usuario.Nome,
                    CidadeNome = u.Cidade.Nome,
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
                .Include(r => r.Cidade)
                .Where(r => r.Id == id)
                .Select(u => new RelatorioResponse
                {
                    Id = u.Id,
                    Descricao = u.Descricao,
                    Data = u.Data,
                    DroneModelo = u.Drone.Modelo,
                    UsuarioNome = u.Usuario.Nome,
                    CidadeNome = u.Cidade.Nome,
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
                .Include(r => r.Cidade)
                .Where(r => r.Cidade.Nome.Equals(cidade))
                .Select(u => new RelatorioResponse
                {
                    Id = u.Id,
                    Descricao = u.Descricao,
                    Data = u.Data,
                    DroneModelo = u.Drone.Modelo,
                    UsuarioNome = u.Usuario.Nome,
                    CidadeNome = u.Cidade.Nome,
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
