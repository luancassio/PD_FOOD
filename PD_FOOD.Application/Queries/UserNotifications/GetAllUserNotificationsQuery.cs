using MediatR;
using PD_FOOD.Domain.Entities;

namespace PD_FOOD.Application.Queries.UserNotifications
{
    public class GetAllUserNotificationsQuery : IRequest<List<UserNotification>>{}
}
