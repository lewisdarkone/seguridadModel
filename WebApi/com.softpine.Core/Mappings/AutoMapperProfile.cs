using AutoMapper;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Entities;
using com.softpine.muvany.models.Entities.EntitiesIdentity;
using com.softpine.muvany.models.Entities.Subscripcion;
using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.Requests.SuscripcionCatalog;

namespace com.softpine.muvany.core.Mappings
{
    /// <summary>
    /// Clase para el Mapeo de las clases
    /// </summary>
    public class AutoMapperProfile : Profile
    {


        
        /// <summary>
        /// 
        /// </summary>
        public AutoMapperProfile()
        {
            // addMapper
            #region Generales
            CreateMap<ReferenciaUsuarios, UserBasicInfo>()
                .ReverseMap();
            #endregion
            #region Suscripciones
            CreateMap<CreateSuscripcionCatalogRequest, SuscripcionCatalog>()
                .ReverseMap();
            #endregion

            CreateMap<Modulos, ModulosDto>()
                .ForMember(dest => dest.Recursos, op => op.Ignore())
                .ForMember(dest => dest.NombreModuloPadre, source => source.MapFrom(source => source.ModuloPadreNav != null ? source.ModuloPadreNav.Nombre: null))
                .ReverseMap();

            CreateMap<Acciones, AccionesDto>().ReverseMap();

            CreateMap<Recursos, RecursosDto>()
                .ForMember(dest => dest.NombreModulo, source => source.MapFrom(sourc => sourc.IdModuloNavigation.Nombre))
                .ReverseMap();

            //Endpoints Maps
            CreateMap<Endpoints, CreateEndpointRequest>()
                .ReverseMap();

            //Endpoints Maps
            CreateMap<Endpoints, EndpointsDto>()
                 .ForMember(dest => dest.Permiso, source => source.MapFrom(source => source.EndpointsPermisos != null ? source.EndpointsPermisos.Permiso : null))
                .ReverseMap();

            //Permisos Maps
            CreateMap<Permisos, PermisosDto>()
                 .ForMember(dest => dest.AccionNombre, source => source.MapFrom(source => source.IdAccionNavigation.Nombre))
                 .ForMember(dest => dest.RecursoNombre, source => source.MapFrom(source => source.IdRecursoNavigation.Nombre))
                .ReverseMap();


        }
    }
}
