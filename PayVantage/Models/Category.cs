using System;
using System.Collections.Generic;
using System.Text;

namespace PayVantage.Models
{
    public class Category
    {
        #region Fields
        //private string uid, uname, phone, name;
        //private string utype, retailerid, subagid;
        //private string supid, vendbal, profbal;
        //private string totbal, thekey, pchanger, hash;
        #endregion

        #region Properties
        public string CatId { get; set; }
        public string CatName { get; set; }
        #endregion
    }
}
