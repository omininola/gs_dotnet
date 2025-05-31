using System.Text.Json.Serialization;
using NaturalDisasterMVC.Models;

namespace NaturalDisasterMVC.DTO;

public class UsuarioResponse
{
    public long Id { get; set; }
    
    public string CidadeNome { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}