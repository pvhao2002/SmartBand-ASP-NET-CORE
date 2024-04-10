using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanHang.Models.Entities
{
    public class CartItems
    {

        public CartItems()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cart_item_id { get; set; }

        public int? quantity { get; set; }

        public decimal? total_price { get; set; }

        public DateTime? created_at { get; set; } 

        public DateTime? updated_at { get; set; } 

        public virtual Cart cart { get; set; }

        public virtual Product product { get; set; }
    }
}
