using APBD3.DTOs.Requests;
using APBD3.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD3.Services
{
    public interface IStudentDbService
    {
        void AddRefreshToken(Guid newToken,string login);
        string CheckRefresh(string refresh);
        int CheckCred(LoginRequestcs request);
        int EnrollStudent(EnrollStudentRequest request);
        int PromoteStudents(PromoteStudentRequest request);
    }
}
