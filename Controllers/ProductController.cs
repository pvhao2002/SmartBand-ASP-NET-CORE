using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WebBanHang.Models.Entities;
using WebBanHang.Models;
using Microsoft.EntityFrameworkCore;

namespace WebBanHang.Controllers
{
    public class ProductController : Controller
    {
        private readonly DBContext ctx;

        public ProductController(DBContext ctx)
        {
            this.ctx = ctx;
        }

        public IActionResult Index(int? cate)
        {
            ViewBag.CurrentController = "Product";
            var listCate = ctx.categories
                .Where(item => item.status.Equals("active"))
                .ToList();
            List<Product> products;
            if (cate != null)
            {
                products = ctx.products
                    .Include(item => item.category)
                    .Where(item => item.category.category_id == cate
                    && item.status.Equals("active"))
                    .ToList();
            }
            else
            {
                products = ctx.products
                    .Where(item => "active".Equals(item.status))
                    .ToList();
            }
            var productModel = new ProductViewModel(products, listCate);
            return View(productModel);
        }

        public ActionResult Detail(int id)
        {
            ViewBag.CurrentController = "Product";
            var p = ctx.products.FirstOrDefault(item => item.product_id == id);
            if (p == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(p);
        }
    }
}
