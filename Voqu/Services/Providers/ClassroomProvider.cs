using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voqu.Models;

namespace Voqu.Services.Providers
{
    public class ClassroomProvider : IClassroomProvider
    {
        public ClassroomProvider()
        {
            ActiveClassrooms = new List<Classroom>();
        }

        public Classroom GetClassroomByAccessCode(string accessCode)
        {
            return ActiveClassrooms.FirstOrDefault((x) => x.AccessCode == accessCode);
        }

        public List<Classroom> ActiveClassrooms { get; set; }
    }
}
