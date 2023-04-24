using System.ComponentModel.DataAnnotations;
namespace com.softpine.muvany.models.Requests;

    /// <summary>
    /// 
    /// </summary>
    public class CreateParametrosServidorEmailRequest
    {


        [Required]
    [StringLength(100, ErrorMessage = "El remitente no puede tener mas de 100 caracteres")]
    public String Remitente { get; set; } 

        [Required]
    [StringLength(100, ErrorMessage = "El host no puede tener mas de 100 caracteres")]
    public String Host { get; set; } 

        [Required]
        [StringLength(10, ErrorMessage = "El puerto no puede tener mas de 10 caracteres")]
        public String Puerto { get; set; } 

        public String? Usuario { get; set; } 

        public String? Password { get; set; } 

    }


