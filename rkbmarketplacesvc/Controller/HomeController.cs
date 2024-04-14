using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AuthenticationSvc;
using AuthenticationSvc.Entities;
using EsearchSvc.Models.Entities;
using EsearchSvc.Models.Interfaces;
using EsearchSvc.Services.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsearchSvc.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly ISearch _search;
        private readonly ISearchRefer<SearchCustomerRef> _searchCustomerRef;
        private readonly ISearchRefer<SearchUserRefer> _searchUserRefer;
        
        private readonly PlatfromdbContext _plaformdbContext;
        public HomeController(Func<string,int, ISearch> serviceResolver, 
            ISearchRefer<SearchUserRefer> searchUserRefer,
            ISearchRefer<SearchCustomerRef> searchCustomerRefer,
            ISearchServiceResolver searchServiceResolver,
            PlatfromdbContext plaformdbContext)
        {
            _plaformdbContext = plaformdbContext;
            _search = serviceResolver("FLIPKART",1);
            _searchUserRefer = searchUserRefer;
            _searchCustomerRef = searchCustomerRefer;
            var c = searchServiceResolver.Resolve("FlipKart");
               }

        // [FirstActionFilter]
        [HttpGet("getdetail")]
        public async Task<IActionResult> GetAction()
        {
            var newsEntity = _plaformdbContext.news.ToList<NewsEntity>();
            return Ok(newsEntity);
        }
        [HttpPost("postdetail")]
        [ServiceFilter(typeof(TransactionRequiredAttribute))]
        public async Task<IActionResult> PostAction()
        {
            var max=_plaformdbContext.news.Max(n => n.Id);
       var e= await _plaformdbContext.news.AddAsync(new NewsEntity
            {
                FullDetail = "Test",
                ShortDetail="Test",
                Title="Test",
                LanguageId=1,
               
           CreatedOnUtc=DateTime.UtcNow

       });
           await _plaformdbContext.SaveChangesAsync();
            return Ok(e.Entity);
        }
    }
}