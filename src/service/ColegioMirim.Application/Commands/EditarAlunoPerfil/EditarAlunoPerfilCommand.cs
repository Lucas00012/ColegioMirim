using ColegioMirim.Application.DTO;
using ColegioMirim.Core.Messages;
using ColegioMirim.Domain.Alunos.Rules;
using ColegioMirim.Domain.Usuarios.Rules;
using FluentValidation;
using MediatR;

namespace ColegioMirim.Application.Commands.EditarAlunoPerfil
{
    public class EditarAlunoPerfilCommand : Command, IRequest<CommandResponse<AlunoDTO>>
    {
        public string Nome { get; set; }
        public string Email { get; set; }

        public class Validator : AbstractValidator<EditarAlunoPerfilCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Nome)
                    .Must(c => new NomeAlunoValidoRule(c).IsValid())
                    .WithMessage(NomeAlunoValidoRule.ErrorMessage);

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
