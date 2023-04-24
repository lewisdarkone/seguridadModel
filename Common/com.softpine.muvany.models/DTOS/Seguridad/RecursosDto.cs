#pragma warning disable 1591


namespace com.softpine.muvany.models.DTOS
{
    public class RecursosDto
    {

        public int Id { get; set; }

        public string Nombre { get; set; }

        public int? IdModulo { get; set; }

        public string? NombreModulo { get; set; }

        public int? EsMenuConfiguracion { get; set; }
        public string? DescripcionMenuConfiguracion { get; set; }
        public string? Url { get; set; }
    }

}
