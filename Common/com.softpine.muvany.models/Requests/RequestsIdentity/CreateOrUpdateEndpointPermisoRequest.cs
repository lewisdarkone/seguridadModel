
namespace com.softpine.muvany.models.Requests;

/// <summary>
/// 
/// </summary>
public class CreateOrUpdateEndpointPermisoRequest
{
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int EndpointId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int PermisoId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool Estado { get; set; }
}


