using ColegioMirim.Application.Services.JwtToken.Models;
using ColegioMirim.Core.Messages;
using FluentValidation;
using MediatR;

namespace ColegioMirim.Application.Commands.RealizarLogin
{
    public class RealizarLoginCommand : Command, IRequest<CommandResponse<JwtTokenResult>>
    {
        public string Email { get; set; }
        public string Senha { get; set; }

        public class Validator : AbstractValidator<RealizarLoginCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Email)
                    .EmailAddress();

                RuleFor(c => c.Senha)
                    .NotEmpty();
            }
        }

        public override bool EhValido()
        {
            ValidationResult = new Validator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
