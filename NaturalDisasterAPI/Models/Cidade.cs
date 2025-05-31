using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NaturalDisasterAPI.Models;

public class Cidade
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public Estado Estado { get; set; }
    public string Nome { get; set; }
    public List<Drone> Drones { get; set; }
    public List<Usuario> Usuarios { get; set; }
}