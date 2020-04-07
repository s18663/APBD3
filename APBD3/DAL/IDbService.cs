using APBD3.Models;
using System.Collections.Generic;

namespace APBD3.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
        public void SetStudents(List<Student> students);
        public string GetStudies(Student student);
        bool CheckIndex(string index);
    }
}
