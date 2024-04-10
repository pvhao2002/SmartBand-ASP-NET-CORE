using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebBanHang.Models;
using WebBanHang.Models.Entities;

namespace WebBanHang.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DBContext ctx;

        public HomeController(ILogger<HomeController> logger, DBContext ctx)
        {
            _logger = logger;
            this.ctx = ctx;
        }

        public IActionResult Index()
        {
            ViewBag.CurrentController = "Home";
            var listCategory = ctx.categories
                     .Where(item => item.status.Equals("active"))
                     .Include(item => item.products)
                 .ToList()
                     .Select(item => new Category
                     {
                         category_id = item.category_id,
                         category_name = item.category_name,
                         products = item.products.ToList()
                     })
                     .ToList();
            var model = new HomeViewModel(listCategory);
            return View(model);
        }

        public ActionResult Logout()
        {
            HttpContext.Session.SetInt32("user_id", 0);
            HttpContext.Session.SetString("userFullName", string.Empty);
            return RedirectToAction("Index", "Home");
        }
    }
}