using ColegioMirim.Application.DTO;
using ColegioMirim.Core.Messages;
using ColegioMirim.Domain.Alunos;
using ColegioMirim.Domain.Alunos.Rules;
using ColegioMirim.Domain.Usuarios;
using ColegioMirim.Domain.Usuarios.Rules;
using FluentValidation;
using MediatR;

namespace ColegioMirim.Application.Commands.RegistrarAluno
{
    public class RegistrarAlunoCommand : Command, IRequest<CommandResponse<AlunoDTO>>
    {
        public string Nome { get; set; }
        public string RA { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }

        public class Validator : AbstractValidator<RegistrarAlunoCommand>
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

                RuleFor(c => c.Senha)
                    .Must(c => new SenhaForteRule(c).IsValid())
                    .WithMessage(SenhaForteRule.ErrorMessage);

                RuleFor(c => c.ConfirmarSenha)
                    .Must((c, d) => c.Senha == d)
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
