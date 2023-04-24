using com.softpine.muvany.models.CustomEntities;

namespace com.softpine.muvany.models.ResponseModels.RoleClaims;

public class GetRoleClaimsResponse : BaseResponse
{
    public GetRoleClaimsData? Data { get; set; }
    public ExceptionResponse? Exception { get; set; }
}

public class GetRoleClaimsData
{
    public ICollection<RoleClaimsData>? Data { get; set; }
    public Metadata? Meta { get; set; }
}

/// <summary>
/// Entidad que representa objetos a retornar en esta respuesta.
/// </summary>
public class RoleClaimsData
{
    public int Id { get; set; }
    public string? RoleId { get; set; }
    public string? ClaimValue { get; set; }
}
