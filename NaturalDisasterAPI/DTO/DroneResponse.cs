using System.Text.Json.Serialization;
using NaturalDisasterAPI.Models;

namespace NaturalDisasterAPI.DTO;

public class DroneResponse
{
    public long Id { get; set; }
    public string CidadeNome { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public List<RelatorioResponse> Relatorios { get; set; } = new List<RelatorioResponse>();
}