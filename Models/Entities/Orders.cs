using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanHang.Models.Entities
{
    public class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            order_items = new HashSet<OrderItems>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int order_id { get; set; }

        [StringLength(255)]
        public string shipping_address { get; set; }

        [StringLength(20)]
        public string phone_number { get; set; }

        [StringLength(255)]
        public string full_name { get; set; }

        public decimal? total_price { get; set; }

        public int? total_quantity { get; set; }

        [StringLength(50)]
        public string status { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItems> order_items { get; set; }

        public virtual Users user { get; set; }
    }
}
