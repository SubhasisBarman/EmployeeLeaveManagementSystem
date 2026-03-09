using DotNetAssignmentMVC.DataAccess;
using DotNetAssignmentMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAssignmentMVC.Controllers
{
    public class AuthController : Controller
    {
        
        private readonly AuthRepository _repo;

        public AuthController(AuthRepository repo)
        {
            _repo = repo;
        }

        // Login Page
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginObj)
        {
            if (!ModelState.IsValid)
                return View(loginObj);

            var user = _repo.Login(loginObj);

            if (user != null)
            {
                // Store Session
                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                HttpContext.Session.SetString("FullName", user.FullName);
                HttpContext.Session.SetString("Role", user.Role);

                if (user.Role == "Admin")
                    return RedirectToAction("Dashboard", "Admin");
                else
                    return RedirectToAction("Dashboard", "Employee");
            }

            ViewBag.Error = "Invalid Email or Password";
            return View(loginObj);
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
