using Microsoft.EntityFrameworkCore;
using PD_FOOD.Domain.Entities;
using PD_FOOD.Domain.Interfaces;

namespace PD_FOOD.Infrastructure.Repositories
{
    public class UserNotificationRepository : IUserNotificationRepository
    {
        private readonly ApplicationDbContext _context;
        public UserNotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<UserNotification>> GetAllAsync()
        {
            return await _context.UserNotifications.ToListAsync();
        }
        public async Task<UserNotification> CreateAsync(UserNotification userNotification)
        {
            await _context.UserNotifications.AddAsync(userNotification);
            await _context.SaveChangesAsync();
            return userNotification;
        }
        public async Task<bool> UpdateIsActiveAsync(Guid id)
        {
            var userNotification = await _context.UserNotifications.FirstOrDefaultAsync(x => x.Id == id);

            if (userNotification != null)
            {
                userNotification.IsActive = !userNotification.IsActive;

                _context.UserNotifications.Update(userNotification);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var userNotification = await _context.UserNotifications.FirstOrDefaultAsync(x => x.Id == id);

            if (userNotification != null)
            {
                _context.UserNotifications.Remove(userNotification);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}

