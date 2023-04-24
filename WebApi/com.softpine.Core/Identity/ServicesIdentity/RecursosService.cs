using AutoMapper;
using Microsoft.Extensions.Options;
using com.softpine.muvany.core.Exceptions;
using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.Constants;
using com.softpine.muvany.models.Entities.EntitiesIdentity;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.Enumerations;
using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.DTOS;

namespace com.softpine.muvany.core.Identity.ServicesIdentity;

/// <summary>
/// Servicio para el mantenimiento para las Recursos
/// </summary>
public class RecursosService : IRecursosService
{
    private readonly IUserService userService;
    private readonly IBaseRepositoryDapper repositoryDapper;
    private readonly IUnitOfWorkIdentity unitOfWork;
    private readonly IMapper mapper;
    private readonly PaginationOptions _paginationOptions;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userService"></param>
    /// <param name="repositoryDapper"></param>
    /// <param name="unitOfWork"></param>
    /// <param name="mapper"></param>
    /// <param name="options"></param>
    public RecursosService(IUserService userService, IBaseRepositoryDapper repositoryDapper, IUnitOfWorkIdentity unitOfWork, IMapper mapper, IOptions<PaginationOptions> options)
    {
        this.userService = userService;
        this.repositoryDapper = repositoryDapper;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        _paginationOptions = options.Value;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<ModulosDto>> GetRecursosWithIdUser()
    {
        string idUsuario = userService.GetUserLoginId().Result;

        if ( string.IsNullOrEmpty(idUsuario) )
            throw new BusinessException(ApiConstants.Messages.AuthenticationFailed);

        //var recursos = await repositoryDapper.QueryTemplate<Recursos>(
        //    @"select distinct re.* from [dbo].[Tbl_Seg_Permisos] pe 
        //    inner join[dbo].[Tbl_Seg_Recursos] re on re.Id = pe.IdRecurso
        //    inner join[dbo].[Tbl_Seg_Acciones] ac on ac.Id = pe.IdAccion
        //    inner join[dbo].[Tbl_Seg_RolesClaims] rc on rc.ClaimValue like '%permisos.' + re.Nombre + '.' + ac.Nombre + '%'
        //    inner join[dbo].[Tbl_Seg_UsuarioRoles] ur on ur.RoleId = rc.RoleId
        //    inner join[dbo].[Tbl_Seg_Usuarios] us on us.Id = ur.UserId
        //    where re.EsMenuConfiguracion = 1 and ac.Id = 11 and re.Estado = 1 
        //    and us.Id = '" + idUsuario + "'");
        var recursos = await repositoryDapper.QueryTemplate<Recursos>(
            @"select distinct re.* from Tbl_Seg_Permisos pe 
            inner join Tbl_Seg_Recursos re on re.Id = pe.IdRecurso
            inner join Tbl_Seg_Acciones ac on ac.Id = pe.IdAccion
            inner join Tbl_Seg_RolesClaims rc on rc.ClaimValue like concat('%permisos.',re.Nombre , '.', ac.Nombre , '%')
            inner join Tbl_Seg_UsuarioRoles ur on ur.RoleId = rc.RoleId
            inner join Tbl_Seg_Usuarios us on us.Id = ur.UserId
            where re.EsMenuConfiguracion = 1
            and re.Estado = 1
            and us.Id = '"+idUsuario+"'");


        if ( recursos == null )
            throw new BusinessException(ApiConstants.Messages.DataEmptyError);
        //var modulos = unitOfWork.ModulosRepository.GetAll().Result;

        //var reponse = modulos.Join(recursos.DistinctBy(d => d.IdModulo), mo => mo.Sucursal, re => re.IdModulo, (mo, re) => new ModulosDto
        //{
        //    Sucursal = mo.Sucursal,
        //    Nombre = mo.Nombre,
        //    ModuloPadre = mo.ModuloPadre,
        //    Recursos = (ICollection<ModulosDto>)recursos
        //    .Where(r => r.IdModulo == mo.Sucursal).Select(t =>
        //    new ModulosDto { Sucursal = t.Sucursal, Nombre = t.DescripcionMenuConfiguracion }
        //    ).ToList(),
        //}).ToList();
        var reponse = recursos.OrderBy(p => p.OrdenMenu).Select(p => new ModulosDto { Id = p.Id, ModuloPadre = p.IdModulo, Nombre = p.DescripcionMenuConfiguracion, URL = p.Url }).ToList();

        if ( reponse.DistinctBy(o => o.ModuloPadre).Where(p => p.ModuloPadre != null && p.ModuloPadre > 0).Count() > 0 )
        {
            reponse = Menu(reponse);
        }

        return reponse;


    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private List<ModulosDto> Menu(List<ModulosDto> menu)
    {

        var result = new List<ModulosDto>();
        //obtener modulos
        var modulos = unitOfWork.ModulosRepository.GetAll().Result;
        // busca los id ModulosPadres sin repetir que estan dentro de la lista que son hijos menu.
        var me = menu.Where(p => p.ModuloPadre != null).DistinctBy(j => j.ModuloPadre).Select(q => q.ModuloPadre).ToList();
        // filtra los modulos con los ModulosPadres anteriores.
        var moPadres = modulos.OrderBy(p => p.OrdenMenu).Where(p => me.Contains(p.Id));
        foreach ( var item in moPadres )
        {
            if ( item.Estado == (int)EstadosEnum.Activo )
            {
                // filtra los elmentos que pertenecen al item(ModuloPadre)
                var menuHijo = menu.Where(p => p.ModuloPadre == item.Id).ToList();
                // filtra los elementos por id y nombre (ModuloPdre) para extraer los hijos
                var HijoExistentes = menu.FirstOrDefault(p => p.Id == item.Id && p.Nombre == item.Nombre);
                // si existe un elemento igual al ModuloPadre extrae los hijos y unelos a los elemento que pertenecen al item(ModuloPadre)
                if ( HijoExistentes != null )
                {
                    menuHijo.AddRange(HijoExistentes.Recursos);
                    menu.Remove(HijoExistentes);
                }
                // agrega los elementos hijos al item(ModuloPadre) y aqregalo a la lista del menu
                result.Add(new ModulosDto
                {
                    Id = item.Id,
                    Nombre = item.Nombre,
                    ModuloPadre = item.ModuloPadre,
                    Cssicon = item.Cssicon,
                    Recursos = (ICollection<ModulosDto>)menuHijo
                });
            }
        }
        // agrega los modulos que no tienen ModulosPadres a la lista menu
        result.AddRange(menu.Where(p => p.ModuloPadre == null));
        // verificar si existe un elemento con ModuloPadre 
        if ( result.DistinctBy(o => o.ModuloPadre).Where(p => p.ModuloPadre != null && p.ModuloPadre > 0).Count() > 0 )
        {
            // si exite Modulo padre se recrea llamando a la misma funcion
            result = Menu(result);
        }
        // retorna el menu
        return result;
    }

    /// <summary>
    /// Obtener las Recursos 
    /// </summary>
    /// <param name="filter"> opciones para filtrar la obtencion de datos</param>
    /// <returns> Retorna los datos paginados</returns>
    public async Task<PagedList<RecursosDto>> GetAllAsync(RecursosQueryFilter filters)
    {


        filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
        filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

        var recursosGet = await unitOfWork.Recursos.GetAllRecursos();
        if ( recursosGet == null )
            throw new BusinessException(ApiConstants.Messages.DataEmptyError);

        recursosGet = recursosGet.Where(f => f.Estado.Equals((int)EstadosEnum.Activo));

        List<Recursos> recursos = new List<Recursos>();
        if ( filters.NombreModulo != null || filters.Nombre != null )
        {
            if ( filters.NombreModulo != null && filters.NombreModulo.Trim().Length > 0 )
            {
                recursos.AddRange(recursosGet.Where(f => f.IdModuloNavigation.Nombre.Contains(filters.NombreModulo, StringComparison.OrdinalIgnoreCase)).ToList());
            }


            if ( filters.Nombre != null && filters.Nombre.Trim().Length > 0 )
            {
                recursos.AddRange(recursosGet.Where(f => f.Nombre.Contains(filters.Nombre, StringComparison.OrdinalIgnoreCase)));
            }
        }
        else
        {
            recursos = recursosGet.ToList();
        }

        if ( filters.Id != null && filters.Id != 0 )
        {
            recursos = recursos.Where(f => f.Id.Equals(filters.Id)).ToList();
        }


        if ( filters.IdModulo != null && filters.IdModulo != 0 )
        {
            recursos = recursos.Where(f => f.IdModulo.Equals(filters.IdModulo)).ToList();
        }


        if ( filters.EsMenuConfiguracion != null && filters.EsMenuConfiguracion != 0 )
        {
            recursos = recursos.Where(f => f.EsMenuConfiguracion.Equals(filters.EsMenuConfiguracion)).ToList();
        }


        if ( filters.DescripcionMenuConfiguracion != null
            && filters.DescripcionMenuConfiguracion.Trim().Length > 0 )
        {
            recursos = recursos.Where(f => f.DescripcionMenuConfiguracion.Contains(filters.DescripcionMenuConfiguracion, StringComparison.OrdinalIgnoreCase)).ToList();
        }


        var reponse = mapper.Map<IEnumerable<RecursosDto>>(recursos);



        var pagedRecursos = PagedList<RecursosDto>.Create(reponse, filters.PageNumber, filters.PageSize);
        return pagedRecursos;


    }

    /// <summary>
    /// Elimina una Recursos
    /// </summary>
    /// <param name="id">El Sucursal que de desea eliminar</param>
    /// <returns>true o false</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await unitOfWork.Recursos.GetById(id);

        if ( existing == null )
            throw new BusinessException(ApiConstants.Messages.DeletionNotAllowed);

        existing.Estado = (int)EstadosEnum.Eliminado;

        unitOfWork.Recursos.Update(existing);
        await unitOfWork.SaveChangesAsync();

        return true;

    }

    /// <summary>
    /// Inserta una nueva OpcionesCondicionesComponentesVehiculo
    /// </summary>
    /// <param name="request">Objeto OpcionesCondicionesComponentesVehiculo que va a ser agregado</param>
    /// <returns>true o false</returns>
    public async Task<RecursosDto> CreateAsync(CreateRecursosRequest request)
    {


        var param = new Recursos
        {
            Nombre = request.Nombre,
            Estado = (int)EstadosEnum.Activo,
            IdModulo = request.IdModulo,
            EsMenuConfiguracion = request.EsMenuConfiguracion,
            DescripcionMenuConfiguracion = request.DescripcionMenuConfiguracion, 
            Url = request.Url
        };
        if ( request != null )
        {
            await unitOfWork.Recursos.Add(param);
            await unitOfWork.SaveChangesAsync();
        }


        return mapper.Map<RecursosDto>(param);
    }

    /// <summary>
    /// Actualiza una Recursos
    /// </summary>
    /// <param name="request">Objeto Recursos que va a ser agregado</param>
    /// <returns>true o false</returns>
    public async Task<bool> UpdateAsync(UpdateRecursosRequest request)
    {
        var existing = await unitOfWork.Recursos.GetById((int)request.Id);

        if ( existing == null )
            throw new BusinessException(ApiConstants.Messages.UpdateNotAllowed);

        existing.Nombre = request.Nombre != null ? request.Nombre : existing.Nombre;
        existing.IdModulo = request.IdModulo != null ? (int)request.IdModulo : existing.IdModulo;
        existing.EsMenuConfiguracion = request.EsMenuConfiguracion != null ? (int)request.EsMenuConfiguracion : existing.EsMenuConfiguracion;
        existing.DescripcionMenuConfiguracion = request.DescripcionMenuConfiguracion != null ? request.DescripcionMenuConfiguracion : existing.DescripcionMenuConfiguracion;
        existing.Url = request.Url != null ? request.Url : existing.Url;

        unitOfWork.Recursos.Update(existing);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

}




