using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Voqu.Models
{
    public class ClassroomViewModel : IViewModel
    {
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string AccessCode { get; set; }
        public List<Voqu> VotedQuestions { get; set; }
        public RoleTypes RoleType { get; set; }
        public string NewQuestion { get; set; }
    }
}
