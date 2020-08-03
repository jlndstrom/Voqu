using Microsoft.AspNetCore.Http;
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
        private readonly IMapper<Classroom, ClassroomViewModel> _classroomMapper;


        public ClassroomService(IClassroomRepository classroomRepository, IMapper<Classroom, ClassroomViewModel> classroomMapper)
        {
            _classroomRepository = classroomRepository;
            _classroomMapper = classroomMapper;
        }

        public ClassroomViewModel CreateQuestion(string accessCode, string question, string userId)
        {
            var classroom = _classroomRepository.GetClassroomByAccessCode(accessCode);
            classroom.Voqus.Add(new Models.Voqu() { Question = question, Id = classroom.Voqus.Count + 1 });
            var viewModel = SetupViewModel(classroom, userId);
            viewModel.Question = "";

            return viewModel;
        }

        public ClassroomViewModel DeleteQuestion(string accessCode, long voquId, string userId)
        {
            var classroom = _classroomRepository.GetClassroomByAccessCode(accessCode);
            classroom.Voqus.Remove(classroom.Voqus.FirstOrDefault(x => x.Id == voquId));

            return SetupViewModel(classroom, userId);
        }

        public ClassroomViewModel GetClassroom(string accessCode, string userId)
        {
            var classroom = _classroomRepository.GetClassroomByAccessCode(accessCode);

            return SetupViewModel(classroom, userId);
        }

        public ClassroomViewModel Vote(string accessCode, long voquId, string userId)
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

            return SetupViewModel(classroom, userId);
        }

        private ClassroomViewModel SetupViewModel(Classroom currClassroom, string userId)
        {
            var viewModel = _classroomMapper.Map(currClassroom);

            viewModel.Voqus.ForEach(x =>
            {
                x.Deletable = x.CreatedBy == userId;
                x.HasVoted = x.Votes.Any(x => x.GivenBy == userId);
            });

            viewModel.RoleType = currClassroom.CreatedBy == userId ? RoleTypes.Presenter : RoleTypes.Participant;

            return viewModel;
        }
    }
}