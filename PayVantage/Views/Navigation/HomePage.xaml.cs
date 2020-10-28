using PayVantage.DataService;
using PayVantage.Helpers;
using PayVantage.Models;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using PayVantage.ViewModels;
using PayVantage.Views.Forms;

namespace PayVantage.Views.Navigation
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage, INotifyPropertyChanged
    {
        public HomePage()
        {
            InitializeComponent();
            _ = PopulateCategs();
            this.BindingContext = this;
            CategsView.ItemTapped += CategsView_ItemTapped;
            if (App.TheUser.Pchanger.Equals("0"))
                _ = NavHelper.PushAsync(new ChangePasswordPage(Constants.changePassFirst));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = PopulateCategs();
        }

        public List<Category> Categories { get; set; }

        private async void CategsView_ItemTapped(object sender, ItemTappedEventArgs ev)
        {
            try
            {
                var item = ev.Item as Category;
                await NavHelper.PushAsync(new ProductsPage(new VendorsGroupViewModel(item.CatId)));
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Exception", e.Message + "\n\n" + e.InnerException + "\n\n" + e.Source + "\n\n" + e.StackTrace + "\n\n", "Ok");
            }
        }
        private async Task PopulateCategs()
        {
            try
            {
                NameLabel.Text = App.TheUser.Name;
                VendBalLabel.Text = App.TheUser.VendBal;
                ProfBalLabel.Text = App.TheUser.ProfBal;
                TotBalLabel.Text = App.TheUser.TotBal;

                RestClient client = new RestClient(App.client);
                Categories = await client.GetCategoriesAsync(App.TheUser.Uname, App.TheUser.TheKey);
                CategsView.ItemsSource = Categories;
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Exception", e.Message + "\n\n" + e.InnerException + "\n\n" + e.Source + "\n\n" + e.StackTrace + "\n\n", "Ok");
            }
        }
    }
}