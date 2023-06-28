using ColegioMirim.Core.Messages;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace ColegioMirim.WebAPI.Core.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        protected IActionResult CustomResponse(CommandResponse commandResponse)
        {
            if (commandResponse.Success)
                return NoContent();

            return CommandError(commandResponse);
        }

        protected IActionResult CustomResponse<T>(CommandResponse<T> commandResponse)
        {
            if (commandResponse.Success)
                return Ok(commandResponse.Result);

            return CommandError(commandResponse);
        }

        protected IActionResult ErrorResponse(params string[] errors)
        {
            return BadRequest(ModelError(errors));
        }

        private ObjectResult CommandError(CommandResponse commandResponse)
        {
            return StatusCode(
                (int)commandResponse.ErrorStatusCode, 
                ModelError(commandResponse.ValidationResult)
            );
        }

        private ValidationProblemDetails ModelError(ValidationResult validationResult)
        {
            var errors = validationResult.Errors
                .Select(c => c.ErrorMessage)
                .ToArray();

            return ModelError(errors);
        }

        private ValidationProblemDetails ModelError(string[] errors)
        {
            return new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", errors }
            });
        }
    }
}
