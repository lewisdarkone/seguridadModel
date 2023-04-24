using com.softpine.muvany.models.DTOS;
using FluentValidation;

namespace com.softpine.muvany.component.Forms;

/// <summary>
/// Clase para manejo del formulario crear o editar modulos
/// </summary>
public class EditCreatePermisosForm
{
    public int? Id { get; set; }
    public AccionesDto? Accion { get; set; }
    public RecursosDto? Recurso { get; set; }
    public bool EsBasico { get; set; }

}
public class EditCreatePermisosFormValidator : AbstractValidator<EditCreatePermisosForm>
{
    public EditCreatePermisosFormValidator()
    {
        RuleFor(x => x.Accion)
            .NotNull();

        RuleFor(x => x.Recurso)
            .NotNull();
    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<EditCreatePermisosForm>.CreateWithOptions((EditCreatePermisosForm)model, x => x.IncludeProperties(propertyName)));
        if ( result.IsValid )
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
