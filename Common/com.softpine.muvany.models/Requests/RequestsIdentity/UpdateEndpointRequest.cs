
namespace com.softpine.muvany.models.Requests;

/// <summary>
/// Clase para Actualizar los Endpoints
/// </summary>
public class UpdateEndpointRequest
{
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Nombre { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Controlador { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Metodo { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string HttpVerbo { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool? Estado { get; set; }
}


