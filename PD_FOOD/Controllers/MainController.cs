using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace PD_FOOD.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Erros = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperationValid()) return Ok(result);

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Message", Erros.ToArray() }
            }));
        }
        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            IEnumerable<ModelError> erros = modelState.Values.SelectMany(e => e.Errors);

            foreach (var erro in erros)
            {
                AddErrorProcess(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected bool OperationValid() { return !Erros.Any(); }
        protected void AddErrorProcess(string erro) { Erros.Add(erro); }
        protected void CleanErrorProcess() { Erros.Clear(); }


    }
}
