using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NaturalDisasterAPI.Data;
using NaturalDisasterAPI.DTO;
using NaturalDisasterAPI.DTO.SensorDTO;
using NaturalDisasterAPI.Models;

namespace NaturalDisasterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public SensorController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public async Task<ActionResult<SensorResponse>> PostSensor([FromBody] SensorRequest sensorRequest)
        {
            var drone = await _context.Drones.FindAsync(sensorRequest.DroneId);
            if (drone == null)
            {
                return BadRequest("Drone não encontrado");
            }
            
            var sensor = new Sensor
            {
                Tipo = sensorRequest.Tipo,
                Status = sensorRequest.Status,
                Descricao = sensorRequest.Descricao,
                Drone = drone
            };

            _context.Sensores.Add(sensor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSensor), new { Id = sensor.Id }, sensor);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorResponse>>> GetSensores()
        {
            var sensors = await _context.Sensores
                .Include(s => s.Drone)
                .Select(s => new SensorResponse
                {
                    Id = s.Id,
                    Tipo = s.Tipo,
                    Status = s.Status,
                    Descricao = s.Descricao
                })
                .ToListAsync();

            return Ok(sensors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SensorResponse>> GetSensor(long id)
        {
            var sensor = await _context.Sensores
                .Include(s => s.Drone)
                .Where(s => s.Id == id)
                .Select(s => new SensorResponse
                {
                    Id = s.Id,
                    Tipo = s.Tipo,
                    Status = s.Status,
                    Descricao = s.Descricao
                })
                .FirstOrDefaultAsync();

            if (sensor == null)
            {
                return NotFound("Sensor não encontrado");
            }
            
            return Ok(sensor);
        }
    }
}
