using APBD3.Models;
using System.Collections.Generic;

namespace APBD3.DAL
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> _students;

        static MockDbService()
        {

        }

        public void SetStudents(List<Student> students)
        {
            _students = students;
        }

        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }

        public string GetStudies(Student student)
        {
            return student.Studies;//" "+student.Semester;
        }

        public bool CheckIndex(string index)
        {
            throw new System.NotImplementedException();
        }
    }
}
