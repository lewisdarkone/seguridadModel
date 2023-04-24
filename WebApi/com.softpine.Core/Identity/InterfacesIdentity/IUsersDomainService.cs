
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.Request.RequestsIdentity;

namespace com.softpine.muvany.core.Identity.InterfacesIdentity
{
    /// <summary>
    /// Interface para lo procesos de los usuarios de Dominio
    /// </summary>
    public interface IUsersDomainService: ITransientService
    {
        /// <summary>
        /// Funcion para obtener los datos de los usuarios de dominio
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<UserDomainRequest> GetUserDomainByEmailAsync(string email);

        /// <summary>
        /// Funcion para verificar si las credenciales son correctas
        /// </summary>
        /// <param name="sUserName"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        Task<bool> ValidateCredentials(string sUserName, string sPassword);
    }
}
