using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using PayVantage.Helpers;

namespace PayVantage.Views.Forms
{
    /// <summary>
    /// Page to retrieve the password forgotten.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : INotifyPropertyChanged
    {
        #region Fields
        private string email;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ForgotPasswordPage" /> class.
        /// </summary>
        public ForgotPasswordPage()
        {
            this.SendCommand = new Command(this.SendEmail);
            this.SignupCommand = new Command(this.SignUpClicked);
            this.BindingContext = this;
            InitializeComponent();
        }

        #region Events Handler
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Commands
        public Command SendCommand { get; set; }
        public Command SignupCommand { get; set; }
        #endregion

        #region Properties
        public string Email
        {
            get { return this.email; }
            set
            {
                if (this.email == value)
                {
                    return;
                }
                this.email = value;
                this.NotifyPropertyChanged("email");
            }
        }
        #endregion

        #region Methods
        private async void SendEmail(object obj)
        {
            NavHelper.InserPageBefore(new LoginPage());
            await NavHelper.PopAsync();
        }
        private async void SignUpClicked(object obj)
        {
            //await NavHelper.
            await NavHelper.PushAsync(new SignUpPage());
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}