using Microsoft.EntityFrameworkCore;
using WebBanHang.Models.Entities;

namespace WebBanHang.Models
{
    public class DBContext: DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
            
        }
        public virtual DbSet<CartItems> cart_items { get; set; }
        public virtual DbSet<Cart> carts { get; set; }
        public virtual DbSet<Category> categories { get; set; }
        public virtual DbSet<OrderItems> order_items { get; set; }
        public virtual DbSet<Orders> orders { get; set; }
        public virtual DbSet<Product> products { get; set; }
        public virtual DbSet<Users> users { get; set; }
    }
}
