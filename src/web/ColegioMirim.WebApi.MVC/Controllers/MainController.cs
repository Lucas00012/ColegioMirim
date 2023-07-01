using ColegioMirim.Core.Communication;
using ColegioMirim.WebAPI.Core.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ColegioMirim.WebApi.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool PossuiErros(ResponseResult resposta)
        {
            if (resposta != null && resposta.Errors.Mensagens.Any())
            {
                foreach (var mensagem in resposta.Errors.Mensagens)
                    ModelState.AddModelError(string.Empty, mensagem);

                return true;
            }

            return false;
        }

        protected string[] ExtrairErros(ModelStateDictionary state)
        {
            return state.Values
                .SelectMany(c => c.Errors)
                .Select(c => c.ErrorMessage)
                .ToArray();
        }

        protected void AdicionarErroValidacao(string mensagem)
        {
            ModelState.AddModelError(string.Empty, mensagem);
        }

        protected bool OperacaoValida()
        {
            return ModelState.ErrorCount == 0;
        }

        protected IActionResult RedirecionarPaginaPrincipal(UserSession userSession)
        {
            if (userSession.IsAuthenticated)
            {
                if (userSession.IsAdmin)
                    return RedirectToAction("Index", "Alunos");

                if (userSession.IsAluno)
                    return RedirectToAction("Matricula", "Turmas");
            }

            return RedirectToAction("Login", "Usuarios");
        }
    }
}
