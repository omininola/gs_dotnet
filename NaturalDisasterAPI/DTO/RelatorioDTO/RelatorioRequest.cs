using System.ComponentModel.DataAnnotations;

namespace NaturalDisasterAPI.DTO.RelatorioDTO;

public class RelatorioRequest
{
    [Required(ErrorMessage = "Id da cidade é obrigatório")]
    public long CidadeId { get; set; }
    
    [Required(ErrorMessage = "Descrição do relatório é obrigatória")]
    public string Descricao { get; set; } = string.Empty;
}

public class RelatorioUsuarioRequest : RelatorioRequest
{
    [Required(ErrorMessage = "Id do usuário é obrigatório")]
    public long UsuarioId { get; set; }
}

public class RelatorioDroneRequest : RelatorioRequest
{
    [Required(ErrorMessage = "Id do drone é obrigatório")]
    public long DroneId { get; set; }
}
