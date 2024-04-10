namespace WebBanHang.Models
{
    public class HomeAdminViewModel
    {
        public int totalProduct { get; set; }
        public int totalOrder { get; set; }
        public decimal? totalRevenue { get; set; }
        public HomeAdminViewModel() 
        {
            this.totalProduct = 0;
            this.totalOrder = 0;
            this.totalRevenue = 0;
        }
        public HomeAdminViewModel(int p, int o, decimal? r)
        {
            this.totalRevenue = r;
            this.totalProduct = p;
            this.totalOrder = o;
        }
    }
}
