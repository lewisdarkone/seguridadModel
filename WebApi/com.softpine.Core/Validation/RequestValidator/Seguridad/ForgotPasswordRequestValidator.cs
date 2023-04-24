using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.models.Requests;
using FluentValidation;

namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// 
/// </summary>
public class ForgotPasswordRequestValidator : CustomValidator<ForgotPasswordRequest>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userService"></param>
    public ForgotPasswordRequestValidator(IUserService userService) =>
        RuleFor(p => p.Email).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress()
                .WithMessage("Dirección de email inválida")
            .MustAsync(async (email, _) => await userService.ExistsWithEmailAsync(email))
                .WithMessage((_, email) => $"El email {email} no está registrado.");
}
