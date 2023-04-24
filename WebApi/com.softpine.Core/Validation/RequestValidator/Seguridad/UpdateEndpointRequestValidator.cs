using com.softpine.muvany.models.Requests;
using FluentValidation;

namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// Validación de la clase con FluentValidation
/// </summary>
public class UpdateEndpointRequestValidator : CustomValidator<UpdateEndpointRequest>
{
    /// <summary>
    /// 
    /// </summary>
    public UpdateEndpointRequestValidator()
    {
        RuleFor(r => r.Id)
            .NotNull()
            .NotEqual(0)
            .WithMessage("Debe ingresar un Sucursal valido.");

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
