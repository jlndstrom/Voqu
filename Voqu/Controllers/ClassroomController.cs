using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using System.Linq;
using Voqu.Models;
using Voqu.Services.Mappers;
using Voqu.Services.Services;

namespace Voqu.Controllers
{
    public class ClassroomController : Controller
    {
        private readonly IClassroomService _classroomService;
        private readonly IMapper<Classroom, ClassroomViewModel> _classroomMapper;

        public ClassroomController(IClassroomService classroomService, IMapper<Classroom, ClassroomViewModel> classroomMapper)
        {
            _classroomService = classroomService;
            _classroomMapper = classroomMapper;
        }

        public IActionResult Index()
        {
            return View("Classroom");
        }

        public IActionResult Index(ClassroomViewModel viewModel)
        {
            return View("Classroom", viewModel);
        }

        public IActionResult UpdateVoqus(string accessCode)
        {
            var classroom = _classroomService.GetClassroom(accessCode);
            var viewModel = SetupViewModel(classroom, getUserId());

            return PartialView("Voqus", viewModel);
        }

        public IActionResult Vote(string accessCode, long voquId)
        {
            var classroom = _classroomService.Vote(accessCode, voquId, getUserId());
            var viewModel = SetupViewModel(classroom, getUserId());

            return View("Classroom", viewModel);
        }

        public IActionResult CreateQuestion(ClassroomViewModel model)
        {
            var classroom = _classroomService.CreateQuestion(model.AccessCode, model.Question);
            var viewModel = SetupViewModel(classroom, getUserId());
            viewModel.Question = "";

            return View("Classroom", viewModel);
        }

        public IActionResult DeleteQuestion(string accessCode, long voquId)
        {
            var classroom = _classroomService.DeleteQuestion(accessCode, voquId);
            var viewModel = SetupViewModel(classroom, getUserId());

            return View("Classroom", viewModel);
        }

        private ClassroomViewModel SetupViewModel(Classroom currClassroom, string userId)
        {
            var viewModel = _classroomMapper.Map(currClassroom);

            viewModel.Voqus.ForEach(x =>
            {
                x.Deletable = x.CreatedBy == userId || viewModel.RoleType == RoleTypes.Presenter;
                x.HasVoted = x.Votes.Any(x => x.GivenBy == userId);
            });

            return viewModel;
        }

        private string getUserId()
        {
            return HttpContext.Session.GetString("userId");
        }
    }
}