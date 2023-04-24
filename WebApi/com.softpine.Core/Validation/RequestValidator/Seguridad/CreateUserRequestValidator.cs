using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.models.Requests;
using FluentValidation;

namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// 
/// </summary>
/// <param name="userService"></param>
/// 
public class CreateUserRequestValidator : CustomValidator<CreateUserRequest>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userService"></param>
    public CreateUserRequestValidator(IUserService userService)
    {
        RuleFor(u => u.Email).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Dirección de correo invalida.")
            .EmailAddress()
                .WithMessage("Dirección de correo invalida.")
            .MustAsync(async (email, _) => !await userService.ExistsWithEmailAsync(email))
                .WithMessage((_, email) => $"Email {email} ya esta registrado.");

        RuleFor(u => u.PhoneNumber).Cascade(CascadeMode.Stop)
            //.Length(10)
            //.WithMessage("El tamaño del telefono debe ser igual 10 números")
            .MustAsync(async (phone, _) => await userService.IsPhoneNumber(phone!))
              .WithMessage("Numero de telefono invalido.")
            ;

        RuleFor(p => p.FullName).Cascade(CascadeMode.Stop)
            .NotEmpty();

    }
}
