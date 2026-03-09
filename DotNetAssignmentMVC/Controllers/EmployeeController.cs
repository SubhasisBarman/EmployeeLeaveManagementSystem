using DotNetAssignmentMVC.DataAccess;
using DotNetAssignmentMVC.Filters;
using DotNetAssignmentMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNetAssignmentMVC.Controllers
{
    [RoleAuthorize("Employee")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _repo;

        public EmployeeController(EmployeeRepository repo)
        {
            _repo = repo;
        }

        // Dashboard
        public IActionResult Dashboard()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            int empId = _repo.GetEmployeeId(userId);

            var data = _repo.GetDashboard(empId);
            return View(data);
        }

        // Apply Leave
        public IActionResult ApplyLeave()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ApplyLeave(LeaveRequest leaveR)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            int empId = _repo.GetEmployeeId(userId);

            leaveR.EmployeeId = empId;
            var result = _repo.ApplyLeave(leaveR);

            if (result == "success")
            {
                TempData["Success"] = "Leave Applied Successfully!";
                return RedirectToAction("Dashboard");
            }

            ModelState.AddModelError("", result);
            return View(leaveR);
        }

        // My Leaves
        public IActionResult MyLeaves()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            int empId = _repo.GetEmployeeId(userId);

            var leaves = _repo.GetLeaveHistory(empId);
            return View(leaves);
        }
    }
}
