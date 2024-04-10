using Microsoft.AspNetCore.Mvc;
using WebBanHang.Models;
using WebBanHang.Models.Entities;

namespace WebBanHang.Controllers
{
    public class RegisterController : Controller
    {
        private readonly DBContext ctx;

        public RegisterController(DBContext ctx)
        {
            this.ctx = ctx;
        }

        public IActionResult Index()
        {
            ViewBag.CurrentController = "Login";
            return View();
        }

        [HttpPost]
        public ActionResult doRegister(Users user, IFormCollection form)
        {
            if (!user.password.Equals(form["confirm-password"]))
            {
                HttpContext.Session.SetString("error", "Nhập lại mật khẩu không khớp");
                return View("Index");
            }
            var isExistEmail = ctx.users.FirstOrDefault(item => item.email.Equals(user.email));
            if (isExistEmail != null)
            {
                HttpContext.Session.SetString("error", "Email đã tồn tại");
                return View("Index");
            }
            user.role = "user";
            user.created_at = DateTime.Now;
            user.updated_at = DateTime.Now;
            user.status = "active";
            ctx.users.Add(user);
            ctx.SaveChanges();
            HttpContext.Session.SetString("success", "Đăng ký thành công");
            return RedirectToAction("Index", "Login");
        }
    }
}
