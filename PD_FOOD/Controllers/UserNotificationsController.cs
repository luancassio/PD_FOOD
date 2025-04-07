using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PD_FOOD.Application.Commands.Transactions;
using PD_FOOD.Application.Commands.UserNotifications;
using PD_FOOD.Application.Queries.Transactions;
using PD_FOOD.Application.Queries.UserNotifications;
using System.Net;

namespace PD_FOOD.Controllers
{
    [Route("api/[controller]")]
    public class UserNotificationsController : MainController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserNotificationsController> _logger;
        public UserNotificationsController(IMediator mediator, ILogger<UserNotificationsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all Users Notification");
            var result = await _mediator.Send(new GetAllUserNotificationsQuery());

            _logger.LogInformation("Returned {Count} Users Notification", result.Count);

            return CustomResponse(new { status = HttpStatusCode.OK, data = result });
        }
        [HttpPut("IsActive")]
        public async Task<IActionResult> UpdateIsActiveNotification(UpdateUserNotificationIsActiveCommand command)
        {
            _logger.LogInformation("Update isActive User Notification");
            var result = await _mediator.Send(command);
            if (!result)
            {
                _logger.LogWarning("Id is not find");
                AddErrorProcess($"Id is not find");
                return CustomResponse();
            }
            return CustomResponse(new { status = HttpStatusCode.OK, data = result });
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserNotification([FromBody] CreateUserNotificationsCommand command)
        {
            _logger.LogInformation("Created User Notification");
            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogWarning("Erro Created User Notification");
                AddErrorProcess($"Erro Created User Notification");
                return CustomResponse();
            }

            return CustomResponse(new { status = HttpStatusCode.OK, data = result });
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUserNotification(DeleteUserNotificationCommand command)
        {
            _logger.LogInformation("Delete User Notification");
            var result = await _mediator.Send(command);
            if (!result)
            {
                _logger.LogWarning("Erro Delete User Notification");
                AddErrorProcess($"Erro Delete User Notification");
                return CustomResponse();
            }
            return CustomResponse(new { status = HttpStatusCode.OK, data = result });
        }
    }
}
