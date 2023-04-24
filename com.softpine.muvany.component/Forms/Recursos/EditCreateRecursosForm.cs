using com.softpine.muvany.models.DTOS;
using FluentValidation;

namespace com.softpine.muvany.component.Forms;

/// <summary>
/// Clase para manejo del formulario de login
/// </summary>
public class EditCreateRecursosForm
{
    public int? Id { get; set; }
    public string? Nombre { get; set; }
    public ModulosDto? Modulo { get; set; }
    public bool EsMenuConfiguracion { get; set; }
    public string? DescripcionMenuConfiguracion { get; set; }
    public bool? Estado { get; set; }
    public string? Url { get; set; }

}
public class EditCreateRecursosFormValidator : AbstractValidator<EditCreateRecursosForm>
{
    public EditCreateRecursosFormValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.DescripcionMenuConfiguracion)
            .MaximumLength(200);

        RuleFor(x => x.Url)
            .MaximumLength(100);


    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<EditCreateRecursosForm>.CreateWithOptions((EditCreateRecursosForm)model, x => x.IncludeProperties(propertyName)));
        if ( result.IsValid )
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
