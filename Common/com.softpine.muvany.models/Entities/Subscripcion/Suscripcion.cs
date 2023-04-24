using com.softpine.muvany.models.Entities.Generals;
using com.softpine.muvany.models.Enumerations;

namespace com.softpine.muvany.models.Entities.Subscripcion;

/// <summary>
/// Productos de suscripciones que los clientes pueden adquirir para operar en muvany
/// </summary>
public class Suscripcion : EntidadBase
{
    public UserBasicInfo Owner { get; set; }
    public string NombreSuscripcion { get; set; }
    public string Descripcion { get; set; }
    public TiposSuscripcionesEnum TipoSuscripcion { get; set; }
    public ICollection<UserBasicInfo>? Empleados { get; set; }
    public bool Principal { get; set; }
    public ICollection<FacturacionSuscripcion>? Facturaciones { get; set; }
    public float PrecioPorUsuario { get; set; }
    public DateTime FechaComprado { get; set; }
    public float Precio { get; set; }
    public float? Descuento { get; set; }
    public float TAX { get; set; }
    public float CostoFinal { get; set; }
    public DateTime SiguienteFacturacion { get; set; }
}
