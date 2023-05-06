using FluentValidation;
using Shared;
using Shared.ValueObject.DTO;
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
}
