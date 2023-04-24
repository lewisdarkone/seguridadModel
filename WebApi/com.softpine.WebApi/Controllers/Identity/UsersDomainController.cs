using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Net;
using Matrices.Api.Responses;
using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.infrastructure.Identity.Auth.Permissions;
using com.softpine.muvany.infrastructure.Shared.Middleware;
using com.softpine.muvany.models.Request.RequestsIdentity;

namespace com.softpine.muvany.WebApi.Controllers
{
    /// <summary>
    /// Controlador para los procesos relacionados a la vista de usuarios de Dominio
    /// </summary>
    public class UsersDomainController : VersionNeutralApiController
    {
        private readonly IUsersDomainService _userService;

        /// <summary>
        /// Constructor para la inyección de interfaces.
        /// </summary>
        /// <param name="userService"></param>
        public UsersDomainController(IUsersDomainService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Retorna la lista de todos los usuarios registrados.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [Authorization("Authorization")]
        [OpenApiOperation("Retorna la lista de todos los usuarios registrados en el dominio.", "")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<UserDomainRequest>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("get/{email}", Name = nameof(GetUsersDomainByEmailAsync))]
        public async Task<IActionResult> GetUsersDomainByEmailAsync(string email)
        {
            var response = await _userService.GetUserDomainByEmailAsync(email);
            return Ok(new ApiResponse<UserDomainRequest>(response));
        }
    }
}
