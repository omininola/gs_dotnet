using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NaturalDisasterAPI.Models;

public class Sensor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [JsonIgnore]
    public Drone Drone { get; set; } = new Drone();
    
    public string Tipo { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? Descricao { get; set; } = string.Empty;
}