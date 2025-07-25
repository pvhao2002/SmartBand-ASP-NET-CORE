﻿using WebBanHang.Models.Entities;

namespace WebBanHang.Models
{
    public class ProductViewModel
    {
        public Product product;
        public List<Category> categories;
        public List<Product> listProduct;
        public string search;

        public ProductViewModel(List<Category> listCate, Product p = null)
        {
            this.product = p;
            this.categories = listCate;
        }

        public ProductViewModel()
        {
        }

        public ProductViewModel(List<Product> list, List<Category> listCate, string search = "")
        {
            this.listProduct = list;
            this.categories = listCate;
            this.search = search;
        }
    }
}