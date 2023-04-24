using com.softpine.muvany.models.CustomEntities;

namespace com.softpine.muvany.models.ResponseModels.RoleClaims
{
    public class GetRoleClaimsPermissionResponse : BaseResponse 
    {
        public GetRoleClaimsPermisionData? Data { get; set; }
        public ExceptionResponse? Exception { get; set; }
    }

    public class GetRoleClaimsPermisionData 
    {
        public Data? Data { get; set; }
        public Metadata? Meta { get; set; }
    }

    public class Data
    {
        //public string? Id { get; set; }
        //public string? name { get; set; }
        //public string? description { get; set; }
        //public int? typeRole { get; set; }
        //public string? typeRoleDescription { get; set; }
        public ICollection<RolePermisoModel>? Permisos { get; set; }

    }

    /// <summary>
    /// Representacion de Permisos en GetRoleClaimsPermission
    /// </summary>
    public class RolePermisoModel
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
    }
}
