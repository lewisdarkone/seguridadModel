using System.DirectoryServices.AccountManagement;
using com.softpine.muvany.core.Exceptions;
using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.models.Constants;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.Request.RequestsIdentity;

namespace com.softpine.muvany.infrastructure.Identity.Services
{
    /// <summary>
    /// Servicio para los procesos relacionados a los Usuarios de Dominio
    /// </summary>
    public class UsersDomainService : IUsersDomainService
    {

        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public UsersDomainService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Función que retorna el usuario de dominio por su email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<UserDomainRequest> GetUserDomainByEmailAsync(string email)
        {
            var users =  await _unitOfWork.UvwUsuariosDominioRepository.GetAll();
            if (users == null) throw new BusinessException(ApiConstants.Messages.DataEmptyError);

            var user = users.Where(u => !string.IsNullOrEmpty(u.Email) && u.Email.ToUpper().Trim() == email.ToUpper().Trim()).FirstOrDefault();

            if (user == null) throw new BusinessException(ApiConstants.Messages.UserNotFound);    
            
            return  new UserDomainRequest {
            NombreCompleto = user.NombreCompleto,
            Email = email,
            Puesto = user.Puesto,
            IdEmpleado = user.CodigoEmpleado.StringToInt()
            };
        }

        /// <summary>
        /// Funcion que valida al usuario registrado que este en el dominio
        /// </summary>
        /// <param name="sUserName"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public async Task<bool> ValidateCredentials(string sUserName, string sPassword)
        {
            var result = false;
            PrincipalContext oPrincipalContext = GetPrincipalContext();
            result =  oPrincipalContext.ValidateCredentials(sUserName, sPassword);
            return result;
        }

        /// <summary>
        /// Obtiene el la base del contexto principal
        /// </summary>
        /// <returns>Returns the PrincipalContext object</returns>
        public PrincipalContext GetPrincipalContext()
        {
            PrincipalContext oPrincipalContext = new PrincipalContext
               (ContextType.Domain, ApiConstants.ValidateUsersDomainVariables.sDomain,
               ApiConstants.ValidateUsersDomainVariables.sServiceUser, ApiConstants.ValidateUsersDomainVariables.sServicePassword);
            return oPrincipalContext;
        }       
    }
}
