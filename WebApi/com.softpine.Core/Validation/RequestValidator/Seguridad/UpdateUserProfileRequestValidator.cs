using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.models.Requests;
using FluentValidation;

namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// 
/// </summary>
public class UpdateUserProfileRequestValidator : CustomValidator<UpdateUserProfileRequest>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userService"></param>
    public UpdateUserProfileRequestValidator(IUserService userService)
    {
        RuleFor(p => p.Id).NotEmpty();

        RuleFor(u => u.PhoneNumber).Cascade(CascadeMode.Stop)
            .MinimumLength(10)
            .MustAsync(async (phone, _) => await userService.IsPhoneNumber(phone!))
            .WithMessage("Invalid Phone Number.").Unless(u => string.IsNullOrWhiteSpace(u.PhoneNumber));
    }
}
