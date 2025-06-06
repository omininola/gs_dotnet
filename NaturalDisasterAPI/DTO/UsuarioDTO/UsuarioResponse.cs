using NaturalDisasterAPI.DTO.RelatorioDTO;

namespace NaturalDisasterAPI.DTO.UsuarioDTO;

public class UsuarioResponse
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<RelatorioResponse> Relatorios { get; set; } = new List<RelatorioResponse>();
}