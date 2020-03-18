using APBD3.Models;
using System.Collections.Generic;

namespace APBD3.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
    }
}
