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
                VotedQuestions = viewModel.VotedQuestions,
                AccessCode = viewModel.AccessCode
            };
        }

        public ClassroomViewModel Map(Classroom model)
        {
            return new ClassroomViewModel()
            {
                Name = model.Name,
                Created = model.Created,
                VotedQuestions = model.VotedQuestions,
                AccessCode = model.AccessCode
            };
        }
    }
}
