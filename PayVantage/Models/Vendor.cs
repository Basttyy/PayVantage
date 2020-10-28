using System;
using System.Collections.Generic;
using System.Text;

namespace PayVantage.Models
{
    public class Vendor
    {
        public string VendorName { get; set; }
        public string VendorLogo { get; set; }
        public bool IsVisible { get; set; } = false;
        public List<Product> Products { get; set; }

        public Vendor()
        {

        }
        public Vendor(string vendor_name, string vendor_logo, List<Product> products)
        {
            VendorName = vendor_name;
            VendorLogo = vendor_logo;
            Products = products;
        }
    }
}
