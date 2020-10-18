using PayVantage.DataService;
using PayVantage.Helpers;
using PayVantage.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PayVantage.Views.Navigation
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionsPage : ContentPage, INotifyPropertyChanged
    {
        public TransactionsPage()
        {
            _ = PopulateTransactions();
            InitializeComponent();
            TransView.ItemTapped += TransView_ItemTapped;

            //this.BindingContext = PhotosDataService.Instance.PhotosViewModel;
        }

        private void TransView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as Transaction;
        }

        public List<Transaction> Transactions { get; set; }

        private async Task PopulateTransactions()
        {
            try
            {
                RestClient client = new RestClient(App.client);
                Transactions = await client.GetTransDetailsAsync(App.TheUser.Uname, App.TheUser.TheKey);
                //CategsView.ItemsSource = Categories;
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Exception", e.Message + "\n\n" + e.InnerException + "\n\n" + e.Source + "\n\n" + e.StackTrace + "\n\n", "Ok");
            }
        }
    }
}