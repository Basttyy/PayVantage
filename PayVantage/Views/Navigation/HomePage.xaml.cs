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

namespace PayVantage.Views.Navigation
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage, INotifyPropertyChanged
    {
        public HomePage()
        {
            _ = PopulateCategs();
            InitializeComponent();
            this.BindingContext = this;
            CategsView.ItemTapped += CategsView_ItemTapped;
        }

        public List<Category> Categories { get; set; }

        private void CategsView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as Category;


        }
        private async Task PopulateCategs()
        {
            try
            {
                await Application.Current.MainPage.DisplayAlert("Title", App.TheUser.Name, "Ok");
                NameLabel.Text = App.TheUser.Name;
                VendBalLabel.Text = App.TheUser.VendBal;
                ProfBalLabel.Text = App.TheUser.ProfBal;
                TotBalLabel.Text = App.TheUser.TotBal;

                RestClient client = new RestClient(App.client);
                Categories = await client.GetCategoriesAsync(App.TheUser.Uname, App.TheUser.TheKey);
                //CategsView.ItemsSource = Categories;
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Exception", e.Message + "\n\n" + e.InnerException + "\n\n" + e.Source + "\n\n" + e.StackTrace + "\n\n", "Ok");
            }
        }
    }
}