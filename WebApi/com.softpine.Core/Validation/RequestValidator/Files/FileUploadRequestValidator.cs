using com.softpine.muvany.models.Requests;
using FluentValidation;

namespace com.softpine.muvany.core.Validation.RequestValidator.Files;

/// <summary>
/// 
/// </summary>
public class FileUploadRequestValidator : CustomValidator<FileUploadRequest>
{
    /// <summary>
    /// 
    /// </summary>
    public FileUploadRequestValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
                .WithMessage("Image Name cannot be empty!")
            .MaximumLength(150);

        RuleFor(p => p.Extension)
            .NotEmpty()
                .WithMessage("Image Extension cannot be empty!")
            .MaximumLength(5);

        RuleFor(p => p.Data)
            .NotEmpty()
                .WithMessage("Image Data cannot be empty!");
    }
}
