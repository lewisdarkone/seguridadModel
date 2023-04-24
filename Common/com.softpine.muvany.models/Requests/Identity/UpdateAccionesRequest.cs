using System.ComponentModel.DataAnnotations;

namespace com.softpine.muvany.models.Requests;

/// <summary>
/// 
/// </summary>
public class UpdateAccionesRequest
{


    [Required(ErrorMessage = "id es un campo obligatorio")]
    public Int32? Id { get; set; }

    public String? Nombre { get; set; }



}
