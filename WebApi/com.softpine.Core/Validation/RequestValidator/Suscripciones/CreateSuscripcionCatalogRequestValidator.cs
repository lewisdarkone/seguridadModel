using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.models.Enumerations;
using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.Requests.SuscripcionCatalog;
using FluentValidation;

namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// 
/// </summary>
public class CreateSuscripcionCatalogRequestValidator : CustomValidator<CreateSuscripcionCatalogRequest>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userService"></param>
    public CreateSuscripcionCatalogRequestValidator(IUserService userService)
    {
        RuleFor(p => p.NombreCatalogo)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(p => p.Descripcion.Trim())
            .NotEmpty()
            .MaximumLength(300);

        RuleFor(p => p.Precio)
            .NotEmpty();
        RuleFor(p => p.PrecioPorUsuario)
            .NotEmpty();
        RuleFor(p => p.TipoSuscripcion)
            .NotEmpty()
            .IsInEnum();

    }
}
