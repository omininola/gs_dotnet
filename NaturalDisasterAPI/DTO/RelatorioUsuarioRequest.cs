using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace NaturalDisasterAPI.DTO;

public class RelatorioUsuarioRequest
{
    [Required(ErrorMessage = "Id do usuário é obrigatório")]
    public long UsuarioId { get; set; }
    
    [Required(ErrorMessage = "Descrição do relatório é obrigatória")]
    public string Descricao { get; set; } = string.Empty;
}