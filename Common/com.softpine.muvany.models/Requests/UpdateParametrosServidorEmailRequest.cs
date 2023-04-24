using System.ComponentModel.DataAnnotations;

namespace com.softpine.muvany.models.Requests;

/// <summary>
/// 
/// </summary>
public class UpdateParametrosServidorEmailRequest
{


    [Required(ErrorMessage = "id es un campo obligatorio")]
    public Int32? Id { get; set; }

    public String? Remitente { get; set; }

    public String? Host { get; set; }

    public String? Puerto { get; set; }

    public String? Usuario { get; set; }

    public String? Password { get; set; }


}



