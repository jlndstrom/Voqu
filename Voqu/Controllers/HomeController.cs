using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Voqu.Models;
using Voqu.Services.Factories.ClassroomFactory;
using Voqu.Services.Mappers;
using Voqu.Services.Providers;

namespace Voqu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClassroomFactory _classroomFactory;
        private readonly IMapper<Classroom, ClassroomViewModel> _classroomMapper;
        private readonly IClassroomProvider _classroomProvider;

        public HomeController(ILogger<HomeController> logger, IClassroomFactory classroomFactory, IMapper<Classroom, ClassroomViewModel> classroomMapper, IClassroomProvider classroomProvider)
        {
            _logger = logger;
            _classroomFactory = classroomFactory;
            _classroomMapper = classroomMapper;
            _classroomProvider = classroomProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateClassroom(HomeViewModel model)
        {
            var newClassroom = _classroomFactory.CreateClassroom(model.NewClassroomName);

            _classroomProvider.ActiveClassrooms.Add(newClassroom);
            var viewModel = _classroomMapper.Map(newClassroom);
            viewModel.RoleType = RoleTypes.Presenter;

            return View("Classroom", viewModel);
        }

        public IActionResult JoinClassroom(HomeViewModel model)
        {
            var requestedClassroom = _classroomProvider.GetClassroomByAccessCode(model.ClassroomAccessCode);

            if (requestedClassroom == null)
            {
                model.ErrorMessage = "There is no active classroom with the given access code. Please try again.";
                return View("Index", model);
            }

            var viewModel = _classroomMapper.Map(requestedClassroom);

            viewModel.RoleType = RoleTypes.Participant;
           
            return View("Classroom", viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
