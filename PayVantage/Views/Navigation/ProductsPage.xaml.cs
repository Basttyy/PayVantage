using PayVantage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PayVantage.Views.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsPage : ContentPage
    {
        private Product _product;
        public ProductsPage(Product product)
        {
            this._product = product;
            this.BindingContext = this;
            InitializeComponent();
        }

        public string ProductId { get { return _product.ProductId; } set { } }
        public string ProdName { get { return _product.ProdName; } set { } }
        public string Vendor { get { return _product.Vendor; } set { } }
        public string CatId { get { return _product.CatId; } set { } }

        public Product Product
        {
            get => _product;
        }
    }
}