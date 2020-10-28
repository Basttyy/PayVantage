using System;
using PayVantage.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using PSC.Xamarin.MvvmHelpers;

namespace PayVantage.ViewModels
{
    public class VendorViewModel : ObservableRangeCollection<ProductViewModel>, INotifyPropertyChanged
    {
        private ObservableRangeCollection<ProductViewModel> vendProducts = new ObservableRangeCollection<ProductViewModel>();
        public VendorViewModel(Vendor vendor, bool expanded = false)
        {
            this.Vendor = vendor;
            this._expanded = expanded;

            foreach(Product product in vendor.Products)
            {
                vendProducts.Add(new ProductViewModel(product));
            }
            if (expanded)
                this.AddRange(vendProducts);
        }
        public VendorViewModel() { }

        private bool _expanded;
        public bool Expanded
        {
            get { return _expanded; }
            set
            {
                if (!_expanded.Equals(value))
                {
                    _expanded = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Expanded"));
                    OnPropertyChanged(new PropertyChangedEventArgs("DownIcon"));
                    OnPropertyChanged(new PropertyChangedEventArgs("UpIcon"));
                    if (_expanded)
                    {
                        this.AddRange(vendProducts);
                    }
                    else
                    {
                        this.Clear();
                    }
                }
            }
        }
        public bool DownIcon
        {
            get
            {
                if (Expanded)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public bool UpIcon
        {
            get
            {
                if (Expanded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public string VendorName { get { return Vendor.VendorName; } }
        public string VendorLogo { get { return Vendor.VendorLogo; } }
        public Vendor Vendor { get; set; }
    }
}
