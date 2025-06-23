using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Models;

namespace WebBanHang.Areas.Admin.Controllers;

[Area("Admin")]
public class OrderAdminController(DBContext ctx) : Controller
{
    // GET
    public IActionResult Index()
    {
        var list = ctx.orders
            .Include(it => it.order_items)
            .ThenInclude(it => it.product)
            .Include(it => it.user)
            .ToList();
        return View(list);
    }


    public ActionResult Accept(int id)
    {
        var order = ctx.orders.FirstOrDefault(item => item.order_id == id);
        if (order != null)
        {
            order.status = "in_progress";
            order.updated_at = DateTime.Now;
            ctx.SaveChanges();
        }

        return RedirectToAction("Index", "OrderAdmin");
    }


    public ActionResult Reject(int id)
    {
        var order = ctx.orders.FirstOrDefault(item => item.order_id == id);
        if (order != null)
        {
            order.status = "cancel";
            order.updated_at = DateTime.Now;
            ctx.SaveChanges();
        }
        return RedirectToAction("Index", "OrderAdmin");
    }

    public ActionResult Done(int id)
    {
        var order = ctx.orders.FirstOrDefault(item => item.order_id == id);
        if (order != null)
        {
            order.status = "done";
            order.updated_at = DateTime.Now;
            ctx.SaveChanges();
        }
        return RedirectToAction("Index", "OrderAdmin");
    }

    public ActionResult Shipping(int id)
    {
        var order = ctx.orders.FirstOrDefault(item => item.order_id == id);
        if (order != null)
        {
            order.status = "in_shipping";
            order.updated_at = DateTime.Now;
            ctx.SaveChanges();
        }
        return RedirectToAction("Index", "OrderAdmin");
    }
}