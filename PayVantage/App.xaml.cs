using PayVantage.Views.Forms;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PayVantage.Models;
using System.Net.Http;
using Xamarin.Essentials;

namespace PayVantage
{
    public partial class App : Application
    {
        public static User TheUser { get; set; }
        public static HttpClient client;
        public bool NetworkStatus { get; set; }
        public App()
        {
            client = new HttpClient();
            //Register Syncfusion License
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTc5NTY2QDMxMzcyZTMzMmUzMG5Sd0tkNEptcHRTUE8xRHFOdmxDdTJRSXRmWHd4a2EwZCtrZExSN0dDU1U9");
            InitializeComponent();
            NetworkStatus = Connectivity.NetworkAccess.Equals(NetworkAccess.Internet) ? true : false;
            Connectivity.ConnectivityChanged += Connectivity_Changed;

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void Connectivity_Changed(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess.Equals(NetworkAccess.None) || e.NetworkAccess.Equals(NetworkAccess.Unknown))
                NetworkStatus = false;
            else
                NetworkStatus = true;
        }
    }
}
