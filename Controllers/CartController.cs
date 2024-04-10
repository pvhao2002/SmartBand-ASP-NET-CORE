using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Models;
using WebBanHang.Models.Entities;

namespace WebBanHang.Controllers
{
    public class CartController : Controller
    {
        private readonly DBContext ctx;
        public CartController(DBContext ctx)
        {
            this.ctx = ctx;
        }

        public IActionResult Index()
        {
            ViewBag.CurrentController = "Cart";
            var userId = HttpContext.Session.GetInt32("user_id");
            if (userId != null && userId != 0)
            {
                var cart = ctx.carts
                    .Include(item => item.user)
                    .Include(item => item.cart_items)
                    .ThenInclude(item => item.product)
                    .FirstOrDefault(item => item.user.userId == userId);
                Cart cartTemp = new Cart();
                if (cart != null)
                {
                    cartTemp = new Cart
                    {
                        cart_id = cart.cart_id,
                        total_price = cart.total_price,
                        total_quantity = cart.total_quantity,
                        cart_items = cart.cart_items
                                        .ToList()
                                        .Select(item => new CartItems
                                        {
                                            cart_item_id = item.cart_item_id,
                                            product = item.product,
                                            total_price = item.total_price,
                                            quantity = item.quantity
                                        })
                                        .ToList()
                    };
                }
                return View(cartTemp);
            }
            return RedirectToAction("Index", "Login");
        }


