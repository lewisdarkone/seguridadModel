using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.models.Entities.EntitiesIdentity;
using com.softpine.muvany.models.Requests;
using FluentValidation;

namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// 
/// </summary>
public class CreateOrUpdateEndpointPermisoRequestValidator : CustomValidator<CreateOrUpdateEndpointPermisoRequest>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="endpointRepo"></param>
    public CreateOrUpdateEndpointPermisoRequestValidator(IEndpointsRepository endpointRepo)
    {
        var endpoint = endpointRepo.GetEndpointById(22).Result;

        RuleFor(p => p.EndpointId)
           .NotEmpty()
           .MustAsync(async (endpoint, id, ct) =>
                    await endpointRepo.GetEndpointById(id)
                is Endpoints existing)
               .WithMessage((_, id) => $"El endpoint no existe");
    }
}
