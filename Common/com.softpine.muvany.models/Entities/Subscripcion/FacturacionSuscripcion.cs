using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.softpine.muvany.models.Entities.Generals;
using com.softpine.muvany.models.Enumerations;

namespace com.softpine.muvany.models.Entities.Subscripcion;

/// <summary>
/// Representa los pagos que se hacen en las subscripciones segun el estado actual de cada subscripcion
/// </summary>
public class FacturacionSuscripcion : EntidadBase
{
    public DateTime FechaGenerado { get; set; }
    public DateTime FechaLimitePago { get; set; }
    public DateTime? FechaSuspensionCuenta { get; set; }
    public float Monto { get; set; }
    public int CantidadEmpleados { get; set; }
    public float CostoEmpleado { get; set; }
    public float CostoBaseSuscripcionArchivos { get; set; }
    public DateTime? FechaPagado { get; set; }
    public float? Descuento { get; set; }
    public float TAX { get; set; }
    public EstadoFacturacionSuscripcion Estado { get; set; }
}
