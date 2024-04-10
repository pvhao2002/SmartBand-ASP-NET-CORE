using Microsoft.AspNetCore.Mvc;
using WebBanHang.Models;
using WebBanHang.Models.Entities;

namespace WebBanHang.Controllers
{
    public class LoginController : Controller
    {
        private readonly DBContext ctx;
        public LoginController(DBContext ctx)
        {
            this.ctx = ctx;
        }
        public IActionResult Index()
        {
            ViewBag.CurrentController = "Login";
            return View();
        }

        [HttpPost]
        public ActionResult doLogin(Users user)
        {
            var u = ctx.users.FirstOrDefault(item => item.email.Equals(user.email));
            if (u != null && u.password.Equals(user.password))
            {
                HttpContext.Session.SetInt32("user_id", u.userId);
                HttpContext.Session.SetString("userFullName", u.full_name);
                if (u.role.Equals("admin"))
                {
                    return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                }
                return RedirectToAction("Index", "Home");
            }
            HttpContext.Session.SetString("error", "Email hoặc mật khẩu không đúng");
            return RedirectToAction("Index");
        }
    }
}
