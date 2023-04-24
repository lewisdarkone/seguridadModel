using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.models.Request.RequestsIdentity;
using FluentValidation;

namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// 
/// </summary>
public class UpdateModulosRequestValidator : CustomValidator<UpdateModulosRequest>
{
    /// <summary>
    /// Validaciones de agregar una nueva Modulos
    /// </summary>
    public UpdateModulosRequestValidator(IUnitOfWorkIdentity repository)
    {
        RuleFor(p => p.Nombre).MustAsync(async (mo, des, cl) =>
        {
            var paramero = repository.ModulosRepository.GetAll().Result.FirstOrDefault(p => p.Nombre.Equals(des, StringComparison.OrdinalIgnoreCase));
            return paramero is null || paramero.Id == mo.Id;
        }).WithMessage("Ya existe el Modulo ({PropertyValue})");
        RuleFor(p => p.Id).MustAsync(async (id, cl) =>
        {
            var paramero = await repository.ModulosRepository.GetById((int)id);
            return paramero is not null;
        }).WithMessage("No existe el Sucursal {PropertyValue}");

    }
}
