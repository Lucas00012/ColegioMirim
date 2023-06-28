using FluentValidation.Results;
using System.Net;

namespace ColegioMirim.Core.Messages
{
    public class CommandResponse<T> : CommandResponse
    {
        public T Result { get; set; }
    }

    public class CommandResponse
    {
        public ValidationResult ValidationResult { get; set; }
        public HttpStatusCode? ErrorStatusCode { get; set; }

        public bool Success => !ErrorStatusCode.HasValue;
    }
}
