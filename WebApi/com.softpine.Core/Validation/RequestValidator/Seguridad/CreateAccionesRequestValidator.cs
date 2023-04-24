using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.models.Requests;
using FluentValidation;

namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// 
/// </summary>
public class CreateAccionesRequestValidator : CustomValidator<CreateAccionesRequest>
{
    /// <summary>
    /// Validaciones de agregar una nueva Acciones
    /// </summary>
    public CreateAccionesRequestValidator(IUnitOfWorkIdentity repository)
    {
        RuleFor(p => p.Nombre).MustAsync(async (des, cl) =>
        {
            var paramero = repository.Acciones.GetAll().Result.FirstOrDefault(p => p.Nombre.Equals(des, StringComparison.OrdinalIgnoreCase));
            return paramero is null;
        }).WithMessage("Ya existe la Acción ({PropertyValue})");
    }
}
