using com.softpine.muvany.core.Interfaces;
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
public class ResetPasswordRequestValidator : CustomValidator<ResetPasswordRequest>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userService"></param>
    public ResetPasswordRequestValidator(IUserService userService)
    {
        RuleFor(u => u.Email).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress()
                .WithMessage("Dirección de email inválida.")
            .MustAsync(async (email, _) => await userService.ExistsWithEmailAsync(email))
                .WithMessage((_, email) => $"El email {email} no está registrado.");

        RuleFor(p => p.Password).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(8)
            .Matches(@"[A-Z,a-z]+")
            .Matches(@"[0-9]+")
            .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]")
            .WithMessage((_, email) => $"La contraseña no cumple el formato requerido");

        RuleFor(u => u.ConfirmPassword)
            .NotEmpty()
            .Equal(x => x.Password);

        RuleFor(p => p.CodigoValidacion).Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}
