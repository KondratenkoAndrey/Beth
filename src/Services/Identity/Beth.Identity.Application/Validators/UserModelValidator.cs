using Beth.Identity.Application.Models;
using FluentValidation;

namespace Beth.Identity.Application.Validators;

public class UserModelValidator : AbstractValidator<UserModel>
{
    public UserModelValidator()
    {
        RuleFor(x => x.MobilePhone)
            .NotNull()
            .NotEmpty()
            .Length(10)
            .Matches(@"^\d+$").WithMessage(x => $"'{nameof(x.MobilePhone)}' должно содержать только цифры");
    }
}