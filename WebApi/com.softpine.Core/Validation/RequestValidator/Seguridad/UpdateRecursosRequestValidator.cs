using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.models.Enumerations;
using com.softpine.muvany.models.Requests;
using FluentValidation;

namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// 
/// </summary>
public class UpdateRecursosRequestValidator : CustomValidator<UpdateRecursosRequest>
{
    /// <summary>
    /// Validaciones de agregar una nueva Recursos
    /// </summary>
    public UpdateRecursosRequestValidator(IUnitOfWorkIdentity repository)
    {
        RuleFor(p => p.Nombre).MustAsync(async (re, des, cl) =>
        {
            var paramero = repository.Recursos.GetAll().Result.FirstOrDefault(p => p.Nombre.Equals(des, StringComparison.OrdinalIgnoreCase));

            return paramero is null || paramero.Id == re.Id || paramero.Estado == (int)EstadosEnum.Eliminado;
        }).WithMessage("Ya existe el Recurso ({PropertyValue})");
        RuleFor(p => p.Id).MustAsync(async (id, cl) =>
        {
            var paramero = await repository.Recursos.GetById(id.NullToInt());
            return paramero is not null;
        }).WithMessage("No existe el {PropertyName} {PropertyValue}");
        RuleFor(p => p.IdModulo).MustAsync(async (re, id, cl) =>
        {
            if (id == null)
                return true;

            var recurso = await repository.Recursos.GetById(re.Id.NullToInt());
            if (recurso != null && recurso.IdModulo != id)
            {
                var paramero = await repository.ModulosRepository.GetById(id.NullToInt());
                return paramero is not null;
            }
            return true;
        }).WithMessage("No existe el {PropertyName} {PropertyValue}");
    }
}
