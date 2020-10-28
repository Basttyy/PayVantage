using PayVantage.Helpers;
using PayVantage.Views.Forms;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PayVantage.Views.Navigation
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            nameLbl.Text = App.TheUser.Name;
            emailLbl.Text = App.TheUser.Uname;
            phoneLbl.Text = App.TheUser.Phone;
            uTypeLbl.Text = App.TheUser.Utype;
            vendBalLbl.Text = App.TheUser.VendBal;
            profBalLbl.Text = App.TheUser.ProfBal;
            totBalLbl.Text = App.TheUser.TotBal;
        }

        private async void OnLogoutTapped(object sender, System.EventArgs e)
        {
            var item = sender as StackLayout;

            Color color = item.BackgroundColor;

            item.BackgroundColor = Color.DarkGray;
            await Task.Delay(500);
            item.BackgroundColor = color;
            if (await Application.Current.MainPage.DisplayAlert("Alert", "Are sure you want to logout?", "Yes", "Cancel"))
            {
                var client = new RestClient(App.client);
                if (await client.LogoutAsync(App.TheUser.Uname))
                {
                    App.TheUser = null;
                    NavHelper.InserPageBefore(new LoginPage());
                    await NavHelper.PopAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Sorry", "Unable to logout", "Ok");
                }
            }
        }
        private async void OnChangePasswordTapped(object sender, System.EventArgs e)
        {
            var item = sender as StackLayout;

            Color color = item.BackgroundColor;

            item.BackgroundColor = Color.DarkGray;
            await Task.Delay(500);
            item.BackgroundColor = color;

            await NavHelper.PushModalAsync(new ChangePasswordPage(Constants.changePassNotFirst));
        }
    }
}