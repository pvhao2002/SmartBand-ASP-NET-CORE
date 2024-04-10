using Microsoft.AspNetCore.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeAdminController : Controller
    {
        private readonly DBContext ctx;

        public HomeAdminController(DBContext ctx)
        {
            this.ctx = ctx;
        }

        public IActionResult Index()
        {
            var totalP = ctx.products.Where(item => item.status.Equals("active")).ToList().Count;
            var totalO = ctx.orders.ToList().Count;
            var totalR = ctx.orders.Sum(item => item.total_price);
            var homeModel = new HomeAdminViewModel(totalP, totalO, totalR);
            return View(homeModel);
        }

        public ActionResult Logout()
        {
            HttpContext.Session.SetInt32("user_id", 0);
            HttpContext.Session.SetString("userFullName", string.Empty);
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
