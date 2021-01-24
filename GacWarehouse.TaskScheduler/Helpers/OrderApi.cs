using GacWarehouse.TaskScheduler.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GacWarehouse.TaskScheduler.Helpers
{
    public class OrderApi
    {
        private readonly string _baseUrl = "https://localhost:44381/api/";
        private readonly string _authToken;

        public OrderApi()
        {
            var username = "CustomerB";
            var password = "password123";

            var authBytes = System.Text.Encoding.UTF8.GetBytes($"{username}:{password}");
            _authToken = Convert.ToBase64String(authBytes);
        }

        public async Task<GeneralResponse<T>> GetAPI<T>(string url, bool isAuthorized = true)
        {
            var generalResponse = new GeneralResponse<T>();

            try
            {
                using (var client = new HttpClient())
                {

                    //url
                    var fullUrl = $"{_baseUrl}{url}";

                    //  Token
                    if (isAuthorized && !string.IsNullOrWhiteSpace(_authToken))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _authToken);
                    }

                    //Pass in the full URL
                    var response = await client.GetAsync(fullUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        //It would be better to make sure this request actually made it through
                        string jsonResult = await response.Content.ReadAsStringAsync();

                        var result = JsonConvert.DeserializeObject<GeneralResponse<T>>(jsonResult);
                        if (result.Success)
                        {
                            generalResponse = result;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                generalResponse.Success   = false;
                generalResponse.Message = e.Message;
            }


            return generalResponse;
        }
        public async Task<GeneralResponse<T>> PostAPI<T, TT>(string url, TT model, bool isAuthorized = true)
        {
            var generalResponse = new GeneralResponse<T>();

            try
            {
                using (var client = new HttpClient())
                {
                    //url
                    var fullUrl = $"{_baseUrl}{url}";

                    // Token
                    if (isAuthorized && !string.IsNullOrWhiteSpace(_authToken))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _authToken);
                    }

                    //Converting the object to a json string. NOTE: Make sure the object doesn't contain circular references.
                    string json = JsonConvert.SerializeObject(model);

                    //Needed to setup the body of the request
                    StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

                    //Pass in the full URL and the json string content
                    var response = await client.PostAsync(fullUrl, data);

                    if (response.IsSuccessStatusCode)
                    {
                        //It would be better to make sure this request actually made it through
                        string result = await response.Content.ReadAsStringAsync();

                        generalResponse = JsonConvert.DeserializeObject<GeneralResponse<T>>(result);
                    }

                }
            }
            catch (Exception e)
            {
                generalResponse.Success = false;
                generalResponse.Message = e.Message;
            }


            return generalResponse;
        }
    }
}
