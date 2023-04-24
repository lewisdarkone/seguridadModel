using com.softpine.muvany.models.Requests;
using FluentValidation;

namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// 
/// </summary>
public class DeleteRolePermissionsRequestCopyValidator : CustomValidator<DeleteRolePermissionsRequest>
{
    /// <summary>
    /// 
    /// </summary>
    public DeleteRolePermissionsRequestCopyValidator()
    {
        RuleFor(r => r.RoleId)
            .NotEmpty();
        RuleFor(r => r.Permisos)
            .NotNull();
    }
}
