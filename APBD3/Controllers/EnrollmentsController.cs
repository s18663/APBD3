using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using APBD3.DTOs.Requests;
using APBD3.DTOs.Responses;
using APBD3.Models;
using APBD3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBD3.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private IStudentDbService _service;

        public EnrollmentsController(IStudentDbService service)
        {
            _service = service;
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

        [Route("api/enrollments/promotions")]
        [HttpPost]
        public IActionResult PromoteStudent(PromoteStudentRequest request)
        {
            var result = _service.PromoteStudents(request);

            if (result == -1)
                return NotFound();
            else


        }
        
    }
}