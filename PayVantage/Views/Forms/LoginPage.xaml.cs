using PayVantage.Converters;
using PayVantage.Helpers;
using PayVantage.Models;
using PayVantage.Views.Navigation;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PayVantage.Views.Forms
{
    /// <summary>
    /// Page to login with user name and password
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage, INotifyPropertyChanged
    {
        
        #region Fields

        private string password;
        private string email;
        private bool isInvalidEmail;

        #endregion

        #region Event handler

        /// <summary>
        /// Occurs when the property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Command

        public Command LoginCommand { get; set; }

        public Command SignUpCommand { get; set; }

        public Command ForgotPasswordCommand { get; set; }

        //public Command SocialMediaLoginCommand { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage" /> class.
        /// </summary>
        public LoginPage()
        {
            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);
            this.BindingContext = this;
            InitializeComponent();
        }

        #region property

        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                if (this.email == value)
                {
                    return;
                }

                this.email = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the entered email is valid or invalid.
        /// </summary>
        public bool IsInvalidEmail
        {
            get
            {
                return this.isInvalidEmail;
            }

            set
            {
                if (this.isInvalidEmail == value)
                {
                    return;
                }

                this.isInvalidEmail = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that is bound with an entry that gets the password from user in the login page.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.password = value;
                this.NotifyPropertyChanged();
            }
        }

        #endregion

        #region methods
        private async void LoginClicked(object obj)
        {
            if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Password))
            {

            }
            else
            {
                StringToBooleanConverter converter = new StringToBooleanConverter();
                if (converter.Convert(Email))
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Please enter valid email address", "Ok");
                }
                else
                {
                    try
                    {
                        RestClient client = new RestClient(App.client);
                        App.TheUser = await client.LoginAsync(Email, Password);
                        if (App.TheUser.Uname.Any())
                        {
                            NavHelper.InserPageBefore(new MainNavigationPage());
                            await NavHelper.PopAsync();
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Alert", "Unable to login, please try again", "Ok");
                        }
                    }
                    catch(Exception e)
                    {
                        //await Application.Current.MainPage.DisplayAlert("error", e.Message+"\n\n"+e.InnerException+"\n\n"+e.Source+"\n\n"+e.StackTrace+"\n\n", "OK");
                    }
                }
            }
        }

        private async void SignUpClicked(object obj)
        {
            await NavHelper.PushAsync(new SignUpPage());
        }

        private async void ForgotPasswordClicked(object obj)
        {
            //var label = obj as Label;
            //label.BackgroundColor = Color.FromHex("#70FFFFFF");
            //await Task.Delay(100);
            //label.BackgroundColor = Color.Transparent;
            await NavHelper.PushAsync(new ForgotPasswordPage());
        }

        //private void SocialLoggedIn(object obj)
        //{
        //    // Do something
        //}

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}