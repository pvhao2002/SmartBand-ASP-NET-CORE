using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Models;
using WebBanHang.Models.Entities;

namespace WebBanHang.Controllers
{
    public class ProductController(DBContext ctx) : Controller
    {
        public IActionResult Index(int? cate, string search = "")
        {
            ViewBag.CurrentController = "Product";

            var listCate = ctx.categories
                .Where(item => item.status == "active")
                .ToList();

            var products = ctx.products
                .Include(p => p.category)
                .Where(p => p.status == "active")
                .AsEnumerable();

            if (cate.HasValue)
            {
                products = products.Where(p => p.category.category_id == cate);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var normalizedSearch = RemoveDiacritics(search).ToLower();
                products = products.Where(p =>
                    RemoveDiacritics(p.product_name).ToLower().Contains(normalizedSearch)
                    || (!string.IsNullOrEmpty(p.description) &&
                        RemoveDiacritics(p.description).ToLower().Contains(normalizedSearch))
                    || p.product_id.ToString().Contains(search)
                );
            }

            var productList = products.ToList();
            var model = new ProductViewModel(productList, listCate, search);
            return View(model);
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

        public static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            var normalized = text.Normalize(System.Text.NormalizationForm.FormD);
            var builder = new System.Text.StringBuilder();

            foreach (var ch in normalized)
            {
                var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(ch);
                if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(ch);
                }
            }

            return builder.ToString().Normalize(System.Text.NormalizationForm.FormC);
        }
    }
}