#pragma warning disable 1591
using com.softpine.muvany.models.Entities.EntitiesIdentity;

namespace com.softpine.muvany.models.DTOS
{    
    public class ApplicationRoleClaimDto
    {
        public int? Id { get; set; }
        public string? RoleId { get; set; }
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }
        public int? PermisoId { get; set; }
        public Permisos Permiso { get; set; }
    }

}
