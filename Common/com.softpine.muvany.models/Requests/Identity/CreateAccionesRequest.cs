#pragma warning disable 1591
using System.ComponentModel.DataAnnotations;

namespace com.softpine.muvany.models.Requests;

/// <summary>
/// 
/// </summary>
public class CreateAccionesRequest
{


    [Required]
    public String Nombre { get; set; }


}



