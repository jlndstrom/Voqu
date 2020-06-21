using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voqu.Models;

namespace Voqu.Services.Mappers
{
    public class ClassroomMapper : IMapper<Classroom, ClassroomViewModel>
    {
        public Classroom Map(ClassroomViewModel viewModel)
        {
            return new Classroom()
            {
                Name = viewModel.Name,
                Created = viewModel.Created,
                Voqus = viewModel.Voqus.Select((x) => new Voqu.Models.Voqu() { Id = x.Id, Question = x.Question, Votes = x.Votes }).ToList(),
            };
        }

        public ClassroomViewModel Map(Classroom model)
        {
            return new ClassroomViewModel()
            {
                Name = model.Name,
                Created = model.Created,
                Voqus = model.Voqus.Select((x) => new VoquViewModel() { Id = x.Id, Question = x.Question, Votes = x.Votes }).ToList(),
                AccessCode = model.AccessCode.ToString()
            };
        }
    }
}
