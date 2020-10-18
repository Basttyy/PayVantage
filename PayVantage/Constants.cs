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
        public static string baseUrl = "https://payvantage.com.ng/api2/merchant/";
        public static string loginUrl = baseUrl + "login/";
        public static string logoutUrl = baseUrl + "logoff/";
        public static string categUrl = baseUrl + "thecats/";
        public static string prodUrl = baseUrl + "theprods/";
        public static string vendUrl = baseUrl + "vends/";
        public static string transDetailsUrl = baseUrl + "transdetails/";
        public static string changePassUrl = baseUrl + "chpasser/";
    }
}
