using com.softpine.muvany.models.DTOS;
using FluentValidation;

namespace com.softpine.muvany.component.Forms;

/// <summary>
/// Clase para manejo del formulario de Crear y editar usuarios
/// </summary>
public class CreateEditUserForm
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public RoleDto? Role { get; set; }
    public List<UserRoleDto>? Roles { get; set; }
    public DateTime ExpirePasswordDate { get; set; }

}
public class CreateEditUserFormValidator : AbstractValidator<CreateEditUserForm>
{
    public CreateEditUserFormValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.FullName)
            .NotEmpty();

        RuleFor(x => x.Role)
            .NotEmpty();


    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<CreateEditUserForm>.CreateWithOptions((CreateEditUserForm)model, x => x.IncludeProperties(propertyName)));
        if ( result.IsValid )
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
