using DotNetAssignmentMVC.Models;
using System.Data;
using System.Data.SqlClient;

namespace DotNetAssignmentMVC.DataAccess
{

    public class AuthRepository
    {
        private readonly string _connection;

        public AuthRepository(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("DefaultConnection");
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connection);
        }

        public Users Login(LoginViewModel loginObj)
        {
            using (SqlConnection con = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SP_Login", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", loginObj.Email);
                cmd.Parameters.AddWithValue("@Password", loginObj.Password);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return new Users
                    {
                        UserId = Convert.ToInt32(dr["UserId"]),
                        FullName = dr["FullName"].ToString(),
                        Email = dr["Email"].ToString(),
                        Role = dr["Role"].ToString()
                    };
                }
            }

            return null;
        }
    }
}
