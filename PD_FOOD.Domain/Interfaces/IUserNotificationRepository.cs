using PD_FOOD.Domain.Entities;

namespace PD_FOOD.Domain.Interfaces
{
    public interface IUserNotificationRepository
    {
        Task<List<UserNotification>> GetAllAsync();
        Task<UserNotification> CreateAsync(UserNotification userNotification);
        Task<bool> UpdateIsActiveAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
