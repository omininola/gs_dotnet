using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NaturalDisasterAPI.Data;
using NaturalDisasterAPI.DTO;
using NaturalDisasterAPI.Models;

namespace NaturalDisasterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LocalController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("pais")]
        public async Task<ActionResult<PaisResponse>> PostPais(PaisRequest paisRequest)
        {
            var pais = new Pais()
            {
                Nome = paisRequest.Nome
            };
            
            _context.Paises.Add(pais);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPais), new { id = pais.Id }, pais);
        }
        
        [HttpGet("pais")]
        public async Task<ActionResult<IEnumerable<PaisResponse>>> GetPaises()
        {
            var paises = await _context.Paises
                .Include(p => p.Estados)
                .Select(p => new PaisResponse
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Estados = p.Estados
                })
                .ToListAsync();

            return Ok(paises);
        }

        [HttpGet("pais/{id}")]
        public async Task<ActionResult<PaisResponse>> GetPais(long id)
        {
            var pais = await _context.Paises
                .Include(p => p.Estados)
                .Where(p => p.Id == id)
                .Select(p => new PaisResponse
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Estados = p.Estados
                })
                .FirstOrDefaultAsync();

            if (pais == null)
            {
                return NotFound("País não encontrado");
            }
            
            return Ok(pais);
        }
        
        [HttpPost("estado")]
        public async Task<ActionResult<EstadoResponse>> Estado(EstadoRequest estadoRequest)
        {
            var pais = await _context.Paises.FindAsync(estadoRequest.PaisId);
            if (pais == null)
            {
                return BadRequest("País não encontrado");
            }
            
            var estado = new Estado()
            {
                Nome = estadoRequest.Nome,
                Pais = pais
            };
            
            _context.Estados.Add(estado);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEstado), new { id = estado.Id }, estado);
        }
        
        [HttpGet("estado")]
        public async Task<ActionResult<IEnumerable<EstadoResponse>>> GetEstados()
        {
            var estadoes = await _context.Estados
                .Include(e => e.Pais)
                .Include(e => e.Cidades)
                .Select(e => new EstadoResponse
                {
                    Id = e.Id,
                    Nome = e.Nome,
                    PaisNome = e.Pais.Nome,
                    Cidades = e.Cidades
                })
                .ToListAsync();

            return Ok(estadoes);
        }

        [HttpGet("estado/{id}")]
        public async Task<ActionResult<EstadoResponse>> GetEstado(long id)
        {
            var estado = await _context.Estados
                .Include(e => e.Pais)
                .Include(e => e.Cidades)
                .Where(e => e.Id == id)
                .Select(e => new EstadoResponse
                {
                    Id = e.Id,
                    Nome = e.Nome,
                    PaisNome = e.Pais.Nome,
                    Cidades = e.Cidades
                })
                .FirstOrDefaultAsync();

            if (estado == null)
            {
                return NotFound("Estado não encontrado");
            }
            
            return Ok(estado);
        }

        [HttpPost("cidade")]
        public async Task<ActionResult<CidadeResponse>> Cidade(CidadeRequest cidadeRequest)
        {
            var estado = await _context.Estados.FindAsync(cidadeRequest.EstadoId);
            if (estado == null)
            {
                return BadRequest("Estado não encontrado");
            }
            
            var cidade = new Cidade()
            {
                Nome = cidadeRequest.Nome,
                Estado = estado
            };
            
            _context.Cidades.Add(cidade);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetCidade), new { id = cidade.Id }, cidade);
        }
        
        [HttpGet("cidade")]
        public async Task<ActionResult<IEnumerable<CidadeResponse>>> GetCidades()
        {
            var cidades = await _context.Cidades
                .Include(c => c.Estado)
                .Include(c => c.Drones)
                .Include(c => c.Usuarios)
                .Select(c => new CidadeResponse()
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Estado = c.Estado.Nome,
                    Drones = c.Drones.Select(d => new DroneResponse
                    {
                        Id = d.Id,
                        CidadeNome = d.Cidade.Nome,
                        Modelo = d.Modelo,
                        Status = d.Status,
                    }).ToList(),
                    Usuarios = c.Usuarios.Select(u => new UsuarioResponse
                    {
                        Id = u.Id,
                        CidadeNome = u.Cidade.Nome,
                        Nome = u.Nome,
                        Email = u.Email,
                    }).ToList()
                })
                .ToListAsync();

            return Ok(cidades);
        }

        [HttpGet("cidade/{id}")]
        public async Task<ActionResult<CidadeResponse>> GetCidade(long id)
        {
            var cidade = await _context.Cidades
                .Include(c => c.Estado)
                .Include(c => c.Drones)
                .Include(c => c.Usuarios)
                .Where(c => c.Id == id)
                .Select(c => new CidadeResponse()
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Estado = c.Estado.Nome,
                    Drones = c.Drones.Select(d => new DroneResponse
                    {
                        Id = d.Id,
                        CidadeNome = d.Cidade.Nome,
                        Modelo = d.Modelo,
                        Status = d.Status,
                    }).ToList(),
                    Usuarios = c.Usuarios.Select(u => new UsuarioResponse
                    {
                        Id = u.Id,
                        CidadeNome = u.Cidade.Nome,
                        Nome = u.Nome,
                        Email = u.Email,
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (cidade == null)
            {
                return NotFound("Cidade não encontrada");
            }

            return Ok(cidade);
        }
    }
}
