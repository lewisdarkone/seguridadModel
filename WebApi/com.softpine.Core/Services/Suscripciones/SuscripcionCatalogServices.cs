using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.core.Interfaces.InterfacesRepository;
using com.softpine.muvany.models.Entities;
using com.softpine.muvany.models.Entities.Subscripcion;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.Requests.SuscripcionCatalog;
using MongoDB.Driver;

namespace com.softpine.muvany.core.Services.Suscripciones;

/// <summary>
/// Implementa los metodos de el catalogo de suscripciones
/// </summary>
public class SuscripcionCatalogServices : ISuscripcionCatalogService
{
    private readonly IMongoCollectiones _collection;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collection"></param>
    public SuscripcionCatalogServices(IMongoCollectiones collection, IMapper mapper, ICurrentUser currentUser)
    {
        _collection = collection;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    

    /// <summary>
    /// Crea un nuevo producto catalogo de suscripción
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> Create(CreateSuscripcionCatalogRequest request)
    {
        var userBasic = new UserBasicInfo() { Id = _currentUser.GetUserId(), Email = _currentUser.GetUserEmail(), NombreCompleto = _currentUser.GetUserName() };
        
        var catalog = _mapper.Map<SuscripcionCatalog>(request);
        catalog.CreadoPor = userBasic;
        catalog.ModificadoPor = userBasic;
        catalog.SetCreateDates();

        await _collection.SuscripcionCatalogCollection.InsertOneAsync(catalog);

        return true;
    }
}
