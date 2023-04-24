using System.ComponentModel.DataAnnotations;

namespace com.softpine.muvany.models.Requests;

/// <summary>
/// 
/// </summary>
public class UpdateRecursosRequest
{


    [Required(ErrorMessage = "id es un campo obligatorio")]
    public Int32? Id { get; set; }

    public String? Nombre { get; set; }

    public Int32? IdModulo { get; set; }
    public int? EsMenuConfiguracion { get; set; }
    public string? DescripcionMenuConfiguracion { get; set; }
    public string? Url { get; set; }


}



