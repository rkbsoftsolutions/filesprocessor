using ReferigenatorSvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Svc.Services
{
    public class StoreService : IStoreService
    {
        private const string BaseURl = "http://localhost:5001";
        private readonly HttpClient _httpClient;
        public StoreService(IHttpClientFactory httpFactory)
        {
            _httpClient = httpFactory.CreateClient("Context");
        }
        public async Task<bool> AddItem(Guid UserId,ItemViewModel itemViewModel)
        {
            using var hp = new HttpRequestMessage(HttpMethod.Post, new Uri($"{BaseURl}/api/v1/Store/{UserId}"));
            hp.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(itemViewModel), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await _httpClient.SendAsync(hp);
            httpResponse.EnsureSuccessStatusCode();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<List<ItemViewModel>> GetActiveRefrigenationItemsByUserId(Guid userId)
        {
            using var hp = new HttpRequestMessage(HttpMethod.Get, new Uri($"{BaseURl}/api/v1/Store/{userId}"));
            HttpResponseMessage httpResponse = await _httpClient.SendAsync(hp);
            httpResponse.EnsureSuccessStatusCode();
           return Newtonsoft.Json.JsonConvert.DeserializeObject<List<ItemViewModel>>(await httpResponse.Content.ReadAsStringAsync());
        }

        public Task<List<ItemViewModel>> GetExpiredRefrigenationItems()
        {
            throw new NotImplementedException();
        }

        public async Task<ItemlUpsertViewModel> GetItemAndHistory(int Id)
        {
            using var hp = new HttpRequestMessage(HttpMethod.Get, new Uri($"{BaseURl}/api/v1/Store/ItemAndHistory/{Id}"));
            HttpResponseMessage httpResponse = await _httpClient.SendAsync(hp);
            httpResponse.EnsureSuccessStatusCode();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ItemlUpsertViewModel>(await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<ItemlUpsertViewModel> GetItemAndHistory(int id, bool included)
        {
            using var hp = new HttpRequestMessage(HttpMethod.Get, new Uri($"{BaseURl}/api/v1/Store/{id}/{included}"));
            HttpResponseMessage httpResponse = await _httpClient.SendAsync(hp);
            httpResponse.EnsureSuccessStatusCode();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ItemlUpsertViewModel>(await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<bool> UpsertRefrigenratorItems(ItemViewModel itemViewModel)
        {
            using var hp = new HttpRequestMessage(HttpMethod.Post, new Uri($"{BaseURl}/api/v1/Store"));
            hp.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(itemViewModel), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await _httpClient.SendAsync(hp);
            httpResponse.EnsureSuccessStatusCode();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(await httpResponse.Content.ReadAsStringAsync());
        }

        
    }
}
