using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NaturalDisasterMVC.Models;

public class Cidade
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public Estado Estado { get; set; }
    public string Nome { get; set; }
    public List<Relatorio> Relatorios { get; set; } = new List<Relatorio>();
}