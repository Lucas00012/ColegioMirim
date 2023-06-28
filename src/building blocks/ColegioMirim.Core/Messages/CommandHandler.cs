using FluentValidation.Results;
using System.Net;

namespace ColegioMirim.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        public CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AdicionarErro(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

        protected void AdicionarErros(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                ValidationResult.Errors.Add(erro);
            }
        }

        protected CommandResponse<T> Success<T>(T result)
        {
            AssertSuccessResult();
            return new CommandResponse<T>
            {
                Result = result
            };
        }

        protected CommandResponse<T> Error<T>(HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            AssertErrorCode(statusCode);
            return new CommandResponse<T>
            {
                ValidationResult = ValidationResult,
                ErrorStatusCode = statusCode
            };
        }

        protected CommandResponse Success()
        {
            AssertSuccessResult();
            return new CommandResponse();
        }

        protected CommandResponse Error(HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            AssertErrorCode(statusCode);
            return new CommandResponse
            {
                ErrorStatusCode = statusCode,
                ValidationResult = ValidationResult
            };
        }

        private void AssertSuccessResult()
        {
            if (!ValidationResult.IsValid)
                throw new InvalidOperationException();
        }

        private void AssertErrorCode(HttpStatusCode statusCode)
        {
            if (statusCode < HttpStatusCode.BadRequest)
                throw new InvalidOperationException();
        }
    }
}
