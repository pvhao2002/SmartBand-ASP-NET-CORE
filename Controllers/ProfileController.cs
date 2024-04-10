using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Models;
using WebBanHang.Models.Entities;

namespace WebBanHang.Controllers
{
    public class ProfileController : Controller
    {
        private readonly DBContext ctx;
        public ProfileController(DBContext ctx)
        {
            this.ctx = ctx;
        }

        public IActionResult Index()
        {
            ViewBag.CurrentController = "Profile";
            var mUserId = HttpContext.Session.GetInt32("user_id");
            if (mUserId == null && mUserId == 0)
                return RedirectToAction("Index", "Login");
            var listOrder = ctx.orders
                .Include(item => item.user)
                .Include(item => item.order_items)
                .ThenInclude(item => item.product)
                .Where(item => item.user.userId == mUserId)
            .ToList()
                .Select(item => new Orders
                {
                    order_id = item.order_id,
                    created_at = item.created_at,
                    total_price = item.total_price,
                    total_quantity = item.total_quantity,
                    full_name = item.full_name,
                    shipping_address = item.shipping_address,
                    phone_number = item.phone_number,
                    order_items = item.order_items
                    .Select(oitem => new OrderItems
                    {
                        product = oitem.product,
                        total_price = oitem.total_price,
                        quantity = oitem.quantity,
                    })
                    .ToList()
                })
                .ToList();
            var profileModel = new ProfileViewModel(listOrder);
            return View(profileModel);
        }
    }
}
