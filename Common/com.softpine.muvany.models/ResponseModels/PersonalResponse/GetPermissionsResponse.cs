

using com.softpine.muvany.models.CustomEntities;

namespace com.softpine.muvany.models.ResponseModels.PersonalResponse
{
    public class GetPermissionsResponse : BaseResponse
    {
        public GetPermissionsData? Data { get; set; }
        public ExceptionResponse? Exception { get; set; }
    }

    public class GetPermissionsData 
    {
        public ICollection<PermissionData> Data { get; set; }
        public Metadata? Meta { get; set; }

    }
    public class PermissionData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? TypeRole { get; set; }
        public string? TypeRoleDescription { get; set; }
        public ICollection<PermisionSubData>? Permisos { get; set; }
    }
    public class PermisionSubData
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
    }

}
