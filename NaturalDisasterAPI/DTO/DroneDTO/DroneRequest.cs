using System.ComponentModel.DataAnnotations;

namespace NaturalDisasterAPI.DTO.DroneDTO;

public class DroneRequest
{
    [Required(ErrorMessage = "Modelo do drone é obrigatório")]
    [StringLength(20, ErrorMessage = "Modelo do drone deve ter no máximo 20 caracteres")]
    public string Modelo { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Status do drone é obrigatório")]
    [StringLength(20, ErrorMessage = "Status do drone deve ter no máximo 20 caracteres")]
    public string Status { get; set; } = string.Empty;
}