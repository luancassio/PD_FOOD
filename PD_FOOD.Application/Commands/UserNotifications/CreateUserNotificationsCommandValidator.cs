using FluentValidation;

namespace PD_FOOD.Application.Commands.UserNotifications
{
    public class CreateUserNotificationsCommandValidator : AbstractValidator<CreateUserNotificationsCommand>
    {
        public CreateUserNotificationsCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(3, 50);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.IsActive).NotNull();
            RuleFor(x => x.Hour).NotNull();
        }
    }
}
