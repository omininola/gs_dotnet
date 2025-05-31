using System.ComponentModel.DataAnnotations;

namespace NaturalDisasterAPI.DTO;

public class EstadoRequest
{
    [Required(ErrorMessage = "Id do país é obrigatório")]
    public long PaisId { get; set; }
    
    [Required(ErrorMessage = "Nome do estado é obrigatório")]
    [StringLength(50, ErrorMessage = "Nome do estado deve ter no máximo 50 caracteres")]
    public string Nome { get; set; } = string.Empty;
}