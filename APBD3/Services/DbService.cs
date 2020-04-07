using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APBD3.Services
{
    public class DbService : IDbServ
    {
        public bool CheckIndex(string index)
        {
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18663;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                con.Open();
                com.Connection = con;

                com.CommandText = "select count(1) from student where indexnumber=@index";
                com.Parameters.AddWithValue("index", index);

                var dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    return false;
                }

            }
               return true;
        }
    }
}
