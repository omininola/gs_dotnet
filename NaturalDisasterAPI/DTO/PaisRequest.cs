using System.ComponentModel.DataAnnotations;

namespace NaturalDisasterAPI.DTO;

public class PaisRequest
{
    [Required(ErrorMessage = "Nome do país é obrigatório")]
    [StringLength(50, ErrorMessage = "Nome do país deve ter no máximo 50 caracteres")]
    public string Nome { get; set; } = string.Empty;
}