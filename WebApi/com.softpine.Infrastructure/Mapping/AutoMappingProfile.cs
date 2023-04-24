using AutoMapper;
using com.softpine.muvany.infrastructure.Identity.Entities;
using com.softpine.muvany.models.DTOS;

namespace com.softpine.muvany.infrastructure.Mapping
{
    /// <summary>
    /// Clase para el Mapeo de Entidades
    /// </summary>
    public class AutoMappingProfile : Profile
    {
        /// <summary>
        /// Contructor para la ejecucción de los Mapeos
        /// </summary>
        public AutoMappingProfile()
        {
            CreateMap<RolesClaimDto, ApplicationRoleClaim>().ReverseMap();
            CreateMap<ApplicationRole, RoleDto>()
                .ForMember(dest => dest.TypeRolDescription, source => source.MapFrom(source => source.TypeRol == 66 ? "Interno" : "Externo"))
                .ReverseMap();

        }
    }
}
