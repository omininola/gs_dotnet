using System.ComponentModel.DataAnnotations;

namespace NaturalDisasterAPI.DTO.UsuarioDTO;

public class UsuarioRequest
{
    [Required(ErrorMessage = "Nome do usuário é obrigatório")]
    [StringLength(50, ErrorMessage = "Nome do usuário deve ter no máximo 50 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email do usuário é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha do usuário é obrigatória")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Senha deve ter entre 6 e 100 caracteres")]
    public string Senha { get; set; } = string.Empty;
}