using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using APBD3.DAL;
using APBD3.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {



        /*  -----------------------------------------------CW3 i 4

        private readonly IDbService _dbService;

        public StudentController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18663;Integrated Security=True"))
            using (var com=new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "SELECT FirstName, LastName, BirthDate, Name,Semester FROM STUDENT JOIN ENROLLMENT ON STUDENT.IdEnrollment=Enrollment.IdEnrollment JOIN STUDIES ON STUDIES.IdStudy = Enrollment.IdStudy;";

                List<Student> _students = new List<Student>();
                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = dr["BirthDate"].ToString();
                    st.Studies = dr["Name"].ToString();
                    st.Semester = dr["Semester"].ToString();
                    _students.Add(st);
                    _dbService.SetStudents(_students);
                }
               

               
            }
            return Ok(_dbService.GetStudents());
        }

        [HttpGet("{id}")]
        public IActionResult GetStudies(string id)
        {
            var st = new Student();

            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18663;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = $"SELECT Name,SEMESTER FROM STUDENT JOIN ENROLLMENT ON STUDENT.IdEnrollment=Enrollment.IdEnrollment JOIN STUDIES ON STUDIES.IdStudy = Enrollment.IdStudy WHERE Student.IndexNumber =" + "'"+id+"'";
                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    st.Semester = dr["Semester"].ToString();
                    st.Studies = dr["Name"].ToString();
                }
            }

            return Ok(_dbService.GetStudies(st));

        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            //add
            //gen
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult PutStudent(int id)
        {
            return Ok("Aktualizacja dokończona");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok("Usuwanie zakończone");
        }
        */
    }

   
}