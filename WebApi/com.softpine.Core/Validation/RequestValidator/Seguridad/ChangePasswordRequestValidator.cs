using com.softpine.muvany.models.Requests;
using FluentValidation;

namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// 
/// </summary>
internal class ChangePasswordRequestValidator : CustomValidator<ChangePasswordRequest>
{
    /// <summary>
    /// 
    /// </summary>
    public ChangePasswordRequestValidator()
    {
        RuleFor(p => p.Password)
            .NotEmpty()
            .MinimumLength(6)
            .Matches(@"[A-Z,a-z]+")
            .Matches(@"[0-9]+")
            .Matches(@"[\!\?\*\.\-]+");

        RuleFor(p => p.NewPassword)
            .NotEmpty()
            .MinimumLength(6)
            .Matches(@"[A-Z,a-z]+")
            .Matches(@"[0-9]+")
            .Matches(@"[\!\?\*\.\-]+");

        RuleFor(p => p.ConfirmNewPassword)
            .Equal(p => p.NewPassword)
                .WithMessage("La nueva contraseña y la contraseña de confirmación no coinciden.");

    }
}
