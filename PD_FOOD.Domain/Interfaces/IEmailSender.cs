
using PD_FOOD.Domain.Dtos;

namespace PD_FOOD.Domain.Interfaces
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(List<SendEmailDto> emails);
    }
}
