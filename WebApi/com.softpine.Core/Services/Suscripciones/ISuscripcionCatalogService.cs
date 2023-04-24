
using com.softpine.muvany.models.Requests.SuscripcionCatalog;

namespace com.softpine.muvany.core.Services.Suscripciones;

/// <summary>
/// Interfaz para el catalogo de suscripciones
/// </summary>
public interface ISuscripcionCatalogService
{
    /// <summary>
    /// Crea un nuevo producto catalogo de suscripción
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<bool> Create(CreateSuscripcionCatalogRequest request);
}
