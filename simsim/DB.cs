using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using D = System.Data;
using C = System.Data.SqlClient;

namespace simsim
{
    class DB
    {
        private C.SqlConnection connection;
        private string username;
        private string password;

        public void DBConnection()
        {
            connection = new C.SqlConnection(
                "Server=tcp:simsim.database.windows.net,1433;Database=SimsimTest;User ID=simsim;Password=s1ms1mdb!;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                );
            {
                connection.Open();
            }
        }

        public bool SearchUserInfo(string username, string password)
        {
            using (var command = new C.SqlCommand())
            {
                command.Connection = connection;
                command.CommandType = D.CommandType.Text;
                command.CommandText = @"  
    SELECT  
        Username,  
        Password  
    FROM  
        dbo.Users; ";

                C.SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    this.username = reader.GetString(0);
                    this.password = reader.GetString(1);

                    if(username.Equals(this.username) && password.Equals(this.password))
                    {
                        reader.Close();
                        return true;
                    }
                }

                reader.Close();
                return false;
            }
        }

        public void InsertRows()
        {
            C.SqlParameter parameter;

            using (var command = new C.SqlCommand())
            {
                command.Connection = connection;
                command.CommandType = D.CommandType.Text;
                command.CommandText = @"
    INSERT INTO 
        dbo.Users
            (Username,
            Password,
            Gender,
            PhoneNumber
            )
    VALUES 
        (@Username,
        @Password,
        @Gender,
        PhoneNumber
        ); ";

                parameter = new C.SqlParameter("@Username", D.SqlDbType.VarChar, 25);
                parameter.Value = "t5";
                command.Parameters.Add(parameter);

                parameter = new C.SqlParameter("@Password", D.SqlDbType.VarChar, 25);
                parameter.Value = "t5";
                command.Parameters.Add(parameter);

                command.ExecuteScalar();
            }
        }
    }
}
