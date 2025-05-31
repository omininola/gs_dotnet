using System.Text.Json.Serialization;
using NaturalDisasterAPI.Models;

namespace NaturalDisasterAPI.DTO;

public class SensorResponse
{
    public long Id { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    
    [JsonIgnore]
    public Drone Drone { get; set; } = new Drone();
}