using com.softpine.muvany.models.DTOS;
using FluentValidation;

namespace com.softpine.muvany.component.Forms;

/// <summary>
/// Clase para manejo del formulario crear o editar modulos
/// </summary>
public class EditCreateEndpointForm
{
    public int? Id { get; set; }
    public string Nombre { get; set; }
    public string Controlador { get; set; }
    public string Metodo { get; set; }
    public string HttpVerbo { get; set; }
    public PermisosDto? Permiso { get; set; }
    public bool Estado { get; set; }

}
public class EditCreateEndpointFormValidator : AbstractValidator<EditCreateEndpointForm>
{
    public EditCreateEndpointFormValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(125);

        RuleFor(x => x.Controlador)
            .NotEmpty()
            .MaximumLength(30);

        RuleFor(x => x.Metodo)
            .NotEmpty()
            .MaximumLength(30);

        RuleFor(x => x.HttpVerbo)
            .NotEmpty()
            .MaximumLength(6);
    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<EditCreateEndpointForm>.CreateWithOptions((EditCreateEndpointForm)model, x => x.IncludeProperties(propertyName)));
        if ( result.IsValid )
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
