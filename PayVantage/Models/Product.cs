using System;
using System.Collections.Generic;
using System.Text;

namespace PayVantage.Models
{
    public class Product
    {
        #region Fields
        //private string uid, uname, phone, name;
        //private string utype, retailerid, subagid;
        //private string supid, vendbal, profbal;
        //private string totbal, thekey, pchanger, hash;
        #endregion

        #region Properties
        public string ProductId { get; set; }
        public string ProdName { get; set; }
        public string Vendor { get; set; }
        public string Logo { get; set; }
        public string CatId { get; set; }

        #endregion

        public Product()
        {

        }
        public Product(string productId, string prodName, string vendor, string logo, string catId)
        {
            ProductId = productId;
            ProdName = prodName;
            Vendor = vendor;
            Logo = logo;
            CatId = catId;
        }
    }
}
