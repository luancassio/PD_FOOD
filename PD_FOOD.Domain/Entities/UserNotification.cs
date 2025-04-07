
namespace PD_FOOD.Domain.Entities
{
    public class UserNotification
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public TimeOnly Hour { get; set; }

    }
}
