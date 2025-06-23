using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Models;
using WebBanHang.Models.Entities;

namespace WebBanHang.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ProductAdminController(DBContext ctx) : Controller
    {
        public IActionResult Index()
        {
            var list = ctx.products
                .Include(item => item.category)
                    .Where(item => item.status.Equals("active"))
                    .ToList()
                    .Select(item => new Product
                    {
                        product_id = item.product_id,
                        product_name = item.product_name,
                        product_image = item.product_image,
                        status = item.status,
                        price = item.price,
                        description = item.description,
                        category = new Category
                        {
                            category_name = item.category.category_name
                        }
                    })
                    .ToList();
            return View(list);
        }

        public ActionResult Add()
        {
            var listCate = ctx.categories
                .Where(item => item.status.Equals("active"))
                .ToList();
            return View(new ProductViewModel(listCate, new Product()));
        }

        [HttpPost]
        public ActionResult doAdd(Product product, IFormFile img, IFormCollection form)
        {
            var p = new Product();
            p.price = product.price;
            p.product_name = product.product_name;
            p.description = product.description;
            p.status = "active";
            p.created_at = DateTime.Now;
            p.updated_at = DateTime.Now;
            if (img.Length > 0)
            {
                var fileName = Path.GetFileName(img.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    img.CopyTo(stream);
                }
                p.product_image = $"~/uploads/{fileName}";
            }
            var cateId = form["cate"];
            p.category = ctx.categories.FirstOrDefault(item => item.category_id == Convert.ToInt32(cateId));
            ctx.products.Add(p);
            ctx.SaveChanges();
            return RedirectToAction("Index", "ProductAdmin");
        }


        public ActionResult Edit(int id)
        {
            var product = ctx.products
                .Include(item => item.category)
                .FirstOrDefault(item => item.product_id == id);
            if (product == null)
            {
                return RedirectToAction("Index", "ProductAdmin");
            }
            var listCate = ctx.categories
                .Where(item => item.status.Equals("active"))
                .ToList();
            var pModel = new ProductViewModel(listCate, product);
            return View(pModel);
        }

        [HttpPost]
        public ActionResult doUpdate(Product product, IFormFile img, IFormCollection form)
        {
            var p = ctx.products.FirstOrDefault(item => item.product_id == product.product_id);
            p.price = product.price;
            p.product_name = product.product_name;
            p.description = product.description;
            p.status = "active";
            p.updated_at = DateTime.Now;
            if (img.Length > 0)
            {
                var fileName = Path.GetFileName(img.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    img.CopyTo(stream);
                }
                p.product_image = $"~/uploads/{fileName}";
            }
            var cateId = form["cate"];
            p.category = ctx.categories.FirstOrDefault(item => item.category_id == Convert.ToInt32(cateId));
            ctx.SaveChanges();
            return RedirectToAction("Index", "ProductAdmin");
        }


        public ActionResult delete(int id)
        {
            var p = ctx.products.FirstOrDefault(item => item.product_id == id);
            if (p != null)
            {
                p.status = "inactive";
                p.updated_at = DateTime.Now;
                ctx.SaveChanges();
            }
            return RedirectToAction("Index", "ProductAdmin");
        }
    }
}
