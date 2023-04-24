using FluentValidation;

namespace com.softpine.muvany.component.Forms;

/// <summary>
/// Clase para manejo del formulario de login
/// </summary>
public class ResetPasswordForm
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string ValidationCode { get; set; }

}
public class ResetPasswordFormValidator : AbstractValidator<ResetPasswordForm>
{
    public ResetPasswordFormValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.ValidationCode)
            .NotEmpty();

        RuleFor(p => p.Password).NotEmpty().WithMessage("La contraseña no puede estar vacía.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
                .MaximumLength(16).WithMessage("La contraseña debe tener exceder lo 16 caracteres.")
                .Matches(@"[A-Z]+").WithMessage("La contraseña debe tener al menos una letra mayúscula.")
                .Matches(@"[a-z]+").WithMessage("La contraseña debe tener al menos una letra minúscula.")
                .Matches(@"[0-9]+").WithMessage("La contraseña debe tener al menos un numero.")
                .Matches(@"[\!\?\*\.]+").WithMessage("La contraseña debe tener al menos un (!? *.).");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .Equal(x =>x.Password)
            .WithMessage("Las contraseñas no coinciden");


    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<ResetPasswordForm>.CreateWithOptions((ResetPasswordForm)model, x => x.IncludeProperties(propertyName)));
        if ( result.IsValid )
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
