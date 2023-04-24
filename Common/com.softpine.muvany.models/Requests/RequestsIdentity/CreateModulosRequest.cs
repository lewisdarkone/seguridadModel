using System.ComponentModel.DataAnnotations;

namespace com.softpine.muvany.models.Request.RequestsIdentity;

/// <summary>
/// 
/// </summary>
public class CreateModulosRequest
{


    [Required]
    public string Nombre { get; set; }

    public int? ModuloPadre { get; set; }
    public string? Cssicon { get; set; }


}



