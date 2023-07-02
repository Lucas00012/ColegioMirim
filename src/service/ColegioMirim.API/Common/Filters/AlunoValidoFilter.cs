using ColegioMirim.Domain.Alunos;
using ColegioMirim.WebAPI.Core.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ColegioMirim.API.Common.Filters
{
    public class AlunoValidoFilter : IAsyncAuthorizationFilter
    {
        private readonly IUserSession _userSession;
        private readonly IAlunoRepository _alunoRepository;

        public AlunoValidoFilter(IAlunoRepository alunoRepository, IUserSession userSession)
        {
            _alunoRepository = alunoRepository;
            _userSession = userSession;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!_userSession.IsAluno)
                return;

            var aluno = await _alunoRepository.GetByUsuarioId(_userSession.UsuarioId.Value);

            if (!aluno.Ativo)
                context.Result = new UnauthorizedResult();
        }
    }
}
