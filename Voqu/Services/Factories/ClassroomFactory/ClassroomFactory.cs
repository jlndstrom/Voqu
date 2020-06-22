using System;
using System.Collections.Generic;
using System.Linq;
using Voqu.Models;
using Voqu.Services.Providers;

namespace Voqu.Services.Factories.ClassroomFactory
{
    public class ClassroomFactory : IClassroomFactory
    {
        private readonly IClassroomProvider _classroomProvider;

        public ClassroomFactory(IClassroomProvider classroomProvider)
        {
            _classroomProvider = classroomProvider;
        }

        public Classroom CreateClassroom(String name)
        {
            var newClassroom = new Classroom()
            {
                Active = true,
                Created = DateTime.Now,
                Name = name,
                Voqus = new List<Models.Voqu>(),
                AccessCode = GenerateAccessCode()
            };

            return newClassroom;
        }

        public void TeardownClassroom(Classroom classroom)
        {
            classroom = null;
        }

        public long GenerateAccessCode()
        {
            Random random = new Random();

            var accessCode = 0;
            var isUnique = false;

            while(!isUnique)
            {
                accessCode = random.Next(10000);
                isUnique = _classroomProvider.ActiveClassrooms.FirstOrDefault(x => x.AccessCode == accessCode) == null;
            }
            
            return accessCode;
        }
    }
}
