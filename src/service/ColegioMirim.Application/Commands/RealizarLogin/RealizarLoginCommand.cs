using ColegioMirim.Application.Services.JwtToken.Models;
using ColegioMirim.Core.Messages;
using ColegioMirim.Domain.Usuarios.Rules;
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
                    .Must(c => new EmailValidoRule(c).IsValid())
                    .WithMessage(EmailValidoRule.ErrorMessage);

                RuleFor(c => c.Senha)
                    .NotEmpty()
                    .WithMessage("O campo Senha é obrigatório");
            }
        }

        public override bool EhValido()
        {
            ValidationResult = new Validator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
