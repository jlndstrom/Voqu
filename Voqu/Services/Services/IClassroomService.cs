using Voqu.Models;

namespace Voqu.Services.Services
{
    public interface IClassroomService
    {
        Classroom CreateQuestion(string accessCode, string question);

        Classroom DeleteQuestion(string accessCode, long voquId);

        Classroom GetClassroom(string accessCode);

        Classroom Vote(string accessCode, long voquId, string userId);
    }
}