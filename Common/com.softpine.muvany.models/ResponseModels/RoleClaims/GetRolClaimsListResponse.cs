

using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.Entities.EntitiesIdentity;

namespace com.softpine.muvany.models.ResponseModels.RoleClaims;

public class GetRolClaimsListResponse : BaseResponse
{
    public GetRolClaimsListResponseData? Data { get; set; }
    public ExceptionResponse? Exception { get; set; }

}
public class GetRolClaimsListResponseData
{
    public ICollection<GetRolClaimsListModel>? Data { get; set; }
    public Metadata? Meta { get; set; }

}
public class GetRolClaimsListModel
{
    public int Id { get; set; }
    public string? RoleId { get; set; }
    public string? ClaimType { get; set; }
    public string? ClaimValue { get; set; }
    public int PermisoId { get; set; }
    public Permisos? Permiso { get; set; }
}
