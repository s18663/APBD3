using APBD3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD3.Services
{
    public class ContextStudentDbService : IStudentDbService
    {
        DBStudentContext db;
        public ContextStudentDbService(DBStudentContext context)
        {
            db = context;
        }

        public void EnrollStudent(Student student)
        {
            if (!db.Enrollment.Any(x => x.IdEnrollment == student.IdEnrollment))
            {

                var enroll = new Enrollment
                {
                    IdStudy = 1,
                    Semester = 1,
                    StartDate = DateTime.Now
                };

                db.Add(enroll);
                db.SaveChanges();

                student.IdEnrollment = enroll.IdEnrollment;
            }



            db.Add(student);
            db.SaveChanges();
        }

        public IEnumerable<Student> GetStudents()
        {
            return db.Student.ToList();
        }

        public void ModifyStudent(Student student)
        {
            db.Update(student);
            db.SaveChanges();
        }

        public void PromoteStudents(Enrollment enrollment)
        {
            var newEnrollment = new Enrollment
            {
                Semester = enrollment.Semester + 1,
                StartDate = DateTime.Now
            };



            db.Add(newEnrollment);
            db.SaveChanges();


            foreach (var student in db.Student.Where(x => x.IdEnrollment == enrollment.IdEnrollment).ToList())
                student.IdEnrollment = newEnrollment.IdEnrollment;

            db.SaveChanges();
        }

        public void RemoveStudent(Student student)
        {
            if (!db.Enrollment.Any(x => x.IdEnrollment == student.IdEnrollment))
            {

                var enroll = new Enrollment
                {
                    IdStudy = 1,
                    Semester = 1,
                    StartDate = DateTime.Now
                };

                db.Add(enroll);
                db.SaveChanges();

                student.IdEnrollment = enroll.IdEnrollment;
            }



            db.Add(student);
            db.SaveChanges();
        }
    }
    
}
