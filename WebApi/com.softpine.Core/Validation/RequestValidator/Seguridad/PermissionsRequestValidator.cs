using com.softpine.muvany.models.Request.RequestsIdentity;
using FluentValidation;

namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// 
/// </summary>
public class PermissionsRequestValidator : CustomValidator<PermissionsRequest>
{
    /// <summary>
    /// 
    /// </summary>
    public PermissionsRequestValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty()
            .NotNull();
    }
}
