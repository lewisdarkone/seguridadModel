
namespace com.softpine.muvany.models.Requests;

/// <summary>
/// Clase para las propiedades necesarias para la creación de los adjuntos de las solicitudes
/// </summary>
public class AddAdjuntoPhotoProfileRequest
{
    /// <summary>
    /// 
    /// </summary>
    public byte[] AdjuntoInBytes { get; set; } = default!;
    /// <summary>
    /// 
    /// </summary>
    public string? IdUser { get; set; }

}


