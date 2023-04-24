#pragma warning disable 1591


using com.softpine.muvany.models.Entities.Subscripcion;
using com.softpine.muvany.models.EntitiesViews;
using com.softpine.muvany.models.Interfaces;

namespace com.softpine.muvany.core.Interfaces
{
    /// <summary>
    /// Interfaz para la unidad de trabajo con las Entidades de los mantenimientos basicos
    /// </summary>
    public interface IUnitOfWork : IDisposable, IScopedService
    {
        #region Repositorios Suscripciones
        #endregion


        /// <summary>
        /// Repositorio para los Usuarios de Dominio
        /// </summary>
        IRepository<UvwUsuariosDominio> UvwUsuariosDominioRepository { get; }

        /// <summary>
        /// 
        /// </summary>
        void SaveChanges();

        Task SaveChangesAsync(bool isSucceful = true);
    }
}
