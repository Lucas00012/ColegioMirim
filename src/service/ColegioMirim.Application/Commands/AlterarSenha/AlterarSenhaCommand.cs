using ColegioMirim.Core.Messages;
using ColegioMirim.Domain.Usuarios.Rules;
using FluentValidation;
using MediatR;

namespace ColegioMirim.Application.Commands.AlterarSenha
{
    public class AlterarSenhaCommand : Command, IRequest<CommandResponse>
    {
        public string SenhaAtual { get; set; }
        public string NovaSenha { get; set; }
        public string ConfirmarNovaSenha { get; set; }

        public class Validator : AbstractValidator<AlterarSenhaCommand>
        {
            public Validator()
            {
                RuleFor(c => c.SenhaAtual)
                    .NotEmpty();

                RuleFor(c => c.NovaSenha)
                    .Must(c => new SenhaForteRule(c).IsValid())
                    .WithMessage(SenhaForteRule.ErrorMessage);

                RuleFor(c => c.ConfirmarNovaSenha)
                    .Must((c, d) => c.NovaSenha == d)
                    .WithMessage("As senhas não conferem");
            }
        }

        public override bool EhValido()
        {
            ValidationResult = new Validator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
