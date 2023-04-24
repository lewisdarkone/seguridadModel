using AutoMapper;
using Microsoft.Extensions.Options;
using com.softpine.muvany.core.Exceptions;
using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Constants;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.Entities.EntitiesIdentity;

namespace com.softpine.muvany.core.Services
{
    /// <summary>
    /// Servicio para los procesos relacionados a el mantenimiento de Endpoints
    /// </summary>
    public class EndpointsService : IEndpointsService
    {
        private readonly IUnitOfWorkIdentity _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;

        /// <summary>
        /// Controlador para la inyeccion de las dependencias
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        /// <param name="options"></param>
        public EndpointsService(IUnitOfWorkIdentity unitOfWork, IMapper mapper, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paginationOptions = options.Value;
        }

        /// <summary>
        /// Funcion que retorna un Endpoint por su Sucursal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EndpointsDto> GetEndpoint(int id)
        {
            var endpoint = await _unitOfWork.EndpointsRepository.GetById(id);
            if ( endpoint == null || endpoint.Id == 0 )
                throw new BusinessException(ApiConstants.Messages.InvalidId);

            var endpointDto = _mapper.Map<EndpointsDto>(endpoint);

            return endpointDto;
        }

        /// <summary>
        /// Funcion que retorna una lista de Endpoints
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public async Task<PagedList<EndpointsDto>> GetEndpoints(EndpointsQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var endpoints = await _unitOfWork.EndpointsRepository.GetAllEndpoints();
            if ( endpoints == null || endpoints.Count() == 0 )
                throw new BusinessException(ApiConstants.Messages.DataEmptyError);
            if ( filters.Id != null && (int)filters.Id != 0 )
                endpoints = endpoints.Where(e => e.Id.Equals(filters.Id)).ToList();
            if ( !string.IsNullOrEmpty(filters.Nombre) )
                endpoints = endpoints.Where(e => e.Nombre.Contains(filters.Nombre, StringComparison.OrdinalIgnoreCase)).ToList();
            if ( !string.IsNullOrEmpty(filters.Controlador) )
                endpoints = endpoints.Where(e => e.Controlador.Contains(filters.Controlador, StringComparison.OrdinalIgnoreCase)).ToList();
            if ( !string.IsNullOrEmpty(filters.Metodo) )
                endpoints = endpoints.Where(e => e.Metodo.Contains(filters.Metodo, StringComparison.OrdinalIgnoreCase)).ToList();
            if ( !string.IsNullOrEmpty(filters.HttpVerbo) )
                endpoints = endpoints.Where(e => e.HttpVerbo.Contains(filters.HttpVerbo, StringComparison.OrdinalIgnoreCase)).ToList();
            if ( filters.IsActive != null )
                endpoints = endpoints.Where(e => e.Estado.Equals(filters.IsActive)).ToList();

            if ( endpoints == null || endpoints.Count() == 0 )
                throw new BusinessException(ApiConstants.Messages.DataByfilterNotFound);
            var endpointsDtos = _mapper.Map<IEnumerable<EndpointsDto>>(endpoints).ToList();

            var pagedEndpoints = PagedList<EndpointsDto>.Create(endpointsDtos, filters.PageNumber, filters.PageSize);
            return pagedEndpoints;
        }

