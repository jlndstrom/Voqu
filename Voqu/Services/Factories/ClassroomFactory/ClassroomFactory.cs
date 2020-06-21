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
            // TODO: Should be controlled by database, just for testing purposes for now
            Program.amtOfCurrSessions++;

            var newClassroom = new Classroom()
            {
                Id = Program.amtOfCurrSessions,
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
