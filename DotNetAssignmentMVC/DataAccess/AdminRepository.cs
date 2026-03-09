using DotNetAssignmentMVC.Models;
using System.Data;
using System.Data.SqlClient;

namespace DotNetAssignmentMVC.DataAccess
{
    public class AdminRepository
    {
        private readonly string _connection;

        public AdminRepository(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("DefaultConnection");
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connection);
        }
        //Get Dashboard
        public AdminDashboardViewModel GetDashboardData()
        {
            AdminDashboardViewModel model = new();

            using (SqlConnection con = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SP_AdminDashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    model.TotalEmployees = Convert.ToInt32(dr["TotalEmployees"]);
                    model.TotalPending = Convert.ToInt32(dr["TotalPending"]);
                    model.TotalApproved = Convert.ToInt32(dr["TotalApproved"]);
                    model.TotalRejected = Convert.ToInt32(dr["TotalRejected"]);
                }
            }

            return model;
        }

        //  GET Employees
        public List<Employee> GetEmployees()
        {
            List<Employee> list = new();

            using (SqlConnection con = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SP_GetEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new Employee
                    {
                        EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                        FullName = dr["FullName"].ToString(),
                        Email = dr["Email"].ToString(),
                        Department = dr["Department"].ToString(),
                        TotalLeaves = Convert.ToInt32(dr["TotalLeaves"]),
                        IsActive = Convert.ToBoolean(dr["IsActive"])
                    });
                }
            }

            return list;
        }

        //  GET Employees By Id
        public List<Employee> GetEmployeesById(int EmployeeId)
        {
            List<Employee> list = new();

            using (SqlConnection con = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SP_GetEmployeesById", con);
                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new Employee
                    {
                        EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                        FullName = dr["FullName"].ToString(),
                        Email = dr["Email"].ToString(),
                        Department = dr["Department"].ToString(),
                        TotalLeaves = Convert.ToInt32(dr["TotalLeaves"]),
                        IsActive = Convert.ToBoolean(dr["IsActive"])
                    });
                }
            }

            return list;
        }



        // Add Employee
        public void AddEmployee(Employee empObj)
        {
            using (SqlConnection con = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SP_AddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FullName", empObj.FullName);
                cmd.Parameters.AddWithValue("@Email", empObj.Email);
                cmd.Parameters.AddWithValue("@Department", empObj.Department);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //  Update Employee
        public void UpdateEmployee(Employee empObj)
        {
            using (SqlConnection con = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", empObj.EmployeeId);
                cmd.Parameters.AddWithValue("@FullName", empObj.FullName);
                cmd.Parameters.AddWithValue("@Email", empObj.Email);
                cmd.Parameters.AddWithValue("@Department", empObj.Department);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //  Deactivate Employee
        public void DeactivateEmployee(int EmployeeId)
        {
            using (SqlConnection con = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SP_DeactivateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //  Get All Leaves
        public List<LeaveSummaryViewModel> GetAllLeaves()
        {
            List<LeaveSummaryViewModel> list = new();

            using (SqlConnection con = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SP_GetAllLeaves", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new LeaveSummaryViewModel
                    {
                        LeaveId = Convert.ToInt32(dr["LeaveId"]),
                        EmployeeName = dr["FullName"].ToString(),
                        FromDate = Convert.ToDateTime(dr["FromDate"]),
                        ToDate = Convert.ToDateTime(dr["ToDate"]),
                        Status = dr["Status"].ToString(),
                        CreatedDate = Convert.ToDateTime(dr["CreatedDate"])
                    });
                }
            }

            return list;
        }

        // Update Leave Status
        public void UpdateLeaveStatus(int leaveId, string status, int adminId)
        {
            using (SqlConnection con = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateLeaveStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@LeaveId", leaveId);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@AdminId", adminId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
