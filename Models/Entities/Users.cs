using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanHang.Models.Entities
{
    public class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            carts = new HashSet<Cart>();
            orders = new HashSet<Orders>();

            role = "user"; // Gán giá trị mặc định cho role là "user"
            created_at = DateTime.Now;  
            updated_at = DateTime.Now;  
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; }

        [StringLength(255)]
        public string email { get; set; }

        [StringLength(255)]
        public string password { get; set; }

        [StringLength(255)]
        public string full_name { get; set; }

        [StringLength(50)]
        public string role { get; set; }

        [StringLength(50)]
        public string status { get; set; }

        public DateTime? created_at { get; set; } 

        public DateTime? updated_at { get; set; } 

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> carts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> orders { get; set; }
    }
}
