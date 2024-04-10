using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanHang.Models.Entities
{
    public class OrderItems
    {
        public OrderItems()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int order_item_id { get; set; }

        public int? quantity { get; set; }

        public decimal? total_price { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        public virtual Orders order { get; set; }

        public virtual Product product { get; set; }
    }
}
