using AutoMapper;
using com.softpine.muvany.component.Forms;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Entities;
using com.softpine.muvany.models.Entities.EntitiesIdentity;
using com.softpine.muvany.models.Entities.Subscripcion;
using com.softpine.muvany.models.Request.RequestsIdentity;
using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.Requests.SuscripcionCatalog;

namespace com.softpine.muvany.core.Mappings
{
    /// <summary>
    /// Clase para el Mapeo de las clases
    /// </summary>
    public class AutoMapperProfileClient : Profile
    {


        
        /// <summary>
        /// 
        /// </summary>
        public AutoMapperProfileClient()
        {
            // addMapper
            #region Generales

            //Recursos
            CreateMap<RecursosDto, EditCreateRecursosForm>()
                .ReverseMap();
            CreateMap<CreateRecursosRequest, EditCreateRecursosForm>()
                .ReverseMap();
            CreateMap<UpdateRecursosRequest, EditCreateRecursosForm>()
                .ReverseMap();

            //Modulos
            CreateMap<ModulosDto, EditCreateModulosForm>()
                .ReverseMap();
            CreateMap<CreateModulosRequest, EditCreateModulosForm>()
                .ReverseMap();
            CreateMap<UpdateModulosRequest, EditCreateModulosForm>()
                .ReverseMap();

            //Acciones
            CreateMap<AccionesDto, EditCreateAccionesForm>()
                .ReverseMap();
            CreateMap<CreateAccionesRequest, EditCreateAccionesForm>()
                .ReverseMap();
            CreateMap<UpdateAccionesRequest, EditCreateAccionesForm>()
                .ReverseMap();
            //Endpoints
            CreateMap<EndpointsDto, EditCreateEndpointForm>()
                .ReverseMap();
            CreateMap<CreateEndpointRequest, EditCreateEndpointForm>()
                .ReverseMap();
            CreateMap<UpdateEndpointRequest, EditCreateEndpointForm>()
                .ReverseMap();
            //Endpoints
            CreateMap<UserDetailsDto, CreateEditUserForm>()
                .ReverseMap();
            CreateMap<UpdateUserRequest, CreateEditUserForm>()
                .ReverseMap();
            CreateMap<CreateUserRequest, CreateEditUserForm>()
                .ReverseMap();

            //User Profile
            CreateMap<UserDetailsDto, UserProfileForm>()
                .ReverseMap();
            CreateMap<UpdateUserProfileRequest, UserProfileForm>()
                .ReverseMap();
            #endregion





        }
    }
}
