using System.Data.SqlClient;

namespace DotNetAssignmentMVC.DataAccess
{
    public class DbHelper
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public DbHelper(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
