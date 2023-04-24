using com.softpine.muvany.models.DTOS;
using FluentValidation;

namespace com.softpine.muvany.component.Forms;

/// <summary>
/// Clase para manejo del formulario crear o editar modulos
/// </summary>
public class EditCreateAccionesForm
{
    public int? Id { get; set; }
    public string Nombre { get; set; }

}
public class EditCreateAccionesFormValidator : AbstractValidator<EditCreateAccionesForm>
{
    public EditCreateAccionesFormValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(20);
    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<EditCreateAccionesForm>.CreateWithOptions((EditCreateAccionesForm)model, x => x.IncludeProperties(propertyName)));
        if ( result.IsValid )
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
