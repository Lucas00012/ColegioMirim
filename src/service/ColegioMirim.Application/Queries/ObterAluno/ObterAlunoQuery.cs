using ColegioMirim.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioMirim.Application.Queries.ObterAluno
{
    public class ObterAlunoQuery : IRequest<AlunoDTO>
    {
        public int Id { get; set; }
    }
}
