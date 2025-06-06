using NaturalDisasterAPI.DTO.RelatorioDTO;
using NaturalDisasterAPI.DTO.SensorDTO;

namespace NaturalDisasterAPI.DTO.DroneDTO;

public class DroneResponse
{
    public long Id { get; set; }
    public string Modelo { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public List<RelatorioResponse> Relatorios { get; set; } = new List<RelatorioResponse>();
    public List<SensorResponse> Sensores { get; set; } = new List<SensorResponse>();
}