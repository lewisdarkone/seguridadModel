#pragma warning disable 1591

namespace com.softpine.muvany.models.Enumerations;

public enum EstadosEnum
{
    Eliminado,
    Activo

}

public enum EstadoFacturacionSuscripcion
{
    Generado = 1,
    Pendiente,
    Retrasado,
    Pagado
}

public enum EstadoSuscripcion
{
    Activo = 1,
    Suspendido,
    Cancelado,
    Desactivado
}
