using AutoMapper;
using ColegioMirim.Application.DTO;
using ColegioMirim.Application.Queries.ObterAluno;
using ColegioMirim.Core.Messages;
using ColegioMirim.Domain.Alunos;
using ColegioMirim.Domain.Usuarios;
using ColegioMirim.WebAPI.Core.Identity;
using MediatR;
using System.Net;

namespace ColegioMirim.Application.Commands.EditarAluno
{
    public class EditarAlunoHandler :
        CommandHandler,
        IRequestHandler<EditarAlunoCommand, CommandResponse<AlunoDTO>>
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMediator _mediator;

        public EditarAlunoHandler(IAlunoRepository alunoRepository, IUsuarioRepository usuarioRepository, IMediator mediator)
        {
            _alunoRepository = alunoRepository;
            _usuarioRepository = usuarioRepository;
            _mediator = mediator;
        }

        public async Task<CommandResponse<AlunoDTO>> Handle(EditarAlunoCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido())
            {
                AdicionarErros(request.ValidationResult);
                return Error<AlunoDTO>();
            }

            var aluno = await _alunoRepository.GetById(request.Id);
            if (aluno is null)
            {
                AdicionarErro("Aluno não encontrado");
                return Error<AlunoDTO>(HttpStatusCode.NotFound);
            }

            var alunoPorRA = await _alunoRepository.GetByRA(request.RA);
            if (alunoPorRA is not null && alunoPorRA.Id != aluno.Id)
            {
                AdicionarErro("O RA já está sendo utilizado");
                return Error<AlunoDTO>();
            }

            var usuario = await _usuarioRepository.GetById(aluno.UsuarioId);

            var usuarioPorEmail = await _usuarioRepository.GetByEmail(request.Email);
            if (usuarioPorEmail is not null && usuarioPorEmail.Id != usuario.Id)
            {
                AdicionarErro("O email já está sendo utilizado por outro usuário");
                return Error<AlunoDTO>();
            }

            aluno.Nome = request.Nome;
            aluno.RA = request.RA;
            usuario.Email = request.Email;
            aluno.Ativo = request.Ativo;

            await _alunoRepository.Update(aluno);
            await _usuarioRepository.Update(usuario);

            var dto = await _mediator.Send(new ObterAlunoQuery 
            { 
                Id = aluno.Id 
            }, cancellationToken);

            return Success(dto);
        }
    }
}
