using System.ComponentModel.DataAnnotations;

namespace NaturalDisasterAPI.DTO.SensorDTO;

public class SensorRequest
{
    [Required(ErrorMessage = "ID do drone é obrigatório")]
    public long DroneId { get; set; }   
    
    [Required(ErrorMessage = "Tipo de sensor é obrigatório")]
    public string Tipo { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Status do sensor é obrigatório")]
    public string Status { get; set; } = string.Empty;
    
    public string Descricao { get; set; } = string.Empty;
}