using com.softpine.muvany.models.Requests;
using FluentValidation;

namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// 
/// </summary>
public class UpdateRolePermissionsByIdRequestValidator : CustomValidator<UpdateRolePermissionsByIdRequest>
{
    /// <summary>
    /// 
    /// </summary>
    public UpdateRolePermissionsByIdRequestValidator()
    {
        RuleFor(r => r.RoleId)
            .NotEmpty();
        RuleFor(r => r.Permissions)
            .NotNull();
    }
}
