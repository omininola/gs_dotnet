using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NaturalDisasterAPI.Data;
using NaturalDisasterAPI.DTO;
using NaturalDisasterAPI.Models;

namespace NaturalDisasterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DroneController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public DroneController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public async Task<ActionResult<DroneResponse>> PostDrone([FromBody] DroneRequest droneRequest)
        {
            var cidade = await _context.Cidades.FindAsync(droneRequest.CidadeId);
            if (cidade == null)
            {
                return BadRequest("Cidade não encontrada");
            }

            var drone = new Drone
            {
                Modelo = droneRequest.Modelo,
                Status = droneRequest.Status,
                Cidade = cidade
            };

            _context.Drones.Add(drone);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDrone), new { id = drone.Id }, drone);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DroneResponse>>> GetDrones()
        {
            var drones = await _context.Drones
                .Include(d => d.Cidade)
                .Include(d => d.Relatorios)
                .Select(d => new DroneResponse
                {
                    Id = d.Id,
                    Modelo = d.Modelo,
                    Status = d.Status,
                    CidadeNome = d.Cidade.Nome,
                    Relatorios = d.Relatorios.Select(r => new RelatorioResponse
                    {
                        Id = r.Id,
                        Descricao = r.Descricao,
                        Data = r.Data
                    }).ToList()
                })
                .ToListAsync();

            return Ok(drones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DroneResponse>> GetDrone(long id)
        {
            var drone = await _context.Drones
                .Include(d => d.Cidade)
                .Include(d => d.Relatorios)
                .Where(d => d.Id == id)
                .Select(d => new DroneResponse
                {
                    Id = d.Id,
                    Modelo = d.Modelo,
                    Status = d.Status,
                    CidadeNome = d.Cidade.Nome,
                    Relatorios = d.Relatorios.Select(r => new RelatorioResponse
                    {
                        Id = r.Id,
                        Descricao = r.Descricao,
                        Data = r.Data
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (drone == null)
            {
                return NotFound("Drone não encontrado");
            }

            return Ok(drone);
        }
    }
}
