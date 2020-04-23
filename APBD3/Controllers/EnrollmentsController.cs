using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using APBD3.DTOs.Requests;
using APBD3.DTOs.Responses;
using APBD3.Models;
using APBD3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace APBD3.Controllers
{
    [Route("api/enrollments")]
    [Authorize(Roles = "employee")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private IStudentDbService _service;
        public IConfiguration Configuration { get; set; }

        public EnrollmentsController(IConfiguration config,IStudentDbService service)
        {
            
            _service = service;
            Configuration = config;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult LoginEmployee(LoginRequestcs request)
        {
            if (_service.CheckCred(request) <= 0)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,request.login),
                new Claim(ClaimTypes.Role,"employee")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    issuer: "s18663",
                    audience: "Students",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: creds
                );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = Guid.NewGuid()
            });
        }
        
        [HttpPost("refreshToken/{refreshToken}")]
        [AllowAnonymous]
        public IActionResult RefreshToken(string refresh)
        {
            string login = _service.CheckRefresh(refresh);
            if (login == "") return Unauthorized();
            return Ok(CreateToken(login));
        }

        public object CreateToken(string login)
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
                accesstoken = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = Guid.NewGuid()//
            };

        }

        [Route ("api/enrollments")]
        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {

            var result =_service.EnrollStudent(request);

            if (result == 0)
            {
                return BadRequest();
            }
            else
            {
                return Created("",_service.EnrollStudent(request));
            }
            
        }
        /*
        [Route("api/enrollments/promotions")]
        [HttpPost]
        public IActionResult PromoteStudent(PromoteStudentRequest request)
        {
            var result = _service.PromoteStudents(request);

            if (result == -1)
                return NotFound();
            else


        }
        */
        
    }
}