using AutoMapper;
using Microsoft.Extensions.Options;
using com.softpine.muvany.core.Exceptions;
using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.Constants;
using com.softpine.muvany.models.Enumerations;
using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.Entities.EntitiesIdentity;

namespace com.softpine.muvany.core.Identity.ServicesIdentity;

/// <summary>
/// Servicio para el mantenimiento para las Acciones
/// </summary>
public class AccionesService : IAccionesService
{
    private readonly IUnitOfWorkIdentity unitOfWork;
    private readonly IMapper mapper;
    private readonly PaginationOptions _paginationOptions;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="unitOfWork"></param>
    /// <param name="mapper"></param>
    public AccionesService(IUnitOfWorkIdentity unitOfWork, IMapper mapper, IOptions<PaginationOptions> options)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        _paginationOptions = options.Value;
    }
    /// <summary>
    /// Obtener las Acciones 
    /// </summary>
    /// <param name="filter"> opciones para filtrar la obtencion de datos</param>
    /// <returns> Retorna los datos paginados</returns>
    public async Task<PagedList<AccionesDto>> GetAllAsync(AccionesQueryFilter filters)
    {


        filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
        filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

        var acciones = await unitOfWork.Acciones.GetAll();
        if ( acciones == null )
            throw new BusinessException(ApiConstants.Messages.DataEmptyError);

        acciones = acciones.Where(f => f.Estado.Equals((int)EstadosEnum.Activo));

        if ( filters.Id != null && filters.Id != 0 )
        {
            acciones = acciones.Where(f => f.Id.Equals(filters.Id));
        }


        if ( filters.Nombre != null && filters.Nombre.Trim().Length > 0 )
        {
            acciones = acciones.Where(f => f.Nombre.Contains(filters.Nombre, StringComparison.OrdinalIgnoreCase));
        }






        var reponse = mapper.Map<IEnumerable<AccionesDto>>(acciones);


        var pagedAcciones = PagedList<AccionesDto>.Create(reponse, filters.PageNumber, filters.PageSize);
        return pagedAcciones;


    }

    /// <summary>
    /// Elimina una Acciones
    /// </summary>
    /// <param name="id">El Id que de desea eliminar</param>
    /// <returns>true o false</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await unitOfWork.Acciones.GetById(id);

        if ( existing == null )
            throw new BusinessException(ApiConstants.Messages.DeletionNotAllowed);

        existing.Estado = (int)EstadosEnum.Eliminado;

        unitOfWork.Acciones.Update(existing);
        await unitOfWork.SaveChangesAsync();

        return true;

    }

    /// <summary>
    /// Inserta una nueva OpcionesCondicionesComponentesVehiculo
    /// </summary>
    /// <param name="request">Objeto OpcionesCondicionesComponentesVehiculo que va a ser agregado</param>
    /// <returns>true o false</returns>
    public async Task<AccionesDto> CreateAsync(CreateAccionesRequest request)
    {


        var param = new Acciones
        {
            Nombre = request.Nombre,
            Estado = (int)EstadosEnum.Activo
        };
        if ( request != null )
        {
            unitOfWork.Acciones.Add(param);
            await unitOfWork.SaveChangesAsync();
        }


        return mapper.Map<AccionesDto>( param );
    }

    /// <summary>
    /// Actualiza una Acciones
    /// </summary>
    /// <param name="request">Objeto Acciones que va a ser agregado</param>
    /// <returns>true o false</returns>
    public async Task<bool> UpdateAsync(UpdateAccionesRequest request)
    {
        var existing = await unitOfWork.Acciones.GetById((int)request.Id);

        if ( existing == null )
            throw new BusinessException(ApiConstants.Messages.UpdateNotAllowed);

        existing.Nombre = (request.Nombre != null) ? request.Nombre : existing.Nombre;

        unitOfWork.Acciones.Update(existing);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

}




