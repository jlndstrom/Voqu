using Voqu.Models;

namespace Voqu.Services.Services
{
    public interface IClassroomService
    {
        ClassroomViewModel CreateQuestion(string accessCode, string question, string userId);

        ClassroomViewModel DeleteQuestion(string accessCode, long voquId, string userId);

        ClassroomViewModel GetClassroom(string accessCode, string userId);

        ClassroomViewModel Vote(string accessCode, long voquId, string userId);
    }
}