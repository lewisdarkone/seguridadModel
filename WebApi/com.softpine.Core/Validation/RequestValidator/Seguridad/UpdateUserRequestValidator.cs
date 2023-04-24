using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.models.Requests;
using FluentValidation;

namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// 
/// </summary>
public class UpdateUserRequestValidator : CustomValidator<UpdateUserRequest>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userService"></param>
    public UpdateUserRequestValidator(IUserService userService)
    {
        RuleFor(p => p.Id)
            .NotEmpty();

        RuleFor(p => p.FullName.Trim())
            .NotEmpty()
            .MaximumLength(75);

        RuleFor(p => p.Email)
            .NotEmpty()
            .EmailAddress()
                .WithMessage("Dirección de email inválida.")
            .MustAsync(async (user, email, _) => !await userService.ExistsWithEmailAsync(email, user.Id))
                .WithMessage((_, email) => string.Format("El email {0} ya está registrado.", email));

        RuleFor(u => u.PhoneNumber).Cascade(CascadeMode.Stop)
            .MinimumLength(10)
            .MustAsync(async (phone, _) => await userService.IsPhoneNumber(phone!))
              .WithMessage("Número telefónico inválido.")
                //.MustAsync(async (user, phone, _) => !await userService.ExistsWithPhoneNumberAsync(phone!, user.Sucursal))
                //    .WithMessage((_, phone) => string.Format("Phone number {0} is already registered.", phone))
                .Unless(u => string.IsNullOrWhiteSpace(u.PhoneNumber));
    }
}
