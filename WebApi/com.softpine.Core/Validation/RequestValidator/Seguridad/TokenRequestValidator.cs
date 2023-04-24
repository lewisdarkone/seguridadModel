using com.softpine.muvany.models.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.softpine.muvany.core.Validation.RequestValidator.Seguridad;

/// <summary>
/// Validación de los parametros
/// </summary>
public class TokenRequestValidator : CustomValidator<TokenRequest>
{
    /// <summary>
    /// 
    /// </summary>
    public TokenRequestValidator()
    {
        RuleFor(p => p.Email).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress()
                .WithMessage("Dirección de email inválida.");

        RuleFor(p => p.Password).Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}
