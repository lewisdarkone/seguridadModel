using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.models.Requests;
using FluentValidation;

namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// 
/// </summary>
public class UpdateRoleRequestValidator : CustomValidator<UpdateRoleRequest>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="roleService"></param>
    public UpdateRoleRequestValidator(IRoleService roleService) =>
        RuleFor(r => r.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MustAsync(async (role, name, _) => !await roleService.ExistsAsync(name, role.Id))
                .WithMessage("Similar Role already exists.");

}
