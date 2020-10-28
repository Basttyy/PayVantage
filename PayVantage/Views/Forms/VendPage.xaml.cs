using PayVantage.Helpers;
using PayVantage.ViewModels;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PayVantage.Views.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VendPage : ContentPage, INotifyPropertyChanged
    {
        private bool acctNumVisible, emailVisible, amntVisible;
        private int acctMaxLength;
        private string catId, prodId;
        private string acctNumber = "", amount = "", email = "";

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the add card button is clicked.
        /// </summary>
        public Command VendCommand { get; set; }

        #endregion

        public VendPage(string _catId, string _prodId)
        {
            VendCommand = new Command(VendClicked);
            this.BindingContext = this;
            InitializeComponent();
            this.catId = _catId;
            this.prodId = _prodId;
            this.AcctMaxLength = 19;
            switch (_catId)
            {
                case "1":
                case "3":
                    AmntVisible = false; EmailVisible = false; AcctNumVisible = true;
                    break;
                case "2":
                case "4":
                case "5":
                case "8":
                    AmntVisible = true; EmailVisible = false; AcctNumVisible = true;
                    break;
                case "9":
                    AmntVisible = false; EmailVisible = true; AcctNumVisible = true;
                    break;
                default:
                    AmntVisible = true; EmailVisible = true; AcctNumVisible = true;
                    break;
            }
        }

        #region Properties

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the card number from user.
        /// </summary>
        public bool AmntVisible
        {
            get
            {
                return this.amntVisible;
            }

            set
            {
                if (this.amntVisible == value)
                {
                    return;
                }

                this.amntVisible = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the card number from user.
        /// </summary>
        public bool AcctNumVisible
        {
            get
            {
                return this.acctNumVisible;
            }

            set
            {
                if (this.acctNumVisible == value)
                {
                    return;
                }

                this.acctNumVisible = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the card number from user.
        /// </summary>
        public bool EmailVisible
        {
            get
            {
                return this.emailVisible;
            }

            set
            {
                if (this.emailVisible == value)
                {
                    return;
                }

                this.emailVisible = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the card number from user.
        /// </summary>
        public int AcctMaxLength
        {
            get
            {
                return this.acctMaxLength;
            }

            set
            {
                if (this.acctMaxLength == value)
                {
                    return;
                }

                this.acctMaxLength = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the card number from user.
        /// </summary>
        public string AcctNumber
        {
            get
            {
                return this.acctNumber;
            }

            set
            {
                if (this.acctNumber == value)
                {
                    return;
                }
                if ((this.acctNumber == null || value.Length > this.acctNumber.Length) &&
                    value.Length % 5 == 4 && value.Length != 19)
                {
                    this.acctNumber = string.Concat(value, "-");
                } else
                {
                    this.acctNumber = value;
                }
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the expire date from user.
        /// </summary>
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the cvv number from user.
        /// </summary>
        public string Amount
        {
            get
            {
                return this.amount;
            }

            set
            {
                if (this.amount == value)
                {
                    return;
                }

                this.amount = value;
                this.OnPropertyChanged();
            }
        }

        #endregion

        #region Event handler

        /// <summary>
        /// Occurs when the property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the add card button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void VendClicked(object obj)
        {
            if (AcctNumVisible && EmailVisible && AmntVisible)
            {
                if (string.IsNullOrEmpty(AcctNumber) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Amount))
                {
                    CrossToastPopUp.Current.ShowToastMessage("Please fill all entries"); return;
                }
            }
            else if (!AmntVisible && EmailVisible && AcctNumVisible)
            {
                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(AcctNumber))
                {
                    CrossToastPopUp.Current.ShowToastMessage("Please fill all entries"); return;
                }
            }
            else if (AmntVisible && !EmailVisible && AcctNumVisible)
            {
                if (string.IsNullOrEmpty(Amount) || string.IsNullOrEmpty(AcctNumber))
                {
                    CrossToastPopUp.Current.ShowToastMessage("Please fill all entries"); return;
                }
            }
            else if (!AmntVisible && !EmailVisible && AcctNumVisible)
            {
                if (string.IsNullOrEmpty(AcctNumber))
                {
                    CrossToastPopUp.Current.ShowToastMessage("Please fill all entries"); return;
                }
            }
            var client = new RestClient(App.client);
            var response = await client.VendAsync(App.TheUser.Uname, App.TheUser.TheKey, this.prodId, Guid.NewGuid().ToString(), AcctNumber, Amount, Email);
            if (response[1] == "Pending")
            {
                Amount = ""; AcctNumber = ""; Email = "";
                await Application.Current.MainPage.DisplayAlert("Status", response[0] + ", you will be credited shortly.", "Ok");
                return;
            }
            await Application.Current.MainPage.DisplayAlert("Ooop's...", response[0], "Ok");
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}