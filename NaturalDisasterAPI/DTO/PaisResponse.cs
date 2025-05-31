using NaturalDisasterAPI.Models;

namespace NaturalDisasterAPI.DTO;

public class PaisResponse
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public List<Estado> Estados { get; set; } = new List<Estado>();
}