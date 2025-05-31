using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NaturalDisasterMVC.Models;

public class Drone
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public Cidade Cidade { get; set; } = new Cidade();
    public string Modelo { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public List<Relatorio> Relatorios { get; set; } = new List<Relatorio>();
    public List<Sensor> Sensores { get; set; } = new List<Sensor>();
}