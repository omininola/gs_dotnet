using System.ComponentModel.DataAnnotations;

namespace NaturalDisasterAPI.DTO.CidadeDTO;

public class CidadeRequest
{
    [Required(ErrorMessage = "Id do estado é obrigatório")]
    public long EstadoId { get; set; }
    
    [Required(ErrorMessage = "Nome da cidade é obrigatório")]
    [StringLength(50, ErrorMessage = "Nome da cidade deve ter no máximo 50 caracteres")]
    public string Nome { get; set; } = string.Empty;
}