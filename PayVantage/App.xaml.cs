using PayVantage.Views.Forms;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PayVantage.Models;
using System.Net.Http;

namespace PayVantage
{
    public partial class App : Application
    {
        //public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        public static User TheUser { get; set; }
        public static HttpClient client;
        public App()
        {
            client = new HttpClient();
            //Register Syncfusion License
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTc5NTY2QDMxMzcyZTMzMmUzMG5Sd0tkNEptcHRTUE8xRHFOdmxDdTJRSXRmWHd4a2EwZCtrZExSN0dDU1U9");
            InitializeComponent();

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
    }
}
