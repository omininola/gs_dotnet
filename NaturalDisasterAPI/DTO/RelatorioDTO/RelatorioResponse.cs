namespace NaturalDisasterAPI.DTO.RelatorioDTO;

public class RelatorioResponse
{
    public long Id { get; set; }
    public String CidadeNome { get; set; } = string.Empty;
    public string DroneModelo { get; set; }
    public string UsuarioNome { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public DateTime Data { get; set; }
}