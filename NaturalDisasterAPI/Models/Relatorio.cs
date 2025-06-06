using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NaturalDisasterAPI.Models;

public class Relatorio
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [JsonIgnore]
    public Cidade Cidade { get; set; }
    
    [JsonIgnore]
    public Drone? Drone { get; set; }
    
    [JsonIgnore]
    public Usuario? Usuario { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public DateTime Data { get; set; }
}