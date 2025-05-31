using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace NaturalDisasterAPI.DTO;

public class RelatorioDroneRequest
{
    [Required(ErrorMessage = "Id do drone é obrigatório")]
    public long DroneId { get; set; }
    
    [Required(ErrorMessage = "Descrição do relatório é obrigatória")]
    public string Descricao { get; set; } = string.Empty;
}