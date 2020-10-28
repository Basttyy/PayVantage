using PayVantage.Helpers;
using PayVantage.Models;
using PayVantage.Views.Forms;
using PSC.Xamarin.MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Xamarin.Forms;

namespace PayVantage.ViewModels
{
    public class VendorsGroupViewModel : BaseViewModel
    {
        private string _categId;
        private VendorViewModel _oldVendor;
        private ObservableCollection<VendorViewModel> items;
        public ObservableCollection<VendorViewModel> Items
        {
            get => items;
            set => SetProperty(ref items, value);
        }
        public Command LoadVendorsCommand { get; set; }
        public Command<VendorViewModel> RefreshItemsCommand { get; set; }
        public Command<ProductViewModel> ItemTappedCommand { get; set; }

        public VendorsGroupViewModel(string categId)
        {
            this._categId = categId;
            items = new ObservableCollection<VendorViewModel>();
            Items = new ObservableCollection<VendorViewModel>();
            LoadVendorsCommand = new Command(async () => await ExecuteLoadVendorsCommandAsync());
            RefreshItemsCommand = new Command<VendorViewModel>((item) => ExecuteRefreshItemsCommand(item));
            ItemTappedCommand = new Command<ProductViewModel>((item) => HandleItemTapped(item));
        }
        public bool isExpanded = false;
        public async void HandleItemTapped(ProductViewModel item)
        {
            try
            {
                await NavHelper.PushAsync(new VendPage(item.CatId, item.ProductId));
            }
            catch (Exception e)
            {
                //await Application.Current.MainPage.DisplayAlert("Exception", e.Message + "\n\n" + e.InnerException + "\n\n" + e.Source + "\n\n" + e.StackTrace + "\n\n", "Ok");
            }
        }
        public void ExecuteRefreshItemsCommand(VendorViewModel item)
        {
            if (_oldVendor == null)
            {
                _oldVendor = item;
            }
            if (_oldVendor.Equals(item))
            {
                // click twice on the same item will hide it
                item.Expanded = !item.Expanded;
            }
            else
            {
                if (!_oldVendor.Equals(null))
                {
                    // hide previous selected item
                    _oldVendor.Expanded = false;
                }
                item.Expanded = true;
            }
            _oldVendor = item;
        }
        async System.Threading.Tasks.Task ExecuteLoadVendorsCommandAsync()
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                Items.Clear();
                RestClient client = new RestClient(App.client);
                List<Product> products = await client.GetProductsAsync(App.TheUser.Uname, App.TheUser.TheKey, _categId);

                if (products.Count > 0 && !products.Equals(null))
                {
                    products.ForEach((product) =>
                    {
                        Vendor vendor = new Vendor
                        {
                            VendorName = product.Vendor,
                            VendorLogo = product.Logo,
                            Products = products.Where(u => u.Vendor.Equals(product.Vendor)).ToList()
                        };
                        VendorViewModel vm = new VendorViewModel(vendor);
                        if (Items.Where(u => u.Vendor.VendorName.Equals(vm.VendorName)).ToList().Count() == 0)
                        {
                            Items.Add(vm);
                        }
                    });
                }
                else { IsEmpty = true; }
            }
            catch (Exception ex)
            {
                IsBusy = false;
                //await Application.Current.MainPage.DisplayAlert("products", ex.Message + "\n\n" + ex.InnerException + "\n\n" + ex.Source + "\n\n" + ex.StackTrace + "\n\n", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
