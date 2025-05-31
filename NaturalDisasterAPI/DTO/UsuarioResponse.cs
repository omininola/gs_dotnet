using System.Text.Json.Serialization;
using NaturalDisasterAPI.Models;

namespace NaturalDisasterAPI.DTO;

public class UsuarioResponse
{
    public long Id { get; set; }
    
    public string CidadeNome { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<RelatorioResponse> Relatorios { get; set; } = new List<RelatorioResponse>();
}