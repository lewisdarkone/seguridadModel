#pragma warning disable 1591

namespace com.softpine.muvany.models.DTOS;

public class EndpointsDto
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Controlador { get; set; }
    public string? Metodo { get; set; }
    public string? HttpVerbo { get; set; }
    public bool Estado { get; set; }
    public PermisosDto? Permiso { get; set; }

    
}
