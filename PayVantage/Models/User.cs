using System;
using System.Collections.Generic;
using System.Text;

namespace PayVantage.Models
{
    public class User
    {
        #region Fields
        //private string uid, uname, phone, name;
        //private string utype, retailerid, subagid;
        //private string supid, vendbal, profbal;
        //private string totbal, thekey, pchanger, hash;
        #endregion

        #region Properties
        public string Uid { get; set; }
        public string Uname { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Utype { get; set; }
        public string RetailerId { get; set; }
        public string SubagId { get; set; }
        public string SupId { get; set; }
        public string VendBal { get; set; }
        public string ProfBal { get; set; }
        public string TotBal { get; set; }
        public string TheKey { get; set; }
        public string Pchanger { get; set; }
        #endregion
    }
}
