using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSwag.Annotations;
using Matrices.Api.Responses;
using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.infrastructure.Shared.Middleware;
using com.softpine.muvany.infrastructure.Identity.Auth.Permissions;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.Requests;
using com.softpine.muvany.core.Services.Suscripciones;
using Microsoft.AspNetCore.Authorization;
using com.softpine.muvany.models.Requests.SuscripcionCatalog;

namespace com.softpine.muvany.WebApi.Controllers;
/// <summary>
/// Controlador del catalogo de suscripciones
/// </summary>
public class SuscripcionCatalogController : VersionNeutralApiController
{
    private readonly IUriService _uriService;
    private readonly ISuscripcionCatalogService _service;
    private readonly IMailService _mailService;

    /// <summary>
    /// Contructor 
    /// </summary>
    /// <param name="accionesService"></param>
    /// <param name="uriService"></param>
    public SuscripcionCatalogController(ISuscripcionCatalogService service, IUriService uriService, IMailService mailService)
    {

        _service = service;
        _uriService = uriService;
        _mailService = mailService;

    }


    /// <summary>
    /// Crea un nuevo catalogo de suscripciones
    /// </summary>
    /// <param name="request">Objeto Acciones que va a ser agregado</param>
    /// <returns>true o false</returns>
    [HttpPost("create")]
    //[Authorization("Authorization")]
    [AllowAnonymous]
    [OpenApiOperation("Crea un nuevo catalogo de suscripciones", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> CreateAsync(CreateSuscripcionCatalogRequest request)
    {

        var created = await _service.Create(request);
        var response = new ApiResponse<bool>(created);
        return Ok(response);
    }




}

