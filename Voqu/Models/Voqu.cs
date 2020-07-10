using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Voqu.Models
{
    public class Voqu
    {
        public long Id { get; set; }
        public List<Vote> Votes { get; set; }
        public string Question { get; set; }
        public string CreatedById { get; set; }

        public Voqu()
        {
            Votes = new List<Vote>();
        }

        public void Upvote(string participantId)
        {
            Votes.Add(new Vote(participantId));
        }

        public void RemoveVote(string participantId)
        {
            Votes.Remove(Votes.FirstOrDefault(x => x.GivenBy == participantId));
        }
    }
}