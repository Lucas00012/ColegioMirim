using ColegioMirim.Application.DTO;
using ColegioMirim.Core.Messages;
using ColegioMirim.Domain.Alunos.Rules;
using ColegioMirim.Domain.Usuarios.Rules;
using FluentValidation;
using MediatR;
using System.Text.Json.Serialization;

namespace ColegioMirim.Application.Commands.EditarAluno
{
    public class EditarAlunoCommand : Command, IRequest<CommandResponse<AlunoDTO>>
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Nome { get; set; }
        public string RA { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }

        public class Validator : AbstractValidator<EditarAlunoCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Nome)
                    .Must(c => new NomeAlunoValidoRule(c).IsValid())
                    .WithMessage(NomeAlunoValidoRule.ErrorMessage);

                RuleFor(c => c.RA)
                    .Must(c => new RAValidoRule(c).IsValid())
                    .WithMessage(RAValidoRule.ErrorMessage);

                RuleFor(c => c.Email)
                    .Must(c => new EmailValidoRule(c).IsValid())
                    .WithMessage(EmailValidoRule.ErrorMessage);
            }
        }

        public override bool EhValido()
        {
            ValidationResult = new Validator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
