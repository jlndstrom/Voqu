using System;

namespace Voqu.Models
{
    public class Vote
    {
        public Vote(string participantId)
        {
            GivenBy = participantId;
        }

        public string GivenBy { get; set; }
    }
}