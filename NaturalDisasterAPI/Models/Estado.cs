using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NaturalDisasterAPI.Models;

public class Estado
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public Pais Pais { get; set; }
    public string Nome { get; set; } = string.Empty;
    public List<Cidade> Cidades { get; set; } = new List<Cidade>();
}