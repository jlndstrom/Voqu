﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Voqu.Models
{
    public class Classroom : IMappable
    {
        public string CreatedBy { get; set; }
        public long AccessCode { get; set; }
        public DateTime Created { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public long Participants { get; set; }
        public List<Voqu> Voqus { get; set; }
    }
}
