using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voqu.Models;
using Voqu.Services.Mappers;
using Voqu.Services.Providers;

namespace Voqu.Services.Services
{
    public class ClassroomService : IClassroomService
    {
        private readonly IClassroomRepository _classroomRepository;

        public ClassroomService(IClassroomRepository classroomRepository)
        {
            _classroomRepository = classroomRepository;
        }

        public Classroom CreateQuestion(string accessCode, string question)
        {
            var classroom = _classroomRepository.GetClassroomByAccessCode(accessCode);
            classroom.Voqus.Add(new Models.Voqu() { Question = question, Id = classroom.Voqus.Count + 1 });

            return classroom;
        }

        public Classroom DeleteQuestion(string accessCode, long voquId)
        {
            var classroom = _classroomRepository.GetClassroomByAccessCode(accessCode);
            classroom.Voqus.Remove(classroom.Voqus.FirstOrDefault(x => x.Id == voquId));

            return classroom;
        }

        public Classroom GetClassroom(string accessCode)
        {
            var classroom = _classroomRepository.GetClassroomByAccessCode(accessCode);

            return classroom;
        }

        public Classroom Vote(string accessCode, long voquId, string userId)
        {
            var classroom = _classroomRepository.GetClassroomByAccessCode(accessCode);
            var voqu = classroom.Voqus.FirstOrDefault(x => x.Id == voquId);
            var hasUserVotedOnVoqu = voqu.Votes.Any(x => x.GivenBy == userId);

            if (!hasUserVotedOnVoqu)
            {
                voqu.Upvote(userId);
            }
            else
            {
                voqu.RemoveVote(userId);
            }

            return classroom;
        }
    }
}