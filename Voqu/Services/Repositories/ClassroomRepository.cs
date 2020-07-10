using System;
using System.Collections.Generic;
using System.Linq;
using Voqu.Models;

namespace Voqu.Services.Providers
{
    public class ClassroomRepository : IClassroomRepository
    {
        public List<Classroom> ActiveClassrooms { get; set; }

        public ClassroomRepository()
        {
            ActiveClassrooms = new List<Classroom>();
        }

        public Classroom GetClassroomByAccessCode(string accessCode)
        {
            var parsedValue = 0l;

            if (long.TryParse(accessCode, out parsedValue))
            {
                return ActiveClassrooms.FirstOrDefault((x) => x.AccessCode == parsedValue);
            }

            return null;
        }

        public Classroom CreateClassroom(string name, string createdBy)
        {
            var newClassroom = new Classroom()
            {
                CreatedBy = createdBy,
                Active = true,
                Created = DateTime.Now,
                Name = name,
                Voqus = new List<Models.Voqu>(),
                AccessCode = GenerateAccessCode()
            };

            ActiveClassrooms.Add(newClassroom);

            return newClassroom;
        }

        private long GenerateAccessCode()
        {
            Random random = new Random();

            var accessCode = 0;
            var isUnique = false;

            while (!isUnique)
            {
                accessCode = random.Next(10000);
                isUnique = ActiveClassrooms.FirstOrDefault(x => x.AccessCode == accessCode) == null;
            }

            return accessCode;
        }
    }
}