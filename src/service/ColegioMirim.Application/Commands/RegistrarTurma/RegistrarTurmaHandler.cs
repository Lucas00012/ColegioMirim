﻿using AutoMapper;
using ColegioMirim.Application.DTO;
using ColegioMirim.Application.Queries.ObterTurma;
using ColegioMirim.Core.Messages;
using ColegioMirim.Domain.Turmas;
using MediatR;

namespace ColegioMirim.Application.Commands.RegistrarTurma
{
    public class RegistrarTurmaHandler :
        CommandHandler,
        IRequestHandler<RegistrarTurmaCommand, CommandResponse<TurmaDTO>>
    {
        private readonly ITurmaRepository _turmaRepository;
        private readonly IMediator _mediator;

        public RegistrarTurmaHandler(ITurmaRepository turmaRepository, IMediator mediator)
        {
            _turmaRepository = turmaRepository;
            _mediator = mediator;
        }

        public async Task<CommandResponse<TurmaDTO>> Handle(RegistrarTurmaCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido())
            {
                AdicionarErros(request.ValidationResult);
                return Error<TurmaDTO>();
            }

            var turma = await _turmaRepository.GetByName(request.Nome);
            if (turma is not null)
            {
                AdicionarErro("O nome já existe em outra turma");
                return Error<TurmaDTO>();
            }

            turma = new Turma
            {
                Ano = request.Ano,
                Nome = request.Nome,
                Ativo = true
            };

            await _turmaRepository.Create(turma);

            var dto = await _mediator.Send(new ObterTurmaQuery
            {
                Id = turma.Id
            }, cancellationToken);

            return Success(dto);
        }
    }
}
