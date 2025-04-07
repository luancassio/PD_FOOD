
using MediatR;
using PD_FOOD.Domain.Entities;

namespace PD_FOOD.Application.Commands.UserNotifications
{
    public class CreateUserNotificationsCommand : IRequest<UserNotification>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive = true;
        public TimeOnly Hour { get; set; }
    }
}
