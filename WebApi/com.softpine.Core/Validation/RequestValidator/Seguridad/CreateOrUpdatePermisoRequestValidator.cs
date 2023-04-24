using com.softpine.muvany.core.Exceptions;
using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.models.Requests;
using FluentValidation;


namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// 
/// </summary>
public class CreateOrUpdatePermisoRequestValidator : CustomValidator<CreateOrUpdatePermisoRequest>
{
    /// <summary>
    /// 
    /// </summary>
    public CreateOrUpdatePermisoRequestValidator(IUnitOfWorkIdentity repository)
    {

        RuleFor(r => r.IdAccion).MustAsync(async (id, cl) =>
        {
            var parametro = await repository.Acciones.GetById(id);

            return parametro is not null;
        }).WithMessage("No existe el {PropertyName} {PropertyValue}");

        RuleFor(r => r.IdRecurso).MustAsync(async (id, cl) =>
        {
            var parametro = await repository.Recursos.GetById(id);

            return parametro is not null;
        }).WithMessage("No existe el {PropertyName} {PropertyValue}");

        RuleFor(r => r).MustAsync(async (pe, cl) =>
        {

            var permisoExist = await repository.PermisosRepository.GetAll();
            var accion = await repository.Acciones.GetById(pe.IdAccion);
            var recurso = await repository.Recursos.GetById(pe.IdRecurso);


            var exist = permisoExist.FirstOrDefault(x => x.Descripcion == $"{accion.Nombre}{recurso.Nombre}");
            if ( exist != null && exist.Id != pe.Id )
                throw new BusinessException("Ya existe un permiso con esta acción y recurso");


            return  true;
        });

    }
}
