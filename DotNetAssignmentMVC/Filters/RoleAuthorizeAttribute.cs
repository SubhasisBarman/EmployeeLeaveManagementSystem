using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNetAssignmentMVC.Filters
{
    public class RoleAuthorizeAttribute : Attribute, IActionFilter
    {
        private readonly string _role;

        public RoleAuthorizeAttribute(string role)
        {
            _role = role;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;

            var userId = session.GetString("UserId");
            var role = session.GetString("Role");

            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }
            if (role != _role)
            {
                context.HttpContext.Session.Clear();

                context.Result = new RedirectToActionResult(
                    "Login",
                    "Auth",
                    new { message = "Access denied! Invalid role." }
                );
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
