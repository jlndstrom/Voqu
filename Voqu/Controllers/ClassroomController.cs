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

        public ClassroomController(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }

        public IActionResult Index()
        {
            return View("Classroom");
        }

        public IActionResult Index(ClassroomViewModel viewModel)
        {
            return View("Classroom", viewModel);
        }

        public IActionResult UpdateVoqus(string accesssCode)
        {
            return PartialView("Voqus", _classroomService.GetClassroom(accesssCode, GetUserId()));
        }

        public IActionResult Vote(string accessCode, long voquId)
        {
            return View("Classroom", _classroomService.Vote(accessCode, voquId, GetUserId()));
        }

        public IActionResult CreateQuestion(ClassroomViewModel model)
        {
            return View("Classroom", _classroomService.CreateQuestion(model.AccessCode, model.Question, GetUserId()));
        }

        public IActionResult DeleteQuestion(string accessCode, long voquId)
        {
            return View("Classroom", _classroomService.DeleteQuestion(accessCode, voquId, GetUserId()));
        }

        private string GetUserId()
        {
            return HttpContext.Session.GetString("userId");
        }
    }
}