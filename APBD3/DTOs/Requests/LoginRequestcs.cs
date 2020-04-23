using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APBD3.DTOs.Requests
{
    public class LoginRequestcs
    {
        [RegularExpression("s^[0-9]+$")]
        public string login { get; set; }
        public string pass { get; set; }

    }
}
