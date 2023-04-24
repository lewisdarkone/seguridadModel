using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.models.Enumerations;
using com.softpine.muvany.models.Requests;
using FluentValidation;


namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// 
/// </summary>
public class UpdateAccionesRequestValidator : CustomValidator<UpdateAccionesRequest>
{
    /// <summary>
    /// Validaciones de agregar una nueva Acciones
    /// </summary>
    public UpdateAccionesRequestValidator(IUnitOfWorkIdentity repository)
    {
        RuleFor(p => p.Id).MustAsync(async (id, cl) =>
        {
            var paramero = await repository.Acciones.GetById((int)id);
            return paramero is not null && paramero.Estado == (int)EstadosEnum.Activo;
        }).WithMessage("No existe el {PropertyName} {PropertyValue}");

        RuleFor(p => p.Nombre).MustAsync(async (ac, des, cl) =>
        {
            var paramero = repository.Acciones.GetAll().Result.FirstOrDefault(p => p.Nombre.Equals(des, StringComparison.OrdinalIgnoreCase));
            return paramero is null || paramero.Id == ac.Id;
        }).WithMessage("Ya existe la Acción ({PropertyValue})");
    }
}
