using com.softpine.muvany.models.Requests;
using FluentValidation;

namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// Validación de fluentValidation para las propiedades para crear un nuevo endpoint
/// </summary>
public class CreateEndpointRequestValidator : CustomValidator<CreateEndpointRequest>
{
    /// <summary>
    /// 
    /// </summary>
    public CreateEndpointRequestValidator()
    {
        RuleFor(r => r.Nombre)
            .NotNull()
            .NotEmpty()
            .WithMessage("Debe ingresar la ruta del Endpoint valido.");

        RuleFor(r => r.Controlador)
            .NotNull()
            .NotEmpty()
            .WithMessage("Debe ingresar el nombre del Controlador valido.");

        RuleFor(r => r.Metodo)
            .NotNull()
            .NotEmpty()
            .WithMessage("Debe ingresar un Metodo.");

        RuleFor(r => r.HttpVerbo)
            .NotNull()
            .NotEmpty()
            .WithMessage("Debe ingresar un Verbo Http.");
    }
}
