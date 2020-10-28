using PayVantage.Helpers;
using Plugin.Toast;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PayVantage.Views.Forms
{
    /// <summary>
    /// Page to reset old password
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPage : INotifyPropertyChanged
    {
        #region Fields

        private string currentPassword;

        private string newPassword;

        private string confirmPassword;
        private string changeState;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordPage" /> class.
        /// </summary>
        public ChangePasswordPage(string _changeState)
        {
            this.changeState = _changeState;
            this.SubmitCommand = new Command(this.SubmitClicked);
            this.BindingContext = this;
            InitializeComponent();
        }

        #region Event handler

        /// <summary>
        /// Occurs when the property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Submit button is clicked.
        /// </summary>
        public Command SubmitCommand { get; set; }

        #endregion

        #region Public property

        public string CurrentPassword
        {
            get
            {
                return this.currentPassword;
            }

            set
            {
                if (this.currentPassword == value)
                {
                    return;
                }

                this.currentPassword = value;
                this.NotifyPropertyChanged();
            }
        }

        public string NewPassword
        {
            get
            {
                return this.newPassword;
            }

            set
            {
                if (this.newPassword == value)
                {
                    return;
                }

                this.newPassword = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the new password confirmation from the user in the reset password page.
        /// </summary>
        public string ConfirmPassword
        {
            get
            {
                return this.confirmPassword;
            }

            set
            {
                if (this.confirmPassword == value)
                {
                    return;
                }

                this.confirmPassword = value;
                this.NotifyPropertyChanged();
            }
        }

        #endregion

        #region Methods

        private async void SubmitClicked(object obj)
        {
            //CrossToastPopUp.Current.ShowToastMessage("Feature is yet to be implemented...");
            //return;
            // Do something
            if (!string.IsNullOrEmpty(NewPassword) && string.IsNullOrEmpty(ConfirmPassword) && NewPassword == ConfirmPassword)
            {
                var client = new RestClient(App.client);

                if (await client.ChangePasswordAsync(App.TheUser.Uname, newPassword, this.changeState))
                    CrossToastPopUp.Current.ShowToastMessage("Password changed successfully...");
                else
                    CrossToastPopUp.Current.ShowToastMessage("Unable to change password, try again...");
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}