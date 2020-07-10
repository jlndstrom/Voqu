using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Voqu.Models
{
    public class VoquViewModel
    {
        public long Id { get; set; }
        public List<Vote> Votes { get; set; }
        public string Question { get; set; }
        public bool HasVoted { get; set; }
        public bool Deletable { get; set; }
        public string CreatedBy { get; set; }

        public VoquViewModel()
        {
            Votes = new List<Vote>();
        }
    }
}
