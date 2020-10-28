using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PayVantage.Models;
using Xamarin.Forms;

namespace PayVantage.Helpers
{
    /// <summary>
    /// RestClient implements methods for calling CRUD operations
    /// using HTTP.
    /// </summary>
    public class RestClient
    {
        HttpClient httpClient;
        public RestClient(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<User> LoginAsync(string user, string paskey)
        {
            var usar = new User();
            var userObj = new { user, paskey };
            var json = JsonConvert.SerializeObject(userObj);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Constants.ipKey);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync(Constants.loginUrl, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    //await Application.Current.MainPage.DisplayAlert("Info", content, "Ok");

                    usar = JsonConvert.DeserializeObject<User>(content);
                }
                else
                {
                    //await Application.Current.MainPage.DisplayAlert("Info", "Unable to login, check network...", "Ok");
                    //await Application.Current.MainPage.DisplayAlert("Info", response.StatusCode.ToString(), "Ok");
                    usar = null;
                }
            }
            catch (Exception e)
            {
                //await Application.Current.MainPage.DisplayAlert("Info", "Unknow error, try again", "Ok");
                //await Application.Current.MainPage.DisplayAlert("Error", e.Message, "Ok");
                usar = null;
            }

            return usar;
        }

        public async Task<bool> ChangePasswordAsync(string user, string paskey, string usertimes)
        {
            var usar = new User();
            var userObj = new { user, paskey, usertimes };
            var json = JsonConvert.SerializeObject(userObj);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Constants.ipKey);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync(Constants.changePassUrl, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    //await Application.Current.MainPage.DisplayAlert("Info", content, "Ok");

                    dynamic obj = JObject.Parse(content);

                    if (((string)obj.data) == "Success")
                    {
                        return true;
                    }
                }
                else
                {
                    //await Application.Current.MainPage.DisplayAlert("Error", response.StatusCode.ToString(), "Ok");
                }
            }
            catch (Exception e)
            {
                //await Application.Current.MainPage.DisplayAlert("Error", e.Message, "Ok");
            }

            return false;
        }

        public async Task<bool> LogoutAsync(string user)
        {
            var userObj = new { user };
            var json = JsonConvert.SerializeObject(userObj);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Constants.ipKey);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(Constants.logoutUrl, httpContent);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                //await Application.Current.MainPage.DisplayAlert("Info", content, "Ok");
                dynamic obj = JObject.Parse(content);

                if (((string)obj.status) == "Success")
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<List<Category>> GetCategoriesAsync(string user, string tokenkey)
        {
            //await Application.Current.MainPage.DisplayAlert("userinfo", user + " " + tokenkey, "Ok");
            var categories = new List<Category>();
            var userObj = new { user, tokenkey };
            var jsonObj = JsonConvert.SerializeObject(userObj);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Constants.ipKey);
            HttpContent httpContent = new StringContent(jsonObj, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync(Constants.categUrl, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    //await Application.Current.MainPage.DisplayAlert("Info", json, "Ok");

                    JObject content = JsonConvert.DeserializeObject<dynamic>(json);
                    if (content.Value<string>("data") == "Failed")
                    {
                        await Application.Current.MainPage.DisplayAlert("token", "Unable to get data...", "Ok");
                    }
                    else
                    {
                        var _categories = content.Value<JArray>("categories");

                        foreach (var category in _categories)
                        {
                            categories.Add(new Category
                            {
                                CatId = category.Value<string>("catid"),
                                CatName = category.Value<string>("catname")
                            });
                        }
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("token", "Unable to get data...", "Ok");
                }
            }
            catch (Exception e)
            {
                //await Application.Current.MainPage.DisplayAlert("token", "Unable to get data...", "Ok");
                //await Application.Current.MainPage.DisplayAlert("Exception", e.Message + "\n\n" + e.InnerException + "\n\n" + e.Source + "\n\n" + e.StackTrace + "\n\n", "Ok");
            }
            return categories;
        }

        public async Task<List<Product>> GetProductsAsync(string user, string tokenkey, string categ)
        {
            //await Application.Current.MainPage.DisplayAlert("userinfo", user + " " + tokenkey + " " + categ, "Ok");
            var products = new List<Product>();
            var userObj = new { user, tokenkey, categ };
            var jsonObj = JsonConvert.SerializeObject(userObj);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Constants.ipKey);
            HttpContent httpContent = new StringContent(jsonObj, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync(Constants.prodUrl, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    //await Application.Current.MainPage.DisplayAlert("Info", json, "Ok");

                    JObject content = JsonConvert.DeserializeObject<dynamic>(json);
                    if (json.Length < 32)
                    {
                        if (content.Value<string>("data") == "Failed")
                        {
                            await Application.Current.MainPage.DisplayAlert("token", "Unable to get data...", "Ok");
                        }
                    }
                    else
                    {
                        var _products = content.Value<JArray>("products");
                        App.TheUser.TheKey = content.Value<JArray>("data")[0].Value<string>("sess");

                        foreach (var product in _products)
                        {
                            products.Add(new Product
                            {
                                ProductId = product.Value<string>("productid"),
                                ProdName = product.Value<string>("prodname"),
                                Vendor = product.Value<string>("vendor"),
                                Logo = Constants.baseUrl + product.Value<string>("vendorlogo"),
                                CatId = product.Value<string>("catid")
                            });
                        }
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("token", "Unable to get data...", "Ok");
                    //await Application.Current.MainPage.DisplayAlert("Responeerror", response.StatusCode.ToString(), "Ok");
                }
            }
            catch (Exception e)
            {
                //await Application.Current.MainPage.DisplayAlert("token", "Unable to get data...", "Ok");
                //await Application.Current.MainPage.DisplayAlert("Exception", e.Message + "\n\n" + e.InnerException + "\n\n" + e.Source + "\n\n" + e.StackTrace + "\n\n", "Ok");
            }
            return products;
        }

        public async Task<List<Transaction>> GetTransDetailsAsync(string user, string tokenkey)
        {
            //await Application.Current.MainPage.DisplayAlert("userinfo", user + " " + tokenkey, "Ok");
            var transactions = new List<Transaction>();
            var transObj = new { user, tokenkey };
            var jsonObj = JsonConvert.SerializeObject(transObj);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Constants.ipKey);
            HttpContent httpContent = new StringContent(jsonObj, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync(Constants.transDetailsUrl, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    //await Application.Current.MainPage.DisplayAlert("Info", json, "Ok");

                    JObject content = JsonConvert.DeserializeObject<dynamic>(json);
                    if (content.Value<string>("data") == "Failed")
                    {
                        await Application.Current.MainPage.DisplayAlert("token", "Unable to get data...", "Ok");
                    }
                    else
                    {
                        var _transactions = content.Value<JArray>("transdet");

                        foreach (var transaction in _transactions)
                        {
                            transactions.Add(new Transaction
                            {
                                VendId = transaction.Value<string>("vendid"),
                                ProdName = transaction.Value<string>("prodname"),
                                Amount = transaction.Value<string>("amount"),
                                AccountNum = transaction.Value<string>("accnos"),
                                VendTime = transaction.Value<string>("vendtime"),
                                Status = transaction.Value<string>("status"),
                                TransId = transaction.Value<string>("transid")
                            });
                        }
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("token", "Unable to get data...", "Ok");
                    //await Application.Current.MainPage.DisplayAlert("Responeerror", response.StatusCode.ToString(), "Ok");
                }
            }
            catch (Exception e)
            {
                //await Application.Current.MainPage.DisplayAlert("token", "Unable to get data...", "Ok");
                //await Application.Current.MainPage.DisplayAlert("Exception", e.Message + "\n\n" + e.InnerException + "\n\n" + e.Source + "\n\n" + e.StackTrace + "\n\n", "Ok");
            }
            return transactions;
        }

        public async Task<string[]> VendAsync(string user, string tokenkey, string prodid, string utransid, string msid, string amount, string candemail)
        {
            //await Application.Current.MainPage.DisplayAlert("userinfo", user + " " + tokenkey, "Ok");

            string[] ret = new string[2];
            var vendObj = new { user, tokenkey, prodid, utransid, msid, amount, candemail };
            var jsonObj = JsonConvert.SerializeObject(vendObj);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Constants.ipKey);
            HttpContent httpContent = new StringContent(jsonObj, Encoding.UTF8, "application/json");
            try
            {
                var response = await httpClient.PostAsync(Constants.vendUrl, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    await Application.Current.MainPage.DisplayAlert("Info", json, "Ok");

                    JObject content = JsonConvert.DeserializeObject<dynamic>(json);
                    if (content.Value<string>("data") == "Failed")
                    {
                        //await Application.Current.MainPage.DisplayAlert("token", "Unable to get data...", "Ok");
                        ret[0] = "Unable to get data...";
                        ret[1] = "failed";
                        return ret;
                    }
                    else
                    {

                        App.TheUser.TheKey = !string.IsNullOrEmpty(content.Value<string>("sess")) ? App.TheUser.TheKey : content.Value<string>("sess");
                        if (content.Value<string>("airtimeStatus") == "Success")
                        {
                            App.TheUser.VendBal = content.Value<string>("thevbal");
                            App.TheUser.ProfBal = content.Value<string>("theprfbal");
                            App.TheUser.TotBal = App.TheUser.VendBal + App.TheUser.ProfBal;
                            ret[0] = content.Value<string>("statusMessage");
                            ret[1] = "success";
                            return ret;
                        }
                        //await Application.Current.MainPage.DisplayAlert("Status", content.Value<string>("airtimeStatus"), "Ok");

                        ret[0] = content.Value<string>("statusMessage");
                        ret[1] = "failed";
                        return ret;
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("token", "Unable to get data...", "Ok");
                    //await Application.Current.MainPage.DisplayAlert("Responeerror", response.StatusCode.ToString(), "Ok");
                    ret[0] = "Network Error";
                    ret[1] = "failed";
                    return ret;
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("token", "Unable to get data...", "Ok");
                //await Application.Current.MainPage.DisplayAlert("Exception", e.Message + "\n\n" + e.InnerException + "\n\n" + e.Source + "\n\n" + e.StackTrace + "\n\n", "Ok");
                ret[0] = "Unknown Error";
                ret[1] = "failed";
                return ret;
            }
        }
    }
}