        /// <summary>
        /// Funcion que retorna una lista de Endpoints
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public async Task<PagedList<EndpointsDto>> GetEndpoint(EndpointsQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var endpoints = await _unitOfWork.EndpointsRepository.GetAllEndpoints();
            if ( endpoints == null || endpoints.Count() == 0 )
                throw new BusinessException(ApiConstants.Messages.DataEmptyError);
            if ( filters.Id != null && (int)filters.Id != 0 )
                endpoints = endpoints.Where(e => e.Id.Equals(filters.Id)).ToList();
            if ( !string.IsNullOrEmpty(filters.Nombre) )
                endpoints = endpoints.Where(e => e.Nombre.Contains(filters.Nombre, StringComparison.OrdinalIgnoreCase)).ToList();
            if ( !string.IsNullOrEmpty(filters.Controlador) )
                endpoints = endpoints.Where(e => e.Controlador.Contains(filters.Controlador, StringComparison.OrdinalIgnoreCase)).ToList();
            if ( !string.IsNullOrEmpty(filters.Metodo) )
                endpoints = endpoints.Where(e => e.Metodo.Contains(filters.Metodo, StringComparison.OrdinalIgnoreCase)).ToList();
            if ( !string.IsNullOrEmpty(filters.HttpVerbo) )
                endpoints = endpoints.Where(e => e.HttpVerbo.Contains(filters.HttpVerbo, StringComparison.OrdinalIgnoreCase)).ToList();
            if ( filters.IsActive != null )
                endpoints = endpoints.Where(e => e.Estado.Equals(filters.IsActive)).ToList();

            if ( endpoints == null || endpoints.Count() == 0 )
                throw new BusinessException(ApiConstants.Messages.DataByfilterNotFound);
            var endpointsDtos = _mapper.Map<IEnumerable<EndpointsDto>>(endpoints).ToList();

            var pagedEndpoints = PagedList<EndpointsDto>.Create(endpointsDtos, filters.PageNumber, filters.PageSize);
            return pagedEndpoints;
        }

        /// <summary>
        /// Funcion para insertar un nuevo Endpoint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<EndpointsDto> InsertEndpoint(CreateEndpointRequest request)
        {
            var filters = new EndpointsQueryFilter();
            filters.Nombre = request.Nombre;
            var isExists = _unitOfWork.EndpointsRepository.GetAllEndpoints().Result.Where(e => e.Nombre.Equals(request.Nombre, StringComparison.OrdinalIgnoreCase) && e.Metodo.Equals(request.Metodo, StringComparison.OrdinalIgnoreCase));
            if ( isExists != null && isExists.Count() > 0 )
                throw new BusinessException(ApiConstants.Messages.ExistInDB);
            var endpoint = _mapper.Map<Endpoints>(request);
            endpoint.HttpVerbo = endpoint.HttpVerbo.ToUpper();
            endpoint.Estado = true;
            var created = await _unitOfWork.EndpointsRepository.AddEndpoint(endpoint);
            await _unitOfWork.SaveChangesAsync();

            var endpointCreated = _mapper.Map<EndpointsDto>(created);

            return endpointCreated;
        }

        /// <summary>
        /// Funcion para actualizar un Endpoint por su Sucursal
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateEndpoint(UpdateEndpointRequest request)
        {
            var existingEndpoint = await _unitOfWork.EndpointsRepository.GetById(request.Id);
            if ( existingEndpoint == null || existingEndpoint.Id == 0 )
                throw new BusinessException(ApiConstants.Messages.InvalidId);
            existingEndpoint.Nombre = request.Nombre;
            existingEndpoint.Controlador = request.Controlador;
            existingEndpoint.Metodo = request.Metodo;
            existingEndpoint.HttpVerbo = request.HttpVerbo.ToUpper();
            existingEndpoint.Estado = request.Estado != null ? (bool)request.Estado : existingEndpoint.Estado;

            _unitOfWork.EndpointsRepository.Update(existingEndpoint);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Funcion para Desactivar un Endpoint por su Sucursal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteEndpoint(int id)
        {
            var existingEndpoint = await _unitOfWork.EndpointsRepository.GetById(id);
            if ( existingEndpoint == null || existingEndpoint.Id == 0 )
                throw new BusinessException(ApiConstants.Messages.InvalidId);
            existingEndpoint.Estado = false;
            _unitOfWork.EndpointsRepository.Update(existingEndpoint);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Funcion para agregar permisos a un Endpoint por su Sucursal
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddEndpointPermiso(CreateOrUpdateEndpointPermisoRequest request)
        {
            var item = new EndpointsPermisos()
            {
                Id = request.Id,
                EndpointId = request.EndpointId,
                PermisoId = request.PermisoId,
                Estado = request.Estado,
            };

            var existingEndpoint = await _unitOfWork.EndpointsRepository.GetById(request.EndpointId);
            if ( existingEndpoint == null || existingEndpoint.Id == 0 )
                throw new BusinessException(ApiConstants.Messages.InvalidId);
            existingEndpoint.EndpointsPermisos = item;
            _unitOfWork.EndpointsRepository.Update(existingEndpoint);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
