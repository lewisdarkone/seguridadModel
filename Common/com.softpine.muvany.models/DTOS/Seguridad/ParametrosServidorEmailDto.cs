#pragma warning disable 1591


namespace com.softpine.muvany.models.DTOS{
    public class ParametrosServidorEmailDto
    {

        public int Id { get; set; }

        public string Remitente { get; set; }

        public string Host { get; set; }

        public string Puerto { get; set; }

        public string Usuario { get; set; }

        public string Password { get; set; }
    }}
