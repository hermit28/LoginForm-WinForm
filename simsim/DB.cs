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

        public void InsertUserInfo(string username, string password, string phoneNumber, string birth, string gender)
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
            PhoneNumber,
            Birth,
            Gender
            )
    VALUES 
        (@Username,
        @Password,
        @PhoneNumber,
        @Birth,
        @Gender
        ); ";

                parameter = new C.SqlParameter("@Username", D.SqlDbType.VarChar, 25);
                parameter.Value = username;
                command.Parameters.Add(parameter);

                parameter = new C.SqlParameter("@Password", D.SqlDbType.VarChar, 20);
                parameter.Value = password;
                command.Parameters.Add(parameter);

                parameter = new C.SqlParameter("@PhoneNumber", D.SqlDbType.VarChar, 20);
                parameter.Value = phoneNumber;
                command.Parameters.Add(parameter);

                parameter = new C.SqlParameter("@Birth", D.SqlDbType.VarChar, 50);
                parameter.Value = birth;
                command.Parameters.Add(parameter);

                parameter = new C.SqlParameter("@Gender", D.SqlDbType.VarChar, 10);
                parameter.Value = gender;
                command.Parameters.Add(parameter);

                command.ExecuteScalar();
            }
        }
    }
}
