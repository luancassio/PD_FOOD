using MediatR;

namespace PD_FOOD.Application.Commands.UserNotifications
{
    public class UpdateUserNotificationIsActiveCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
