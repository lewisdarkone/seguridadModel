using FluentValidation;

namespace com.softpine.muvany.component.Forms;

/// <summary>
/// Clase para manejo del formulario de login
/// </summary>
public class LoginForm
{
    public string? Email { get; set; }
    public string? Contrasena { get; set; }
}

public class LoginFormValidator : AbstractValidator<LoginForm>
{
    public LoginFormValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.Contrasena)
            .NotEmpty()
            .Length(8, 16);


    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<LoginForm>.CreateWithOptions((LoginForm)model, x => x.IncludeProperties(propertyName)));
        if ( result.IsValid )
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
