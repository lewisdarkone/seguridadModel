using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.models.Request.RequestsIdentity;
using FluentValidation;


namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// 
/// </summary>
public class CreateModulosRequestValidator : CustomValidator<CreateModulosRequest>
{
    /// <summary>
    /// Validaciones de agregar una nueva Modulos
    /// </summary>
    public CreateModulosRequestValidator(IUnitOfWorkIdentity repository)
    {
        RuleFor(p => p.Nombre).MustAsync(async (des, cl) =>
        {
            var paramero = repository.ModulosRepository.GetAll().Result.FirstOrDefault(p => p.Nombre.Equals(des, StringComparison.OrdinalIgnoreCase));
            return paramero is null;
        }).WithMessage("Ya existe el Modulo ({PropertyValue})");

    }
}
