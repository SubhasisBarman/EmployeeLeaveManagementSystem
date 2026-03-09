using DotNetAssignmentMVC.DataAccess;
using DotNetAssignmentMVC.Filters;
using DotNetAssignmentMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAssignmentMVC.Controllers
{
    [RoleAuthorize("Admin")]
    public class AdminController : Controller
    {
        private readonly AdminRepository _repo;

        public AdminController(AdminRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Dashboard()
        {
            var data = _repo.GetDashboardData();
            return View(data);
        }

        public IActionResult Employees()
        {
            return View(_repo.GetEmployees());
        }

        public IActionResult AddEditEmployee(int? id)
        {
            if (id == null)
            {
                return View(new Employee());
            }
            else
            {
                var employee = _repo.GetEmployeesById(id.Value).FirstOrDefault();

                if (employee == null)
                {
                    return NotFound();
                }

                return View(employee);
            }
        }


        [HttpPost]
        public IActionResult AddEditEmployee(Employee empObj)
        {
            if (ModelState.IsValid)
            {
                if (empObj.EmployeeId == 0)
                {
                    _repo.AddEmployee(empObj);
                }
                else
                {
                    _repo.UpdateEmployee(empObj);
                }

                return RedirectToAction("Employees");
            }

            return View(empObj);
        }


        public IActionResult Deactivate(int Id)
        {
            _repo.DeactivateEmployee(Id);
            return RedirectToAction("Employees");
        }

        public IActionResult LeaveRequests()
        {
            return View(_repo.GetAllLeaves());
        }

        public IActionResult UpdateLeave(int Id, string status)
        {
            int adminId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            _repo.UpdateLeaveStatus(Id, status, adminId);
            return RedirectToAction("LeaveRequests");
        }
    }
}
