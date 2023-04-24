using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.models.Entities.EntitiesIdentity;
using com.softpine.muvany.models.Interfaces;

namespace com.softpine.muvany.core.Identity.InterfacesIdentity
{
    /// <summary>
    /// Interface para los procesos de las entidades y servicios relacionados a Identity
    /// </summary>
    public interface IUnitOfWorkIdentity : IDisposable, IScopedService
    {
        /// <summary>
        /// Repositorio para los Permisos de los usuarios
        /// </summary>
        IPermisosRepository PermisosRepository { get; }
        /// <summary>
        /// Repositorio con los permisos para los Endpoint del Api
        /// </summary>
        IEndpointsRepository EndpointsRepository { get; }


        /// <summary>
        /// 
        /// </summary>
        IRepositoryIdentity<Acciones> Acciones { get; }
        /// <summary>
        /// 
        /// </summary>
        IRecursosRepository Recursos { get; }
        /// <summary>
        /// 
        /// </summary>
        IRepositoryIdentity<Modulos> ModulosRepository { get; }

        /// <summary>
        /// 
        /// </summary>
        void SaveChanges();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}
