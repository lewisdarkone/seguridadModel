using com.softpine.muvany.models.DTOS;
using FluentValidation;

namespace com.softpine.muvany.component.Forms;

/// <summary>
/// Clase para manejo del formulario de login
/// </summary>
public class UserProfileForm
{
    public string Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? NombreCompleto { get; set; }
    public bool TwoFactorEnable { get; set; }

    public List<UserRoleDto>? Roles { get; set; }

}
public class UserProfileFormValidator : AbstractValidator<UserProfileForm>
{
    public UserProfileFormValidator()
    {       

        RuleFor(x => x.PhoneNumber)
            .NotEmpty();
    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<UserProfileForm>.CreateWithOptions((UserProfileForm)model, x => x.IncludeProperties(propertyName)));
        if ( result.IsValid )
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
