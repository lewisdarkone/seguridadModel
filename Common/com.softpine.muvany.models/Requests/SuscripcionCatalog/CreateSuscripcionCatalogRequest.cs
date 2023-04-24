
using com.softpine.muvany.models.Enumerations;

namespace com.softpine.muvany.models.Requests.SuscripcionCatalog;

public class CreateSuscripcionCatalogRequest
{
    public string NombreCatalogo { get; set; }
    public string Descripcion { get; set; }
    public TiposSuscripcionesEnum TipoSuscripcion { get; set; }
    public float Precio { get; set; }
    public float PrecioPorUsuario { get; set; }
    public float? TAX { get; set; }
    public float? Descuento { get; set; }
    public Int16? DiasGracia { get; set; }
    public ICollection<string>? Imagenes { get; set; }
}
