using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.models.Enumerations;
using com.softpine.muvany.models.Requests;
using FluentValidation;


namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// 
/// </summary>
public class CreateRecursosRequestValidator : CustomValidator<CreateRecursosRequest>
{
    /// <summary>
    /// Validaciones de agregar una nueva Recursos
    /// </summary>
    public CreateRecursosRequestValidator(IUnitOfWorkIdentity repository)
    {
        RuleFor(p => p.Nombre).MustAsync(async (des, cl) =>
        {
            var paramero = repository.Recursos.GetAll()
            .Result.FirstOrDefault(p => p.Nombre.Equals(des, StringComparison.OrdinalIgnoreCase));
            return paramero is null || paramero.Estado == (int)EstadosEnum.Eliminado;
        }).WithMessage("Ya existe el Recurso ({PropertyValue})");

        RuleFor(p => p.IdModulo).MustAsync(async (id, cl) =>
        {
            if (id == null)
                return true;

            var paramero = await repository.ModulosRepository.GetById((int)id);
            return paramero is not null;
        }).WithMessage("No existe el {PropertyName} {PropertyValue}");


    }
}
