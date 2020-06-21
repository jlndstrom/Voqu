using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Voqu.Models
{
    public class Voqu
    {
        public long Id { get; set; }
        public int Votes { get; set; }
        public string Question { get; set; }

        public void Upvote() 
        { 
            Votes++; 
        }

        public void Downvote() 
        { 
            Votes--; 
        }
    }
}
