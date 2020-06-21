using System.Collections.Generic;
using Voqu.Models;

namespace Voqu.Services.Providers
{
    public interface IClassroomProvider
    {
        List<Classroom> ActiveClassrooms { get; set; }
        public Classroom GetClassroomByAccessCode(string accessCode);
    }
}