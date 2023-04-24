using AutoMapper;
using Microsoft.Extensions.Options;
using com.softpine.muvany.core.Exceptions;
using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.Constants;
using com.softpine.muvany.models.Request.RequestsIdentity;
using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.Entities.EntitiesIdentity;

namespace com.softpine.muvany.core.Services
{
    /// <summary>
    /// Servicio para los proceso relacionados a los permisos
    /// </summary>
    public class PermisosService : IPermisosService
    {
        private readonly IUnitOfWorkIdentity _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;

        /// <summary>
        /// Constructor para la inyeccion de las dependencias
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        /// <param name="options"></param>
        public PermisosService(IUnitOfWorkIdentity unitOfWork, IMapper mapper, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paginationOptions = options.Value;
        }

        /// <summary>
        /// Función para obtener los permisos por su Sucursal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PermisosDto> GetPermiso(int id)
        {
            var permiso = await _unitOfWork.PermisosRepository.GetById(id);

            var permisoDto = _mapper.Map<PermisosDto>(permiso);

            return permisoDto;
        }

        /// <summary>
        /// Función para obtener todos los permisos registrados
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public async Task<PagedList<PermisosDto>> GetPermisos(PermisosQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            PermisosQueryFilter _filters = (PermisosQueryFilter)filters;

            var permisos = await _unitOfWork.PermisosRepository.GetPermisos(_filters.Descripcion);

            var permisosDtos = _mapper.Map<IEnumerable<PermisosDto>>(permisos).ToList();

            var pagedPermisos = PagedList<PermisosDto>.Create(permisosDtos, filters.PageNumber, filters.PageSize);
            return pagedPermisos;
        }

        /// <summary>
        /// Función para obtener todos los permisos segun sus filtros
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="httpVerb"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedException"></exception>
        public async Task<PermisosDto> GetPermisoEndpoint(string controller, string action, string httpVerb)
        {
            var endpoint = await _unitOfWork.EndpointsRepository.GetActiveEndpoint(controller, action, httpVerb);

            var endpointPermisos = endpoint.EndpointsPermisos;
            var permiso = endpointPermisos != null ? endpointPermisos.Permiso : throw new UnauthorizedException(ApiConstants.Messages.UnauthorizedAccess);

            var permisoDto = _mapper.Map<PermisosDto>(permiso);

            return permisoDto;
        }

        /// <summary>
        /// Función para obtener los permisos relacionados con los roles claims
        /// </summary>
        /// <param name="action"></param>
        /// <param name="resource"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedException"></exception>
        public async Task<PermissionsRequest> GetPermisosClaims(string action, string resource)
        {
            var endpoint = await _unitOfWork.EndpointsRepository.GetActiveEndpointPermisos(resource, action);
            var endpointPermisos = endpoint.EndpointsPermisos;
            var permiso = endpointPermisos != null ? endpointPermisos.Permiso : throw new UnauthorizedException("Authentication Failed.");

            return new PermissionsRequest()
            {
                Id = permiso.Id,
                Descripcion = permiso.Descripcion
            };
        }

        /// <summary>
        /// Función para insertar los permisos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PermisosDto> InsertPermiso(CreateOrUpdatePermisoRequest request)
        {
            var accion = await _unitOfWork.Acciones.GetById(request.IdAccion);
            var recurso = await _unitOfWork.Recursos.GetById(request.IdRecurso);
            if (accion == null || recurso == null)
                throw new NotFoundException($"El recurso {request.IdAccion} o la acción {request.IdRecurso} no fueron encontrados.");

            

            var permiso = new Permisos()
            {
                Descripcion = $"{accion.Nombre}{recurso.Nombre}",
                IdAccion = request.IdAccion,
                IdRecurso = request.IdRecurso,
                EsBasico = request.EsBasico != null ? request.EsBasico : 0,  
            };

            var created = await _unitOfWork.PermisosRepository.AddPermiso(permiso);
            await _unitOfWork.SaveChangesAsync();

            var permisoCreated = _mapper.Map<PermisosDto>(created);

            return permisoCreated;
        }

        /// <summary>
        /// Función para actualizar los permisos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePermiso(CreateOrUpdatePermisoRequest request)
        {
            var existingPermiso = await _unitOfWork.PermisosRepository.GetById(request.Id);
            var accion = await _unitOfWork.Acciones.GetById(request.IdAccion);
            var recurso = await _unitOfWork.Recursos.GetById(request.IdRecurso);
            if (accion == null || recurso == null)
                throw new NotFoundException($"El recurso {request.IdAccion} o la acción {request.IdRecurso} no fueron encontrados.");

            if ( existingPermiso == null )
                throw new BusinessException(ApiConstants.Messages.UpdateNotAllowed);
            existingPermiso.Descripcion = $"{accion.Nombre}{recurso.Nombre}";
            existingPermiso.IdAccion = request.IdAccion;
            existingPermiso.IdRecurso = request.IdRecurso;
            existingPermiso.EsBasico = request.EsBasico != null ? request.EsBasico : existingPermiso.EsBasico;

            _unitOfWork.PermisosRepository.Update(existingPermiso);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Función para eliminar los permisos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeletePermiso(int id)
        {
            await _unitOfWork.PermisosRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }


    }
}
