using com.softpine.muvany.models.Requests;
using FluentValidation;


namespace com.softpine.muvany.core.Validation.RequestValidator.Files;

/// <summary>
/// 
/// </summary>
public class AddAjuntoPhotoProfileRequestValidator : CustomValidator<AddAdjuntoPhotoProfileRequest>
{
    /// <summary>
    /// Validaciones de agregar una nueva Recursos
    /// </summary>
    public AddAjuntoPhotoProfileRequestValidator()
    {
        RuleFor(p => p.AdjuntoInBytes).Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage("Debe carga un adjunto valido");
    }
}
