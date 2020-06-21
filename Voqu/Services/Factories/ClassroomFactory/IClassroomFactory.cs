using Voqu.Models;

namespace Voqu.Services.Factories.ClassroomFactory
{
    public interface IClassroomFactory
    {
        Classroom CreateClassroom(string name);
        void TeardownClassroom(Classroom classroom);
    }
}