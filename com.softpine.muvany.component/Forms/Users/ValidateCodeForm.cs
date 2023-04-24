using FluentValidation;

namespace com.softpine.muvany.component.Forms;

/// <summary>
/// Clase para manejo del formulario de login
/// </summary>
public class ValidateCodeForm
{
    public string Email { get; set; }
    public string? NombreCompleto { get; set; }
    public int Code { get; set; }
}

public class ValidateCodeFormValidator : AbstractValidator<ValidateCodeForm>
{
    public ValidateCodeFormValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.Code)
            .NotEmpty()
            .GreaterThan(99999)
            .LessThan(1000000);


    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<ValidateCodeForm>.CreateWithOptions((ValidateCodeForm)model, x => x.IncludeProperties(propertyName)));
        if ( result.IsValid )
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
