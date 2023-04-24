using com.softpine.muvany.models.DTOS;
using FluentValidation;

namespace com.softpine.muvany.component.Forms;

/// <summary>
/// Clase para manejo del formulario crear o editar modulos
/// </summary>
public class EditCreateModulosForm
{
    public int? Id { get; set; }
    public string? Nombre { get; set; }
    public int? ModuloPadre { get; set; }
    public ModulosDto? ModuloPadreNav { get; set; }
    public string? CssIcon { get; set; }
    public bool? Estado { get; set; }

}
public class EditCreateModulosFormValidator : AbstractValidator<EditCreateModulosForm>
{
    public EditCreateModulosFormValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(100);


    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<EditCreateModulosForm>.CreateWithOptions((EditCreateModulosForm)model, x => x.IncludeProperties(propertyName)));
        if ( result.IsValid )
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
