using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Models.Entities
{
    public class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            products = new HashSet<Product>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }

        [Key]
        public int category_id { get; set; }

        [StringLength(255)]
        public string category_name { get; set; }

        public string description { get; set; }

        [StringLength(50)]
        public string status { get; set; }

        public DateTime? created_at { get; set; }  

        public DateTime? updated_at { get; set; }  

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> products { get; set; }
    }
}
