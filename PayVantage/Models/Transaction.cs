using System;
using System.Collections.Generic;
using System.Text;

namespace PayVantage.Models
{
    public class Transaction
    {
        #region Fields
        //private string uid, uname, phone, name;
        //private string utype, retailerid, subagid;
        //private string supid, vendbal, profbal;
        //private string totbal, thekey, pchanger, hash;
        #endregion

        #region Properties
        public string VendId { get; set; }
        public string ProdName { get; set; }
        public string Amount { get; set; }
        public string AccountNum { get; set; }
        public string VendTime { get; set; }
        public string Status { get; set; }
        public string TransId { get; set; }

        #endregion
    }
}
