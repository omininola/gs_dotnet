using System.Text.Json.Serialization;
using NaturalDisasterAPI.Models;

namespace NaturalDisasterAPI.DTO;

public class RelatorioResponse
{
    public long Id { get; set; }
    
    public string DroneModelo { get; set; }
    
    public string UsuarioNome { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public DateTime Data { get; set; }
}