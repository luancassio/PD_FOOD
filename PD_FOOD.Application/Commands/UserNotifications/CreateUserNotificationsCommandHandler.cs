using MediatR;
using PD_FOOD.Domain.Entities;
using PD_FOOD.Domain.Interfaces;

namespace PD_FOOD.Application.Commands.UserNotifications
{
    public class CreateUserNotificationsCommandHandler : IRequestHandler<CreateUserNotificationsCommand, UserNotification>
    {
        private readonly IUserNotificationRepository _repository;

        public CreateUserNotificationsCommandHandler(IUserNotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserNotification> Handle(CreateUserNotificationsCommand request, CancellationToken cancellationToken)
        {
            var userNotification = new UserNotification
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Hour = request.Hour,
                IsActive = request.IsActive
            };

            return await _repository.CreateAsync(userNotification);
        }
    }
}
