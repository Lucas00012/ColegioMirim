using ColegioMirim.Application.DTO;
using ColegioMirim.Core.Messages;
using ColegioMirim.Domain.Turmas.Rules;
using FluentValidation;
using MediatR;

namespace ColegioMirim.Application.Commands.RegistrarTurma
{
    public class RegistrarTurmaCommand : Command, IRequest<CommandResponse<TurmaDTO>>
    {
        public string Nome { get; set; }
        public int Ano { get; set; }

        public class Validator : AbstractValidator<RegistrarTurmaCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Nome)
                    .Must(c => new NomeTurmaValidoRule(c).IsValid())
                    .WithMessage(NomeTurmaValidoRule.ErrorMessage);

                RuleFor(c => c.Ano)
                    .Must(c => new AnoTurmaValidoRule(c).IsValid())
                    .WithMessage(AnoTurmaValidoRule.ErrorMessage);
            }
        }

        public override bool EhValido()
        {
            ValidationResult = new Validator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
