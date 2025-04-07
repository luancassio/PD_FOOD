using MediatR;
using PD_FOOD.Domain.Interfaces;

namespace PD_FOOD.Application.Commands.UserNotifications
{
    public class DeleteUserNotificationCommandHandler : IRequestHandler<DeleteUserNotificationCommand, bool>
    {
        private readonly IUserNotificationRepository _repository;
        public DeleteUserNotificationCommandHandler(IUserNotificationRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(DeleteUserNotificationCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteAsync(request.Id);
        }
    }
}

