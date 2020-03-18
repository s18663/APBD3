using APBD3.Models;
using System.Collections.Generic;

namespace APBD3.DAL
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> _students;

        static MockDbService()
        {
            _students = new List<Student>
            {
                new Student{IdStudent=1,FirstName="Maja",LastName="Maj" },
                new Student{IdStudent=2,FirstName="Karolina",LastName="Koral" },
                new Student{IdStudent=3,FirstName="Zosia",LastName="Wrzos" },
            };
        }

        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }
    }
}
