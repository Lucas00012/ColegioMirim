using FluentValidation.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace ColegioMirim.Core.Messages
{
    public abstract class Command
    {
        [JsonIgnore]
        public ValidationResult ValidationResult { get; protected set; }

        public abstract bool EhValido();
    }
}
