namespace com.softpine.muvany.models.Requests;

/// <summary>
/// Formulario para crear un nuevo Endpoint
/// </summary>
public class CreateEndpointRequest
{
    /// <summary>
    /// 
    /// </summary>
    public string Nombre { get; set; } = default!;
    /// <summary>
    /// 
    /// </summary>
    public string Controlador { get; set; } = default!;
    /// <summary>
    /// 
    /// </summary>
    public string Metodo { get; set; } = default!;
    /// <summary>
    /// 
    /// </summary>
    public string HttpVerbo { get; set; } = default!;

}