        [HttpPost]
        public ActionResult Add(IFormCollection form)
        {
            var mUserId = HttpContext.Session.GetInt32("user_id");
            if (mUserId == null || mUserId == 0) return RedirectToAction("Index", "Login");

            var pid = Convert.ToInt32(form["pid"]);
            var price = Convert.ToDecimal(form["price"]);
            var quantity = Convert.ToInt32(form["quantity"]);
            quantity = quantity == 0 ? 1 : quantity;
            var uUser = ctx.users.FirstOrDefault(item => item.userId == mUserId);
            var pProduct = ctx.products.FirstOrDefault(p => p.product_id == pid);
            var cartByUser = ctx.carts
                 .Include(item => item.user)
                .FirstOrDefault(item => item.user.userId == mUserId);
            if (cartByUser != null)
            {
                var cartItem = ctx.cart_items
                    .Include(item => item.cart)
                    .Include(item => item.product)
                    .FirstOrDefault(item => item.cart.cart_id == cartByUser.cart_id
                                                 && item.product.product_id == pid);
                if (cartItem != null)
                {
                    cartItem.quantity += quantity;
                    cartItem.total_price = cartItem.quantity * price;
                    cartItem.updated_at = DateTime.Now;
                }
                else
                {
                    
                    cartByUser.cart_items.Add(new CartItems
                    {
                        cart = cartByUser,
                        product = pProduct,
                        quantity = quantity,
                        total_price = price * quantity,
                        created_at = DateTime.Now,
                        updated_at = DateTime.Now
                    });
                }
                cartByUser.total_quantity += quantity;
                cartByUser.updated_at = DateTime.Now;
                ctx.SaveChanges();
                cartByUser.total_price = ctx.cart_items
                    .Include(item => item.cart)
                    .Where(item => item.cart.cart_id == cartByUser.cart_id)
                    .Sum(item => item.total_price);
                cartByUser.total_quantity = ctx.cart_items
                    .Include(item => item.cart)
                    .Where(item => item.cart.cart_id == cartByUser.cart_id)
                    .Sum(item => item.quantity);
            }
            else
            {
                var newCart = new Cart
                {
                    user = uUser,
                    total_price = price * quantity,
                    total_quantity = quantity,
                    updated_at = DateTime.Now,
                    created_at = DateTime.Now,
                };
                newCart.cart_items.Add(new CartItems
                {
                    cart = newCart,
                    product = pProduct,
                    quantity = quantity,
                    total_price = price * quantity,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                });
                ctx.carts.Add(newCart);
            }
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult updateCart(int quantity, int pid)
        {
            var mUserId = HttpContext.Session.GetInt32("user_id");
            if (mUserId == null || mUserId == 0) return Json(new { });
            var cartByUser = ctx.carts
            .Include(item => item.user)
            .FirstOrDefault(item => item.user.userId == mUserId);
            if (cartByUser != null)
            {
                var cartItem = ctx.cart_items
                .Include(item => item.cart)
                .Include(item => item.product)
                .FirstOrDefault(item => item.cart.cart_id == cartByUser.cart_id
                                            && item.product.product_id == pid);
                if (cartItem != null)
                {
                    cartItem.quantity = quantity;
                    cartItem.total_price = cartItem.quantity * cartItem.product.price;
                    cartItem.updated_at = DateTime.Now;
                }
                ctx.SaveChanges();
                cartByUser.updated_at = DateTime.Now;
                cartByUser.total_price = ctx.cart_items
                .Include(item => item.cart)
                .Where(item => item.cart.cart_id == cartByUser.cart_id)
                .Sum(item => item.total_price);
                cartByUser.total_quantity = ctx.cart_items
                .Include(item => item.cart)
                .Where(item => item.cart.cart_id == cartByUser.cart_id)
                .Sum(item => item.quantity);
                ctx.SaveChanges();
            }
            return Json(new { });
        }

        public ActionResult Remove(int id)
        {
            var mUserId = HttpContext.Session.GetInt32("user_id");
            if (mUserId == null || mUserId == 0) return RedirectToAction("Index", "Login");
            var cartItem = ctx.cart_items.FirstOrDefault(r => r.cart_item_id == id);
            if (cartItem != null)
            {
                ctx.cart_items.Remove(cartItem);
                ctx.SaveChanges();
            }
            var cartByUser = ctx.carts
            .Include(item => item.user)
            .FirstOrDefault(item => item.user.userId == mUserId);
            if (cartByUser != null)
            {
                cartByUser.updated_at = DateTime.Now;
                cartByUser.total_price = ctx.cart_items
                .Include(item => item.cart)
                .Where(item => item.cart.cart_id == cartByUser.cart_id)
                .Sum(item => item.total_price);
                cartByUser.total_quantity = ctx.cart_items
                .Include(item => item.cart)
                .Where(item => item.cart.cart_id == cartByUser.cart_id)
                .Sum(item => item.quantity);
                ctx.SaveChanges();
            }
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public ActionResult Checkout(IFormCollection form)
        {
            var mUserId = HttpContext.Session.GetInt32("user_id");
            if (mUserId == null || mUserId == 0) return RedirectToAction("Index", "Login");
            var cartByUser = ctx.carts
                .Include(item => item.user)
                .Include(item => item.cart_items)
                .ThenInclude(item => item.product)
                .FirstOrDefault(item => item.user.userId == mUserId);
            var uUser = ctx.users.FirstOrDefault(item => item.userId == mUserId);
            if (cartByUser != null)
            {
                var o = new Orders();
                o.user = uUser;
                o.shipping_address = form["address"].ToString();
                o.phone_number = form["phone"].ToString();
                o.total_price = cartByUser.total_price;
                o.total_quantity = cartByUser.total_quantity;
                o.created_at = DateTime.Now;
                o.updated_at = DateTime.Now;
                o.status = "pending";
                o.full_name = form["fullname"].ToString();

                foreach (var item in cartByUser.cart_items)
                {
                    var p = ctx.products.FirstOrDefault(i => i.product_id == item.product.product_id);
                    o.order_items.Add(new OrderItems
                    {
                        order = o,
                        product = p,
                        quantity = item.quantity,
                        total_price = item.total_price,
                        created_at = DateTime.Now,
                        updated_at = DateTime.Now,
                    });
                }

                ctx.orders.Add(o);
                ctx.cart_items.RemoveRange(cartByUser.cart_items);
                cartByUser.total_price = 0;
                cartByUser.total_quantity = 0;
                cartByUser.updated_at = DateTime.Now;
                ctx.SaveChanges();
                HttpContext.Session.SetString("order", "Đặt hàng thành công");
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}
