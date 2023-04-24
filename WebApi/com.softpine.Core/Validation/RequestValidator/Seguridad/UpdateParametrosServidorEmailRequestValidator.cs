using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.models.Entities;
using com.softpine.muvany.models.Requests;
using FluentValidation;

namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// 
/// </summary>
public class UpdateParametrosServidorEmailRequestValidator : CustomValidator<UpdateParametrosServidorEmailRequest>
{
    /// <summary>
    /// Validaciones de agregar una nueva ParametrosServidorEmail
    /// </summary>
    public UpdateParametrosServidorEmailRequestValidator(IRepository<ParametrosServidorEmail> repository)
    {
        RuleFor(p => p.Id).MustAsync(async (id, cl) =>
        {
            var paramero = await repository.GetById((int)id);
            return paramero is not null;
        }).WithMessage("No existe el Sucursal {PropertyValue}");

    }
}
