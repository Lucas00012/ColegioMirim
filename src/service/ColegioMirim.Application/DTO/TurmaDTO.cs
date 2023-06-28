using AutoMapper;
using ColegioMirim.Domain.Turmas;

namespace ColegioMirim.Application.DTO
{
    public class TurmaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int Ano { get; set; }
        public DateTimeOffset CriadoEm { get; set; }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Turma, TurmaDTO>()
                    .ForMember(c => c.CriadoEm, opt => opt.MapFrom(c => c.CreatedAt))
                    .ReverseMap()
                    .ForMember(c => c.CreatedAt, opt => opt.MapFrom(c => c.CriadoEm));
            }
        }
    }
}
