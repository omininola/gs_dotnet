using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NaturalDisasterAPI.Models;

public class Pais
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public List<Estado> Estados { get; set; } = new List<Estado>();
}