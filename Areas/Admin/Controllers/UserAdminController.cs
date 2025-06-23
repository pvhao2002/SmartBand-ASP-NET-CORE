using Microsoft.AspNetCore.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Areas.Admin.Controllers;

[Area("Admin")]
public class UserAdminController(DBContext ctx) : Controller
{
    // GET
    public IActionResult Index()
    {
        var list = ctx
            .users
            .Where(it => "active".Equals(it.status) && "user".Equals(it.role))
            .ToList();
        return View(list);
    }
    
    public ActionResult Delete(int id)
    {
        var p = ctx.users.FirstOrDefault(item => item.userId == id);
        if (p != null)
        {
            p.status = "inactive";
            p.updated_at = DateTime.Now;
            ctx.SaveChanges();
        }
        return RedirectToAction("Index", "UserAdmin");
    }
}