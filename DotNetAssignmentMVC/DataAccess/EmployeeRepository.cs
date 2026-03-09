using DotNetAssignmentMVC.Models;
using System.Data;
using System.Data.SqlClient;

namespace DotNetAssignmentMVC.DataAccess
{
    public class EmployeeRepository
    {
        private readonly string _connection;

        public EmployeeRepository(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("DefaultConnection");
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connection);
        }

        // Get EmployeeId By UserId
        public int GetEmployeeId(int userId)
        {
            using (SqlConnection con = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SP_GetEmployeeId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // Dashboard Details
        public EmployeeDashboardViewModel GetDashboard(int employeeId)
        {
            EmployeeDashboardViewModel model = new();

            using (SqlConnection con = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SP_EmployeeDashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    model.TotalLeaves = Convert.ToInt32(dr["TotalLeaves"]);
                    model.PendingLeaves = Convert.ToInt32(dr["PendingLeaves"]);
                    model.ApprovedLeaves = Convert.ToInt32(dr["ApprovedLeaves"]);
                    model.RejectedLeaves = Convert.ToInt32(dr["RejectedLeaves"]);
                }
            }

            model.LeaveHistory = GetLeaveHistory(employeeId);
            return model;
        }

        // Leave History
        public List<LeaveRequest> GetLeaveHistory(int employeeId)
        {
            List<LeaveRequest> list = new();

            using (SqlConnection con = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SP_GetEmployeeLeaves", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new LeaveRequest
                    {
                        LeaveId = Convert.ToInt32(dr["LeaveId"]),
                        FromDate = Convert.ToDateTime(dr["FromDate"]),
                        ToDate = Convert.ToDateTime(dr["ToDate"]),
                        Reason = dr["Reason"].ToString(),
                        Status = dr["Status"].ToString(),
                        CreatedDate = Convert.ToDateTime(dr["CreatedDate"])
                    });
                }
            }

            return list;
        }

        // Apply Leave
        public string ApplyLeave(LeaveRequest leaveR)
        {
            try
            {
                using (SqlConnection con = GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SP_ApplyLeave", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmployeeId", leaveR.EmployeeId);
                    cmd.Parameters.AddWithValue("@FromDate", leaveR.FromDate);
                    cmd.Parameters.AddWithValue("@ToDate", leaveR.ToDate);
                    cmd.Parameters.AddWithValue("@Reason", leaveR.Reason);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
