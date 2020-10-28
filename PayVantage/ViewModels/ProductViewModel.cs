using PayVantage.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayVantage.ViewModels
{
    public class ProductViewModel
    {
        private Product _product;
        public ProductViewModel(Product product)
        {
            this._product = product;
        }
        public string ProductId { get { return _product.ProductId; } set { } }
        public string ProdName { get { return _product.ProdName; } set { } }
        public string Vendor { get { return _product.Vendor; } set { } }
        public string Logo { get { return _product.Logo; } set { } }
        public string CatId { get { return _product.CatId; } set { } }

        public Product Product
        {
            get => _product;
        }
    }
}
