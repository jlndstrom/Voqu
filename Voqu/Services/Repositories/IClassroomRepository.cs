using System.Collections.Generic;
using Voqu.Models;

namespace Voqu.Services.Providers
{
    public interface IClassroomRepository
    {
        List<Classroom> ActiveClassrooms { get; set; }

        Classroom CreateClassroom(string name, string createdBy);

        Classroom GetClassroomByAccessCode(string accessCode);
    }
}