using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.infrastructure.Identity.Context;
using com.softpine.muvany.infrastructure.Identity.EFIdentityRepositories;
using com.softpine.muvany.infrastructure.Repositories;
using com.softpine.muvany.models.Entities.EntitiesIdentity;

namespace com.softpine.muvany.infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Unidad de trabajo para las entidades de tipo Identidad
    /// </summary>
    public class UnitOfWorkIdentity : IUnitOfWorkIdentity
    {
        private readonly IdentityContext _context;
        private readonly IPermisosRepository _permisosRepository;
        private readonly IEndpointsRepository _endpointsRepository;
        private readonly IRepositoryIdentity<Acciones> _acciones;
        private readonly IRecursosRepository _recursos;
        private readonly IRepositoryIdentity<EndpointsPermisos> _endpointsPermisos;
        private readonly IRepositoryIdentity<Modulos> modulosRepository;

        /// <summary>
        /// Contructor para la inyeccion de los componentes
        /// </summary>
        /// <param name="context"></param>
        /// <param name="permisosRepository"></param>
        /// <param name="endpointsRepository"></param>
        /// <param name="acciones"></param>
        /// <param name="recursos"></param>
        /// <param name="endpointsPermisos"></param>
        /// <param name="usuariosSuplidor"></param>
        /// <param name="suplidorRepository"></param>
        /// <param name="usuarioSuplidorRepository"></param>
        public UnitOfWorkIdentity(IdentityContext context, IPermisosRepository permisosRepository, IEndpointsRepository endpointsRepository
            , IRepositoryIdentity<Acciones> acciones, IRecursosRepository recursos
            , IRepositoryIdentity<EndpointsPermisos> endpointsPermisos
            , IRepositoryIdentity<Modulos> modulosRepository

            )
        {
            _context = context;
            _permisosRepository = permisosRepository;
            _endpointsRepository = endpointsRepository;
            _acciones = acciones;
            _recursos = recursos;
            _endpointsPermisos = endpointsPermisos;
            this.modulosRepository = modulosRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        public IPermisosRepository PermisosRepository => _permisosRepository ?? new PermisosRepository(_context);
        /// <summary>
        /// 
        /// </summary>
        public IEndpointsRepository EndpointsRepository => _endpointsRepository ?? new EndpointsRepository(_context);
        /// <summary>
        /// 
        /// </summary>
        public IRepositoryIdentity<Acciones> Acciones => _acciones ?? new BaseRepositoryIdentity<Acciones>(_context);
        /// <summary>
        /// 
        /// </summary>
        public IRecursosRepository Recursos => _recursos ?? new RecursosRepository(_context);
        /// <summary>
        /// 
        /// </summary>
        public IRepositoryIdentity<EndpointsPermisos> EndpointsPermisos => _endpointsPermisos ?? new BaseRepositoryIdentity<EndpointsPermisos>(_context);
        /// <summary>
        /// 
        /// </summary>
        public IRepositoryIdentity<Modulos> ModulosRepository => modulosRepository ?? new BaseRepositoryIdentity<Modulos>(_context);
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if ( _context != null )
            {
                _context.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
