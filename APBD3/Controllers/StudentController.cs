using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APBD3.DAL;
using APBD3.DTOs.Requests;
using APBD3.Models;
using APBD3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace APBD3.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {

        private readonly IStudentDbService _context;

        public StudentController(IStudentDbService context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetStudents()
        {
            try
            {
                return Ok(_context.GetStudents());
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [HttpPost]
        public IActionResult ModifyStudent(Student student)
        {
            try
            {
                _context.ModifyStudent(student);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [HttpDelete]
        public IActionResult RemoveStudent(Student student)
        {
            try
            {
                _context.RemoveStudent(student);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }


        [Route("enroll")]
        [HttpPost]
        public IActionResult EnrollStudent(Student student)
        {
            try
            {
                _context.EnrollStudent(student);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [Route("promote")]
        [HttpPost]
        public IActionResult PromoteStudents(Enrollment enrollment)
        {
            try
            {
                _context.PromoteStudents(enrollment);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }



        /*
        public IConfiguration Configuration { get; set; }
        public IStudentDbService _dbService;

        public StudentController(IConfiguration configuration,IStudentDbService dbService)
        {
            Configuration = configuration;
            _dbService = dbService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(LoginRequestcs request)
        {

            if (_dbService.CheckCred(request) <= 0)
            {
                return Unauthorized();
            }

            return Ok(CreateToken(request.login));
        }

        [HttpPost("refreshToken/{refreshToken}")]
        [AllowAnonymous]
        public IActionResult RefreshToken(string refresh)
        {
            string login = _dbService.CheckRefresh(refresh);
            if (login == "") return Unauthorized();
            return Ok(CreateToken(login));
        }

        public  object CreateToken(string login)
        {

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,login),
                new Claim(ClaimTypes.Role,"student")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "s18663",
                audience: "Students",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
                );

            var refToken = Guid.NewGuid();
            

            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = Guid.NewGuid()//
            };

        }

       
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