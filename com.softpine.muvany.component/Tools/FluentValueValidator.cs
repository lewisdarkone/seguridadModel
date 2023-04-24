using FluentValidation;

namespace com.softpine.muvany.component.Tools;

/// <summary>
/// A glue class to make it easy to define validation rules for single values using FluentValidation
/// You can reuse this class for all your fields
/// </summary>
/// <typeparam name="T"></typeparam>
public class FluentValueValidator<T> : AbstractValidator<T>
{
    public FluentValueValidator(Action<IRuleBuilderInitial<T, T>> rule)
    {
        rule(RuleFor(x => x));
    }


    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<T>.CreateWithOptions((T)model, x => x.IncludeProperties(propertyName)));
        if ( result.IsValid )
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
