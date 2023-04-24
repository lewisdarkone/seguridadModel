using System.ComponentModel.DataAnnotations;

namespace com.softpine.muvany.models.Request.RequestsIdentity;

/// <summary>
/// 
/// </summary>
public class UpdateModulosRequest
{


    [Required(ErrorMessage = "id es un campo obligatorio")]
    public int? Id { get; set; }

    public string? Nombre { get; set; }

    public int? ModuloPadre { get; set; }
    public string? Cssicon { get; set; }

    public Int32? Estado { get; set; }



}



