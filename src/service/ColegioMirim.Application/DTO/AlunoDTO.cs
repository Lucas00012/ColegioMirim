using AutoMapper;
using ColegioMirim.Domain.Alunos;
using ColegioMirim.Domain.Usuarios;

namespace ColegioMirim.Application.DTO
{
    public class AlunoDTO
    {
        public int Id { get; set; }
        public string RA { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public DateTimeOffset CriadoEm { get; set; }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Aluno, AlunoDTO>()
                    .ForMember(c => c.CriadoEm, opt => opt.MapFrom(c => c.CreatedAt))
                    .ReverseMap()
                    .ForMember(c => c.CreatedAt, opt => opt.MapFrom(c => c.CriadoEm));

                CreateMap<Usuario, AlunoDTO>()
                    .ReverseMap();
            }
        }
    }
}
