using FluentValidation;

namespace com.softpine.muvany.component.Forms;

/// <summary>
/// Clase para manejo del formulario de login
/// </summary>
public class ForgotPasswordForm
{
    public string? Email { get; set; }

}
public class ForgotPasswordFormValidator : AbstractValidator<ForgotPasswordForm>
{
    public ForgotPasswordFormValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<ForgotPasswordForm>.CreateWithOptions((ForgotPasswordForm)model, x => x.IncludeProperties(propertyName)));
        if ( result.IsValid )
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
