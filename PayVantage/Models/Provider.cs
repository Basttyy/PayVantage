using System;
using System.Collections.Generic;
using System.Text;

namespace PayVantage.Models
{
    public class Provider
    {
        public string VendorName { get; set; }
        public bool IsVisible { get; set; } = false;
        public List<Product> Products { get; set; }

        public Provider()
        {

        }
        public Provider(string vendor_name, List<Product> products)
        {
            VendorName = vendor_name;
            Products = products;
        }
    }
}
