using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.models.Requests;
using FluentValidation;

namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// 
/// </summary>
public class CreateRoleRequestValidator : CustomValidator<CreateRoleRequest>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="roleService"></param>
    public CreateRoleRequestValidator(IRoleService roleService) =>
        RuleFor(r => r.Name)
            .NotEmpty()
            .MinimumLength(3);
}
