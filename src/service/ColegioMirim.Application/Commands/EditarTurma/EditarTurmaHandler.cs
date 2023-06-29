using AutoMapper;
using ColegioMirim.Application.DTO;
using ColegioMirim.Application.Queries.ObterAluno;
using ColegioMirim.Application.Queries.ObterTurma;
using ColegioMirim.Core.Messages;
using ColegioMirim.Domain.Alunos;
using ColegioMirim.Domain.Turmas;
using MediatR;
using System.Net;

namespace ColegioMirim.Application.Commands.EditarTurma
{
    public class EditarTurmaHandler :
        CommandHandler,
        IRequestHandler<EditarTurmaCommand, CommandResponse<TurmaDTO>>
    {
        private readonly ITurmaRepository _turmaRepository;
        private readonly IMediator _mediator;

        public EditarTurmaHandler(ITurmaRepository turmaRepository, IMediator mediator)
        {
            _turmaRepository = turmaRepository;
            _mediator = mediator;
        }

        public async Task<CommandResponse<TurmaDTO>> Handle(EditarTurmaCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido())
            {
                AdicionarErros(request.ValidationResult);
                return Error<TurmaDTO>();
            }

            var turma = await _turmaRepository.GetById(request.Id);
            if (turma is null)
            {
                AdicionarErro("Turma não encontrada");
                return Error<TurmaDTO>(HttpStatusCode.NotFound);
            }

            var turmaPorNome = await _turmaRepository.GetByName(request.Nome);
            if (turmaPorNome is not null && turmaPorNome.Id != turma.Id)
            {
                AdicionarErro("O nome já existe em outra turma");
                return Error<TurmaDTO>();
            }

            turma.Ano = request.Ano;
            turma.Nome = request.Nome;
            turma.Ativo = request.Ativo;

            await _turmaRepository.Update(turma);

            var dto = await _mediator.Send(new ObterTurmaQuery
            {
                Id = turma.Id
            }, cancellationToken);

            return Success(dto);
        }
    }
}
