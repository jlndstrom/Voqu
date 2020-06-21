using System;
using System.Collections.Generic;
using Voqu.Models;

namespace Voqu.Services.Factories.ClassroomFactory
{
    public class ClassroomFactory : IClassroomFactory
    {
        public ClassroomFactory() { }

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
                VotedQuestions = new List<Models.Voqu>()
            };

            return newClassroom;
        }

        public void TeardownClassroom(Classroom classroom)
        {
            classroom = null;
        }
    }
}
