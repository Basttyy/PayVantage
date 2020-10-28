using PayVantage.Models;
using PayVantage.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public ProductsPage(VendorsGroupViewModel viewModel)
        {
            InitializeComponent();
            this.ViewModel = viewModel;
        }

        private VendorsGroupViewModel ViewModel
        {
            get { return (VendorsGroupViewModel)BindingContext; }
            set { BindingContext = value; }
        }
        private List<Vendor> ListVendor = new List<Vendor>();

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                
                if(ViewModel.Items.Count == 0)
                {
                    ViewModel.LoadVendorsCommand.Execute(null);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void vendorsList_ItemTapped(object sender, ItemTappedEventArgs ev)
        {

        }
    }
}