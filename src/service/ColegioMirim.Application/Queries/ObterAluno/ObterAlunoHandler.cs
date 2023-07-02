using AutoMapper;
using ColegioMirim.Application.DTO;
using ColegioMirim.Domain.Alunos;
using ColegioMirim.Domain.Usuarios;
using ColegioMirim.Infrastructure.Data;
using ColegioMirim.WebAPI.Core.Identity;
using Dapper;
using MediatR;

namespace ColegioMirim.Application.Queries.ObterAluno
{
    public class ObterAlunoHandler : IRequestHandler<ObterAlunoQuery, AlunoDTO>
    {
        private readonly IUserSession _userSession;
        private readonly ColegioMirimContext _context;

        public ObterAlunoHandler(IUserSession userSession, ColegioMirimContext context)
        {
            _userSession = userSession;
            _context = context;
        }

        public async Task<AlunoDTO> Handle(ObterAlunoQuery request, CancellationToken cancellationToken)
        {
            var dto = await _context.Connection.QuerySingleAsync<AlunoDTO>(@"
                SELECT
                    a.Id,
                    a.RA,
                    a.Nome,
                    u.Email,
                    a.Ativo,
                    a.CreatedAt AS CriadoEm
                FROM Aluno AS a
                INNER JOIN Usuario AS u ON u.Id = a.UsuarioId
                WHERE a.Id = @Id AND (@IsAdmin = 1 OR a.UsuarioId = @UsuarioId AND a.Ativo = 1)
            ", new { request.Id, _userSession.IsAdmin, _userSession.UsuarioId });

            return dto;
        }
    }
}
