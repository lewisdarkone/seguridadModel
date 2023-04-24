using com.softpine.muvany.models.DTOS;
using FluentValidation;

namespace com.softpine.muvany.component.Forms;

/// <summary>
/// Clase para manejo del formulario crear o editar modulos
/// </summary>
public class EditCreateRoleForm
{
    public string? Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool RolInterno { get; set; }

}
public class EditCreateRoleFormValidator : AbstractValidator<EditCreateRoleForm>
{
    public EditCreateRoleFormValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(100);
    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<EditCreateRoleForm>.CreateWithOptions((EditCreateRoleForm)model, x => x.IncludeProperties(propertyName)));
        if ( result.IsValid )
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
