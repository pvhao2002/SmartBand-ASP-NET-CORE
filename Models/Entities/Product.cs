using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Models.Entities
{
    public class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            cart_items = new HashSet<CartItems>();
            order_items = new HashSet<OrderItems>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int product_id { get; set; }

        [StringLength(255)]
        public string product_name { get; set; }

        public decimal? price { get; set; }

        [Column(TypeName = "text")]
        public string description { get; set; }

        [StringLength(2000)]
        public string product_image { get; set; }

        [StringLength(50)]
        public string status { get; set; }

        public DateTime? created_at { get; set; } 

        public DateTime? updated_at { get; set; } 

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartItems> cart_items { get; set; }

        public virtual Category category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItems> order_items { get; set; }
    }
}
