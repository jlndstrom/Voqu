using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Voqu.Models;
using Voqu.Services.Mappers;
using Voqu.Services.Providers;

namespace Voqu.Controllers
{
    public class ClassroomController : Controller
    {
        private readonly IClassroomProvider _classroomProvider;
        private readonly IMapper<Classroom, ClassroomViewModel> _classroomMapper;


        public ClassroomController(IClassroomProvider classroomProvider, IMapper<Classroom, ClassroomViewModel> classroomMapper)
        {
            _classroomProvider = classroomProvider;
            _classroomMapper = classroomMapper;
        }

        public IActionResult Index(ClassroomViewModel model)
        {
            return View("Classroom", model);
        }

        public IActionResult UpdateVoqus(ClassroomViewModel viewModel)
        {
            var currClassroom = _classroomProvider.GetClassroomByAccessCode(viewModel.AccessCode);
            var updatedViewModel = _classroomMapper.Map(currClassroom);
            return PartialView("Voqus", updatedViewModel);
        }

        public IActionResult Vote(string accessCode, long voquId)

        {
            var currClassroom = _classroomProvider.GetClassroomByAccessCode(accessCode);
            var selectedVoqu = currClassroom.VotedQuestions.FirstOrDefault(x => x.Id == voquId);
            var sessionIdentifierForVoqu = "hasVotedOnVoqu_" + voquId;

            if (HttpContext.Session.GetString(sessionIdentifierForVoqu) == "true")
            {
                selectedVoqu.Downvote();
                HttpContext.Session.Remove(sessionIdentifierForVoqu);
            }
            else
            {
                selectedVoqu.Upvote();
                HttpContext.Session.SetString(sessionIdentifierForVoqu, "true");
            }

            var updatedModel = _classroomMapper.Map(currClassroom);

            return View("Classroom", updatedModel);
        }

        public IActionResult CreateQuestion(ClassroomViewModel model)
        {
            var currClassroom = _classroomProvider.GetClassroomByAccessCode(model.AccessCode);
            currClassroom.VotedQuestions.Add(new Models.Voqu() { Question = model.NewQuestion, Votes = 0, Id = currClassroom.VotedQuestions.Count + 1 });
            var updatedModel = _classroomMapper.Map(currClassroom);
            updatedModel.NewQuestion = "";

            return View("Classroom", updatedModel);
        }
    }
}