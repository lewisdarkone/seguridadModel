
using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.ResponseModels.AccionesResponse;

namespace com.softpine.muvany.clientservices
{
    public interface IAccionesService
    {
        /// <summary>
        /// Obtiene una lista de acciones que pueden ser filtradas por parametros en query
        /// </summary>
        /// <param name="token"></param>
        /// <param name="query">Sucursal=123,Nombre=abc,PageSize=123,PageNumber=123</param>
        /// <returns></returns>
        Task<GetAccionesResponse?> GetAcciones(string query="");

        /// <summary>
        /// Crear una nueva Accion.
        /// </summary>
        /// <param name="createAccionesRequest">Objeto que contiene el Nombre de la acción</param>
        /// <param name="token"></param>
        /// <returns>CreateAccionesResponse?</returns>
        Task<CreateAccionesResponse?> CreateAcciones(CreateAccionesRequest createAccionesRequest);

        /// <summary>
        /// Actualiza una accion
        /// </summary>
        /// <param name="updateAccionesRequest"></param>
        /// <param name="token"></param>
        /// <returns>UpdateAccionesResponse?</returns>
        Task<UpdateAccionesResponse?> UpdateAcciones(UpdateAccionesRequest updateAccionesRequest);

        /// <summary>
        /// Elimina una accion
        /// </summary>
        /// <param name="actionId">Sucursal de la accion a eliminar</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<DeleteAccionesResponse?> DeleteAccion(string actionId, string token);
    }
}
