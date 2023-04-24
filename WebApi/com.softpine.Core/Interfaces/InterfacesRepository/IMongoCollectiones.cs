using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.softpine.muvany.models.Entities.Subscripcion;
using com.softpine.muvany.models.Interfaces;
using MongoDB.Driver;

namespace com.softpine.muvany.core.Interfaces.InterfacesRepository;

/// <summary>
/// Obtener colecciones en mongoDB
/// </summary>
public interface IMongoCollectiones : IDisposable, IScopedService
{
    /// <summary>
    /// Coleccion de suscripcion catalogo
    /// </summary>
    IMongoCollection<SuscripcionCatalog> SuscripcionCatalogCollection { get; }
}
