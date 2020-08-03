using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using Voqu.Models;
using Voqu.Services.Mappers;
using Voqu.Services.Providers;

namespace Voqu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClassroomRepository _classroomRepository;
        private readonly IMapper<Classroom, ClassroomViewModel> _classroomMapper;

        public HomeController(ILogger<HomeController> logger, IClassroomRepository classroomRepository, IMapper<Classroom, ClassroomViewModel> classroomMapper)
        {
            _logger = logger;
            _classroomRepository = classroomRepository;
            _classroomMapper = classroomMapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateClassroom(HomeViewModel model)
        {
            var classroom = _classroomRepository.CreateClassroom(model.NewClassroomName, GetUserId());
            var viewModel = SetupViewModel(GetUserId(), classroom);

            return View("Classroom", viewModel);
        }

        public IActionResult JoinClassroom(HomeViewModel model)
        {
            string userId = GetUserId();
            var classroom = _classroomRepository.GetClassroomByAccessCode(model.ClassroomAccessCode);

            if (classroom == null)
            {
                model.ErrorMessage = "There is no active classroom with the given access code. Please try again.";
                return View("Index", model);
            }

            ClassroomViewModel viewModel = SetupViewModel(userId, classroom);

            return View("Classroom", viewModel);
        }

        private ClassroomViewModel SetupViewModel(string userId, Classroom classroom)
        {
            var viewModel = _classroomMapper.Map(classroom);
            viewModel.RoleType = SetRoleType(userId, classroom);

            return viewModel;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string GetUserId()
        {
            var userId = HttpContext.Session.GetString("userId");

            if (userId == null)
            {
                userId = Guid.NewGuid().ToString();
                HttpContext.Session.SetString("userId", userId);
            }

            return userId;
        }

        private RoleTypes SetRoleType(string currUserId, Classroom classroom)
        {
            if (currUserId == classroom.CreatedBy)
            {
                return RoleTypes.Presenter;
            }
            else
            {
                return RoleTypes.Participant;
            }
        }
    }
}