﻿

using System;

namespace APBD3.Models
{
    public class Student
    {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int IdEnrollment { get; set; }
       // public string Studies { get; set; }

        public virtual Enrollment IdEnrollmentNavigation { get; set; }

    }
}
