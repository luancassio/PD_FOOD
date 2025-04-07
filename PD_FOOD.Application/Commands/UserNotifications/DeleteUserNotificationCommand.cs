using MediatR;

namespace PD_FOOD.Application.Commands.UserNotifications
{
    public class DeleteUserNotificationCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
