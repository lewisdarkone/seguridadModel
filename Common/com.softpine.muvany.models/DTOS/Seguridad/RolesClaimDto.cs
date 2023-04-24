

using com.softpine.muvany.models.Request.RequestsIdentity;

namespace com.softpine.muvany.models.DTOS
{
    /// <summary>
    /// 
    /// </summary>
    public class RolesClaimDto : RoleDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<PermissionsRequest> Permisos { get; set; }

    }
}
