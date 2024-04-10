using WebBanHang.Models.Entities;

namespace WebBanHang.Models
{
    public class HomeViewModel
    {
        public List<Category> categoryList;

        public HomeViewModel() { }
        public HomeViewModel(List<Category> list)
        {
            this.categoryList = list;
        }
    }
}
