using AutoMapper;
using Microsoft.Extensions.Options;
using com.softpine.muvany.core.Exceptions;
using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.Constants;
using com.softpine.muvany.models.Enumerations;
using com.softpine.muvany.models.Request.RequestsIdentity;
using com.softpine.muvany.models.Entities.EntitiesIdentity;
using com.softpine.muvany.models.DTOS;
using Microsoft.EntityFrameworkCore;

namespace com.softpine.muvany.core.Identity.ServicesIdentity;

/// <summary>
/// Servicio para el mantenimiento para las Modulos
/// </summary>
public class ModulosService : IModulosService
{
    private readonly IUnitOfWorkIdentity unitOfWork;
    private readonly IMapper mapper;
    private readonly PaginationOptions _paginationOptions;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="unitOfWork"></param>
    /// <param name="mapper"></param>
    public ModulosService(IUnitOfWorkIdentity unitOfWork, IMapper mapper, IOptions<PaginationOptions> options)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        _paginationOptions = options.Value;
    }
    /// <summary>
    /// Obtener las Modulos 
    /// </summary>
    /// <param name="filters"> opciones para filtrar la obtencion de datos</param>
    /// <returns> Retorna los datos paginados</returns>
    public async Task<PagedList<ModulosDto>> GetAllAsync(ModulosQueryFilter filters)
    {


        filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
        filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

        var modulos = await unitOfWork.ModulosRepository.GetAll();
        if ( modulos == null )
            throw new BusinessException(ApiConstants.Messages.DataEmptyError);


        if ( filters.Id != null && filters.Id != 0 )
        {
            modulos = modulos.Where(f => f.Id.Equals(filters.Id));
        }

        if ( filters.Nombre != null && filters.Nombre.Trim().Length > 0 )
        {
            modulos = modulos.Where(f => f.Nombre.Contains(filters.Nombre, StringComparison.OrdinalIgnoreCase));
        }

        if ( filters.Estado != null)
        {
            modulos = modulos.Where(f => f.Estado.Equals(filters.Estado));
        }

        //loading references
        modulos.ToList();

        var reponse = mapper.Map<IEnumerable<ModulosDto>>(modulos);


        var pagedModulos = PagedList<ModulosDto>.Create(reponse, filters.PageNumber, filters.PageSize);
        
        return pagedModulos;


    }

    /// <summary>
    /// Elimina una Modulos
    /// </summary>
    /// <param name="id">El Sucursal que de desea eliminar</param>
    /// <returns>true o false</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await unitOfWork.ModulosRepository.GetById(id);

        if ( existing == null )
            throw new BusinessException(ApiConstants.Messages.DeletionNotAllowed);

        existing.Estado = (int)EstadosEnum.Eliminado;

        unitOfWork.ModulosRepository.Update(existing);
        await unitOfWork.SaveChangesAsync();

        return true;

    }

    /// <summary>
    /// Inserta una nueva Modulos
    /// </summary>
    /// <param name="request">Objeto Modulos que va a ser agregado</param>
    /// <returns>true o false</returns>
    public async Task<ModulosDto> CreateAsync(CreateModulosRequest request)
    {


        var param = new Modulos
        {
            Nombre = request.Nombre,
            ModuloPadre = request.ModuloPadre,
            Estado = (int)EstadosEnum.Activo,
            Cssicon = request.Cssicon,
        };
        if ( request != null )
        {
            await unitOfWork.ModulosRepository.Add(param);
            await unitOfWork.SaveChangesAsync();
        }


        return mapper.Map<ModulosDto>(param);
    }

    /// <summary>
    /// Actualiza una Modulos
    /// </summary>
    /// <param name="request">Objeto Modulos que va a ser actuaizados</param>
    /// <returns>true o false</returns>
    public async Task<bool> UpdateAsync(UpdateModulosRequest request)
    {
        var existing = await unitOfWork.ModulosRepository.GetById((int)request.Id);

        if ( existing == null )
            throw new BusinessException(ApiConstants.Messages.UpdateNotAllowed);

        existing.Nombre = request.Nombre != null ? request.Nombre : existing.Nombre;

        existing.ModuloPadre = request.ModuloPadre != null ? request.ModuloPadre.Value : existing.ModuloPadre;

        existing.Estado = request.Estado != null ? request.Estado : existing.Estado;

        existing.Cssicon = request.Cssicon != null ? request.Cssicon : existing.Cssicon;

        unitOfWork.ModulosRepository.Update(existing);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

}




