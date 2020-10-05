using ApiHelper.Models;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiHelper
{
    public class DogApiProcessor
    {
        public static async Task<List<string>> LoadBreedList()
        {
            var response = await ApiHelper.ApiClient.GetAsync("https://dog.ceo/api/breeds/list/all");
            if (!response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsAsync<ApiResponse<string>>();
                throw new Exception(json.Message);
            } 
            else
            {
                var json = await response.Content.ReadAsAsync<ApiResponse<Dictionary<string, List<string>>>>();
                return json.Message.Keys.ToList();
            }
        }

        public static async Task<string> GetImageUrl(string breed)
        {
            var response = await ApiHelper.ApiClient.GetAsync($"https://dog.ceo/api/breed/{breed}/images/random");
            if (!response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsAsync<ApiResponse<string>>();
                throw new Exception(json.Message);
            }
            else
            {
                var json = await response.Content.ReadAsAsync<ApiResponse<string>>();
                return json.Message;
            }
        }
    }
}
