using System;
using System.Collections.Generic;
using System.Text;

namespace PayVantage
{
    public static class Constants
    {
        public static string ipKey = "BG:77:W5:21:WER:13";
        public static string username = "abiolaashiru84@gmail.com";
        public static string password = "mmm";
        public static string baseUrl = "https://payvantage.com.ng/";
        public static string apiUrl = "https://payvantage.com.ng/api2/merchant/";
        public static string loginUrl = apiUrl + "login/";
        public static string logoutUrl = apiUrl + "logoff/";
        public static string categUrl = apiUrl + "thecats/";
        public static string prodUrl = apiUrl + "theprods/";
        public static string vendUrl = apiUrl + "vends/";
        public static string transDetailsUrl = apiUrl + "transdetails/";
        public static string changePassUrl = apiUrl + "chpasser/";
        public static string changePassFirst = "0";
        public static string changePassNotFirst = "1";
        public static string changeVpassFirst = "2";
        public static string changeVpassNotFirst = "3";
    }
}
