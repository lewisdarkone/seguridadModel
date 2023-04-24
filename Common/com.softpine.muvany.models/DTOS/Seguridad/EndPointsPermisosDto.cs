#pragma warning disable 1591

namespace com.softpine.muvany.models.DTOS
{
    public class EndpointsPermisosDto
    {
        public int Id { get; set; }
        public int EndpointId { get; set; }
        public int PermisoId { get; set; }
        public string? PermisoAccion { get; set; }
        public string? PermisoRecurso { get; set; }
        public bool Estado { get; set; }
    }
}
