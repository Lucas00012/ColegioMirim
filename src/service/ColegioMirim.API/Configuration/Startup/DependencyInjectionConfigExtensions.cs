using ColegioMirim.API.Services.JwtToken;
using ColegioMirim.Application.Commands.EditarAluno;
using ColegioMirim.Core.Data;
using ColegioMirim.Core.DomainObjects;
using ColegioMirim.Domain.Alunos;
using ColegioMirim.Domain.AlunosTurma;
using ColegioMirim.Domain.Turmas;
using ColegioMirim.Domain.Usuarios;
using ColegioMirim.Infrastructure.Data;
using ColegioMirim.Infrastructure.Data.Repository;
using ColegioMirim.WebAPI.Core.Identity;

namespace ColegioMirim.API.Configuration.Startup
{
    public static class DependencyInjectionConfigExtensions
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<EditarAlunoHandler>());

            services.AddAutoMapper(typeof(Entity), typeof(EditarAlunoHandler), typeof(Aluno));

            services.AddScoped<UserSession>();

            services.AddScoped<JwtTokenService>();

            services.AddScoped<ColegioMirimContext>();
            services.AddScoped<IUnityOfWork, UnityOfWork>();

            services.AddScoped<ITurmaRepository, TurmaRepository>();
            services.AddScoped<IAlunoRepository, AlunoRepository>();
            services.AddScoped<IAlunoTurmaRepository, AlunoTurmaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }
    }
}
