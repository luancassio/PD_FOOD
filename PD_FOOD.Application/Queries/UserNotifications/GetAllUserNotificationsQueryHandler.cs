using MediatR;
using PD_FOOD.Domain.Entities;
using PD_FOOD.Domain.Interfaces;


namespace PD_FOOD.Application.Queries.UserNotifications
{
    public class GetAllUserNotificationsQueryHandler : IRequestHandler<GetAllUserNotificationsQuery, List<UserNotification>>
    {
        private readonly IUserNotificationRepository _repository;
        public GetAllUserNotificationsQueryHandler(IUserNotificationRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<UserNotification>> Handle(GetAllUserNotificationsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
