

using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.ResponseModels;
using com.softpine.muvany.models.ResponseModels.RecursoResponse;

namespace com.softpine.muvany.clientservices;

public interface IRecursoService
{
    /// <summary>
    /// Retorna el listado del menu autorizado para el usuario
    /// </summary>
    /// <returns>GetMenuResponse</returns>
    Task<GetMenuResponse> GetMenu();

    /// <summary>
    /// Retorna una lista de todos los recursos.
    /// </summary>
    /// <param name="token"></param>
    /// <param name="query">Sucursal=123, Nombre=abc, IdModulo=123, EsMenuConfiguracion=123, DescripcionMenuConfiguracion=abc,PageSize=123,PageNumber=123</param>
    /// <returns>GetRecursosResponse</returns>
    Task<GetRecursosResponse?> Get(string query = "");

    /// <summary>
    /// Inserta un nuevo Recurso
    /// </summary>
    /// <param name="createRecursoRequest">Objeto con propiedades para crear un recurso nuevo</param>
    /// <param name="token"></param>
    /// <returns>CreateRecursoResponse?</returns>
    Task<CreateRecursoResponse?> Create(CreateRecursosRequest createRecursoRequest);

    /// <summary>
    /// Actualiza un nuevo recurso
    /// </summary>
    /// <param name="recurso"></param>
    /// <param name="token"></param>
    /// <returns>UpdateResponse?</returns>
    Task<UpdateResponse?> Update(UpdateRecursosRequest recurso);

    /// <summary>
    /// Elimina un recurso
    /// </summary>
    /// <param name="recursoId">Sucursal del recurso a eliminar</param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<DeleteRecursoResponse?> Delete(string recursoId);
}
