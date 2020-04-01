using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APBD3.DTOs.Requests
{
    public class PromoteStudentRequest
    {
        [Required(ErrorMessage = "Musisz podać kierunek")]
        public string Studies { get; set; }
        [Required(ErrorMessage = "Musisz podać semestr")]
        public string Semester { get; set; }
    }
}
