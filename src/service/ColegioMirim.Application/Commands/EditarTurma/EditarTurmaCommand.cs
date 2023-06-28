using ColegioMirim.Application.Commands.EditarAluno;
using ColegioMirim.Application.DTO;
using ColegioMirim.Core.Messages;
using ColegioMirim.Domain.Alunos.Rules;
using ColegioMirim.Domain.Turmas.Rules;
using ColegioMirim.Domain.Usuarios.Rules;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ColegioMirim.Application.Commands.EditarTurma
{
    public class EditarTurmaCommand : Command, IRequest<CommandResponse<TurmaDTO>>
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int Ano { get; set; }

        public class Validator : AbstractValidator<EditarTurmaCommand>
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
