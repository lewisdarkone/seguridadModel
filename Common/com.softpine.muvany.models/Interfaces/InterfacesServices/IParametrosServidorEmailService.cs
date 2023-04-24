
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.Requests;

namespace com.softpine.muvany.models.Interfaces.InterfacesServices;
	/// <summary>
    /// Interfaz para el servicio del mantenimiento de ParametrosServidorEmail
    /// </summary>
    public interface IParametrosServidorEmailService : ITransientService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametrosServidorEmail"></param>
        /// <returns></returns>
        Task<ParametrosServidorEmailDto> CreateAsync(CreateParametrosServidorEmailRequest parametrosServidorEmail);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<PagedList<ParametrosServidorEmailDto>> GetAllAsync(ParametrosServidorEmailQueryFilter filters);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(UpdateParametrosServidorEmailRequest request);
    }
		
		


