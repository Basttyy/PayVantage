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
                    await Application.Current.MainPage.DisplayAlert("Info", content, "Ok");

                    usar = JsonConvert.DeserializeObject<User>(content);
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", e.Message, "Ok");
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
                    await Application.Current.MainPage.DisplayAlert("Info", content, "Ok");

                    dynamic obj = JObject.Parse(content);

                    if ((string)obj.Status == "Success")
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", e.Message, "Ok");
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

                if ((string)obj.Status == "Success")
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<List<Category>> GetCategoriesAsync(string user, string tokenkey)
        {
            await Application.Current.MainPage.DisplayAlert("userinfo", user + " " + tokenkey, "Ok");
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
                    await Application.Current.MainPage.DisplayAlert("Info", json, "Ok");

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
                    await Application.Current.MainPage.DisplayAlert("Responeerror", response.StatusCode.ToString(), "Ok");
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Exception", e.Message + "\n\n" + e.InnerException + "\n\n" + e.Source + "\n\n" + e.StackTrace + "\n\n", "Ok");
            }
            return categories;
        }

        public async Task<List<Product>> GetProductsAsync(string user, string tokenkey)
        {
            await Application.Current.MainPage.DisplayAlert("userinfo", user + " " + tokenkey, "Ok");
            var products = new List<Product>();
            var userObj = new { user, tokenkey };
            var jsonObj = JsonConvert.SerializeObject(userObj);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Constants.ipKey);
            HttpContent httpContent = new StringContent(jsonObj, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync(Constants.prodUrl, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    await Application.Current.MainPage.DisplayAlert("Info", json, "Ok");

                    JObject content = JsonConvert.DeserializeObject<dynamic>(json);
                    if (content.Value<string>("data") == "Failed")
                    {
                        await Application.Current.MainPage.DisplayAlert("token", "Unable to get data...", "Ok");
                    }
                    else
                    {
                        var _products = content.Value<JArray>("products");

                        foreach (var product in _products)
                        {
                            products.Add(new Product
                            {
                                ProductId = product.Value<string>("productid"),
                                ProdName = product.Value<string>("prodname"),
                                Vendor = product.Value<string>("category"),
                                CatId = product.Value<string>("catid")
                            });
                        }
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Responeerror", response.StatusCode.ToString(), "Ok");
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Exception", e.Message + "\n\n" + e.InnerException + "\n\n" + e.Source + "\n\n" + e.StackTrace + "\n\n", "Ok");
            }
            return products;
        }

        public async Task<List<Transaction>> GetTransDetailsAsync(string user, string tokenkey)
        {
            await Application.Current.MainPage.DisplayAlert("userinfo", user + " " + tokenkey, "Ok");
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
                    await Application.Current.MainPage.DisplayAlert("Info", json, "Ok");

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
                    await Application.Current.MainPage.DisplayAlert("Responeerror", response.StatusCode.ToString(), "Ok");
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Exception", e.Message + "\n\n" + e.InnerException + "\n\n" + e.Source + "\n\n" + e.StackTrace + "\n\n", "Ok");
            }
            return transactions;
        }

        public async Task<string> VendAsync(string user, string tokenkey, string prodid, string utransid, string msid, string amount, string candemail)
        {
            await Application.Current.MainPage.DisplayAlert("userinfo", user + " " + tokenkey, "Ok");

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
                        await Application.Current.MainPage.DisplayAlert("token", "Unable to get data...", "Ok");
                        return "";
                    }
                    else
                    {
                        App.TheUser.TheKey = content.Value<string>("sess");
                        await Application.Current.MainPage.DisplayAlert("Status", content.Value<string>("airtimeStatus"), "Ok");
                        return "";
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Responeerror", response.StatusCode.ToString(), "Ok");
                    return "";
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Exception", e.Message + "\n\n" + e.InnerException + "\n\n" + e.Source + "\n\n" + e.StackTrace + "\n\n", "Ok");
                return "";
            }
        }
    }
}
