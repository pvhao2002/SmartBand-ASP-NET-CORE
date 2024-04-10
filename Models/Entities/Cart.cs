using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanHang.Models.Entities
{
    public class Cart
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cart()
        {
            cart_items = new HashSet<CartItems>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cart_id { get; set; }

        public decimal? total_price { get; set; }

        public int? total_quantity { get; set; }

        public DateTime? created_at { get; set; } 

        public DateTime? updated_at { get; set; } 

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartItems> cart_items { get; set; }

        public virtual Users user { get; set; }
    }
}
