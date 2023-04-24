using com.softpine.muvany.models.Entities.Generals;
using com.softpine.muvany.models.Enumerations;

namespace com.softpine.muvany.models.Entities.Subscripcion;

/// <summary>
/// Productos de suscripciones que los clientes pueden adquirir para operar en muvany
/// </summary>
public class SuscripcionCatalog : EntidadBase
{
    public string NombreCatalogo { get; set; }
    public string Descripcion { get; set; }
    public TiposSuscripcionesEnum TipoSuscripcion { get; set; }
    public float Precio { get; set; }
    public float PrecioPorUsuario { get; set; }
    public float TAX { get; set; }
    public float? Descuento { get; set; }
    public Int16 DiasGracia { get; set; }
    public ICollection<string>? Imagenes { get; set; }
}
