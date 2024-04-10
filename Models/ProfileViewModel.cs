using WebBanHang.Models.Entities;

namespace WebBanHang.Models
{
    public class ProfileViewModel
    {
        public List<Orders> myOrder { get; set; }
        public ProfileViewModel() { }
        public ProfileViewModel(List<Orders> myOrder)
        {
            this.myOrder = myOrder;
        }
    }
}
