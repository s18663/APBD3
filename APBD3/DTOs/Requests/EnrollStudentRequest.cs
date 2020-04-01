﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APBD3.DTOs.Requests
{
    public class EnrollStudentRequest
    {
        [Required(ErrorMessage = "Musisz podać index")]
        [RegularExpression("s^[0-9]+$")]
        public string IndexNumber { get; set; }
        [Required(ErrorMessage ="Musisz podać imię")]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Musisz podać nazwisko")]
        [MaxLength(200)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Musisz podać datę urodzenia")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Musisz podać kierunek")]
        public string Studies { get; set; }
    }
}
