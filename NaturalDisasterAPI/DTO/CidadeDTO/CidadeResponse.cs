using NaturalDisasterAPI.Models;

namespace NaturalDisasterAPI.DTO.CidadeDTO;

public class CidadeResponse
{ 
    public long Id { get; set; }
    public string Estado { get; set; }
    public string Nome { get; set; }
    public List<Relatorio> Relatorios { get; set; } = new List<Relatorio>();
}