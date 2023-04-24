#pragma warning disable 1591

using com.softpine.muvany.models.Entities.EntitiesIdentity;

namespace com.softpine.muvany.models.DTOS;

public class ModulosDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int? ModuloPadre { get; set; }
    public string? NombreModuloPadre { get; set; }
    public string? URL { get; set; }
    public string? Cssicon { get; set; }
    public int? Estado { get; set; }

    public ICollection<ModulosDto>? Recursos { get; set; }
}
