using NaturalDisasterAPI.Models;

namespace NaturalDisasterAPI.DTO;

public class CidadeResponse
{ 
    public long Id { get; set; }
    public string Estado { get; set; }
    public string Nome { get; set; }
    public List<DroneResponse> Drones { get; set; }
    public List<UsuarioResponse> Usuarios { get; set; }
}