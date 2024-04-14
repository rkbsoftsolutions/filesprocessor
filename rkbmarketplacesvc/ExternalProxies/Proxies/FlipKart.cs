using EsearchSvc.Models.Entities;
using EsearchSvc.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace rkbmarketplace.ExternalProxies.Proxies
{
    public class FlipKart : SearchBase
    {
        private readonly HttpClient _httpClient;
        public FlipKart(IHttpClientFactory factory)
        {
            _httpClient=factory.CreateClient("Context");
        }
      public override async Task<bool> Search(string a)
        {

            var result = await _httpClient.GetAsync("https://gmail.com/");
            var str = "fnaxtyyzz";
            var dic = new Dictionary<char, int>();
            foreach (char c in str)
            {
                dic[c] = dic.ContainsKey(c) ? dic[c] + 1 : dic[c] = 1;
            }

            var filterMultipleValuesKeys = dic.Where(x => x.Value > 1);
            char key = filterMultipleValuesKeys.First().Key;
            int value = filterMultipleValuesKeys.First().Value;
            foreach (var kp in filterMultipleValuesKeys)
            {
                if (((int)kp.Key) < ((int)key))
                {
                    key = (char)kp.Key;
                    value = (char)kp.Value;

                }
            }

            Console.WriteLine($" {key} and {value}");
            return true;
        }

       
    }
}
