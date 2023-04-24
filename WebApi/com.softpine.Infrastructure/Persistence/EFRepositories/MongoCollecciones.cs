using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.softpine.muvany.core.Interfaces.InterfacesRepository;
using com.softpine.muvany.infrastructure.Identity.Models;
using com.softpine.muvany.models.Entities.Subscripcion;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace com.softpine.muvany.infrastructure.Persistence.EFRepositories;

/// <summary>
/// Obtener colecciones en mongoDB
/// </summary>
public class MongoCollecciones : IMongoCollectiones
{
    private readonly IMongoCollection<SuscripcionCatalog>? _suscripcionCatalogCollection;
    private readonly IMongoDatabase _database;

    /// <summary>
    /// Obtener colecciones de suscripcion db
    /// </summary>
    /// <param name="options"></param>
    public MongoCollecciones(IOptions<DatabaseSettings> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionStringMongoDb);
        _database = mongoClient.GetDatabase(options.Value.SuscripcionDatabase);
    }

    /// <summary>
    /// Obtener la suscripciones
    /// </summary>
    public IMongoCollection<SuscripcionCatalog> SuscripcionCatalogCollection => _suscripcionCatalogCollection ?? _database.GetCollection<SuscripcionCatalog>(nameof(SuscripcionCatalog));

    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Dispose()
    {
        
    }
}
