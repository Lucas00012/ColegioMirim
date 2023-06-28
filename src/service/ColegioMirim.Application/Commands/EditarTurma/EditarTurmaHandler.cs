using AutoMapper;
using ColegioMirim.Application.DTO;
using ColegioMirim.Core.Messages;
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
        private readonly IMapper _mapper;

        public EditarTurmaHandler(ITurmaRepository turmaRepository, IMapper mapper)
        {
            _turmaRepository = turmaRepository;
            _mapper = mapper;
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

            turma.Ano = request.Ano;
            turma.Nome = request.Nome;
            turma.Ativo = request.Ativo;

            await _turmaRepository.Update(turma);

            var dto = _mapper.Map<TurmaDTO>(turma);
            return Success(dto);
        }
    }
}
