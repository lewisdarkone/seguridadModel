#pragma warning disable 1591
using System.ComponentModel.DataAnnotations;

namespace com.softpine.muvany.models.Requests;

/// <summary>
/// 
/// </summary>
public class CreateRecursosRequest
{



    [Required(ErrorMessage ="El campo Nombre es obligatorio")]
    public String Nombre { get; set; }
    [Required(ErrorMessage = "El campo IdModulo es obligatorio")]
    public Int32? IdModulo { get; set; }

    public int? EsMenuConfiguracion { get; set; }
    public string? DescripcionMenuConfiguracion { get; set; }
    public string? Url { get; set; }


}



