using APBD3.DTOs.Requests;
using APBD3.DTOs.Responses;
using APBD3.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APBD3.Services
{
    public class SqlServerStudentDbService : IStudentDbService
    {
        public int EnrollStudent(EnrollStudentRequest request)
        {
            var st = new Student();
            st.FirstName = request.FirstName;

            var response = new EnrollStudentResponse();

            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18663;Integrated Security=True"))
            using (var com = new SqlCommand())
            {

                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();

                try
                {
                    com.CommandText = "select IdStudy from studies where name=@name";
                    com.Parameters.AddWithValue("name", request.Studies);

                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        tran.Rollback();
                        return 0;

                    }
                    dr.Close();

                    int idStudies = (int)dr["IdStudies"];

                    com.CommandText = "select ISNULL(max(IdEnrollment),0) from enrollment";
                    dr = com.ExecuteReader();
                    int idEnroll = (int)dr["ISNULL(max(IdEnrollment),0)"] + 1;

                    com.CommandText = "select IdEnrollment from Enrollment where idStudy=@idstud AND semester=1";
                    com.Parameters.AddWithValue("idstud",idStudies);
                    dr = com.ExecuteReader();
                    if (!dr.Read())
                    {   
                        com.CommandText = "INSERT INTO enrollment(IdEnrollment,Semester,IdStudy,StartDate) VALUES(@id,@sem,@idstudy,@start)";
                        com.Parameters.AddWithValue("id", idEnroll);
                        com.Parameters.AddWithValue("@sem", 1);
                        com.Parameters.AddWithValue("@idstudy", idStudies);
                        com.Parameters.AddWithValue("@start", DateTime.Today);

                    }

                    com.CommandText = "INSERT INTO student(IndexNumber,FirstName,LastName,BirthDate,IdEnrollment) VALUES(@index,@name,@lname,@bd,@en";
                    com.Parameters.AddWithValue("index", request.IndexNumber);
                    com.Parameters.AddWithValue("name", request.FirstName);
                    com.Parameters.AddWithValue("Lname", request.LastName);
                    com.Parameters.AddWithValue("bd", request.BirthDate);
                    com.Parameters.AddWithValue("en", idEnroll);

                    com.ExecuteNonQuery();


                    tran.Commit();

                }
                catch (SqlException exc)
                {
                    tran.Rollback();
                    return 0;
                }
            }

            return 1;

        }

        public int PromoteStudents(PromoteStudentRequest request)
        {
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18663;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                con.Open();
                com.Connection = con;

                com.CommandText="select count(1) from enrollment join studies on enrollment.idstudy=studies.idstudy where studies.name=@study and enrollment.semester=@sem";
                com.Parameters.AddWithValue("study", request.Studies);
                com.Parameters.AddWithValue("sem", request.Semester);

                var dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    return -1;
                }
                dr.Close();

                com.CommandText = "Exec proc @study,@sem";
                com.Parameters.AddWithValue("study", request.Studies);
                com.Parameters.AddWithValue("sem", request.Semester);
                com.ExecuteNonQuery();

                com.CommandText = "select ISNULL(max(IdEnrollment)+1,0) from enrollment join studies on enrollment.idstudy=studies.idstudy where studies.name=@study and enrollment.semester=@sem";
                com.Parameters.AddWithValue("study", request.Studies);
                com.Parameters.AddWithValue("sem", request.Semester);
                dr = com.ExecuteReader();
                int idEnroll = (int)dr["ISNULL(max(IdEnrollment)+1,0)"];

                dr = com.ExecuteReader();
                if (!dr.Read()){
                    return -1;
                }

                com.CommandText = "update enrollment set semester = @sem where idenrollment=@iden";
                com.Parameters.AddWithValue("sem", request.Semester + 1);
                com.Parameters.AddWithValue("iden", idEnroll);

                return 1;
            }


        }
    }
}
