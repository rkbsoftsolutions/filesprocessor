using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace rkbmarketplace.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserDetailController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        public UserDetailController(IHttpClientFactory httpClientFactory)
        {

            this._httpClient = httpClientFactory.CreateClient("Context");
        }
        public async Task<IActionResult> Index()
        {
            using var request = new HttpRequestMessage {
                Method = HttpMethod.Get,
                RequestUri = new Uri("http://localhost:5001/api/home/getdetail")
            };

           using var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
           await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return Ok();
        }
    }
}