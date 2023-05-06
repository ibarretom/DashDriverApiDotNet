using FluentValidation;
using FluentValidation.Results;
using Shared;
using Shared.ValueObject.DTO;
using Shared.ValueObject.Exceptions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CoreTest")]
namespace Core.Domain.Services.Validation;

internal class UserDTOValidation : AbstractValidator<UserDTO>
{
    public UserDTOValidation()
    {
        RuleFor(obj => obj.Name).NotEmpty().WithMessage(ErrorMessagesResource.NAME_BLANK);
        RuleFor(obj => obj.Email).NotEmpty().WithMessage(ErrorMessagesResource.EMAIL_BLANK);
        RuleFor(obj => obj.Password).NotEmpty().WithMessage(ErrorMessagesResource.PASSWORD_BLANK);
        When(obj => !string.IsNullOrEmpty(obj.Email), () =>
        {
            RuleFor(obj => obj.Email).EmailAddress().WithMessage(ErrorMessagesResource.EMAIL_INVALID);
        });
    }

    public void ValidateInput(UserDTO user)
    {
        ValidationResult result = Validate(user);

        if (!result.IsValid)
        {
            throw new ValidationErrorException(result.Errors.Select(error => error.ErrorMessage));
        }
    }
}
