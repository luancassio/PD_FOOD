using MediatR;
using PD_FOOD.Domain.Interfaces;


namespace PD_FOOD.Application.Commands.UserNotifications
{
    public class UpdateUserNotificationIsActiveCommandHandler : IRequestHandler<UpdateUserNotificationIsActiveCommand, bool>
    {
        private readonly IUserNotificationRepository _repository;
        public UpdateUserNotificationIsActiveCommandHandler(IUserNotificationRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(UpdateUserNotificationIsActiveCommand request, CancellationToken cancellationToken)
        {
            return await _repository.UpdateIsActiveAsync(request.Id);
        }
    }
}
