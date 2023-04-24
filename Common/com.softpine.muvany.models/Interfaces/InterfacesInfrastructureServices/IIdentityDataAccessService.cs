using System.Security.Claims;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Interfaces;

namespace com.softpine.muvany.infrastructure.Identity.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IIdentityDataAccessService: ITransientService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="httpVerb"></param>
        /// <returns></returns>
        Task <EndpointsPermisosDto> GetPermsisosEndpoint(ClaimsPrincipal principal, string controller, string action, string httpVerb);
     }
}
