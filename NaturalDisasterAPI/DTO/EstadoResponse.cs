using NaturalDisasterAPI.Models;

namespace NaturalDisasterAPI.DTO;

public class EstadoResponse
{
    public long Id { get; set; }
    public string PaisNome { get; set; }
    public string Nome { get; set; } = string.Empty;
    public List<Cidade> Cidades { get; set; } = new List<Cidade>();
}