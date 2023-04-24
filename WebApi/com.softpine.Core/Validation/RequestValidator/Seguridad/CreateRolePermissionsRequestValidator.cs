using com.softpine.muvany.models.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// 
/// </summary>
public class CreateRolePermissionsRequestValidator : CustomValidator<CreateRolePermissionsRequest>
{
    /// <summary>
    /// 
    /// </summary>
    public CreateRolePermissionsRequestValidator()
    {
        RuleFor(r => r.RoleId)
            .NotEmpty();
        RuleFor(r => r.Permissions)
            .NotNull();
    }
}
