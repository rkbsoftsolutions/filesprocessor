using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthenticationSvc.IdentityClasses;
using AuthenticationSvc.Interface;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ReferigenatorSvc.Filters;
using ReferigenatorSvc.Hub.NotificationHub;
using ReferigenatorSvc.Models;
using RefrigenatorSvc.Models;
using Svc.Services;

namespace ReferigenatorSvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStoreService _referigenatorService;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly List<StorageTypes> _storageTyeps;
        private IServiceProvider services;
        private readonly ITokenProcessor _tokenProcessor;

        public HomeController(ILogger<HomeController> logger,IStoreService referigenatorService,
            IHubContext<NotificationHub> hubContext,IOptions<List<StorageTypes>> storageTyps, ITokenProcessor tokenProcessor)
        {
            _logger = logger;
            _referigenatorService = referigenatorService;
            _hubContext = hubContext;
            _storageTyeps = storageTyps.Value;
            _tokenProcessor = tokenProcessor;

        }

        private Guid userId => HttpContext.User.Claims.Any(x => x.Type == ClaimTypes.NameIdentifier)
              ?
             Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value) :
             Guid.Empty;

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Function : Index");
            var allActiveItems = await _referigenatorService.GetActiveRefrigenationItemsByUserId(userId);
            return View(new ItemlUpsertViewModel { 
                storageTypes =this._storageTyeps,
                ItemViewModel = new ItemViewModel(),
                currentActiveItems = allActiveItems
            });
        }
        [Route("History/{id}")]
        public async Task<IActionResult> History([FromRoute] int Id)
        {
            if (Id > 0)
            {
                var allActiveItems = await _referigenatorService.GetItemAndHistory(Id);
                return View(allActiveItems);
            }
            return RedirectToAction("Index");
        }

        [Route("GetItemDetail/{id}")]
        [HttpGet]
        public async Task <IActionResult> GetItemDetail([FromRoute] int id)
        {
            if (id > 0)
            {
                var allActiveItems = await _referigenatorService.GetItemAndHistory(id, false);

              
                return Json(allActiveItems);
            }
            return Json(false);
        }

        [HttpPost]
        [ServiceFilterAttribute(typeof(TransactionRequiredAttribute))]
        public async Task<IActionResult> Index(ItemlUpsertViewModel itemModel )
        {
            if (ModelState.IsValid)
            {
                if (itemModel.ItemViewModel.UpdateItemQuantity > default(float) && itemModel.ItemViewModel.Id > 0)
                {
                    if( itemModel.ItemViewModel.UpdateItemQuantity> itemModel.ItemViewModel.ItemQuantity)
                    {
                        ModelState.AddModelError("Quantity", "Quantity Should not be greate then available item");
                        return View(itemModel);
                    }
                    if (itemModel.ItemViewModel.UpdateItemQuantity <=0)
                    {
                        ModelState.AddModelError("Quantity", "Quantity should be greater than zero");
                        return View(itemModel);
                    }

                    itemModel.ItemViewModel.ItemQuantity = itemModel.ItemViewModel.ItemQuantity - itemModel.ItemViewModel.UpdateItemQuantity;
                    await _referigenatorService.UpsertRefrigenratorItems(itemModel.ItemViewModel);
                    return RedirectToAction("Index");
                }
                else
                {
                    await _referigenatorService.AddItem(userId, itemModel.ItemViewModel).ConfigureAwait(false);
                    return RedirectToAction("Index");
                }
            }
            itemModel.storageTypes = this._storageTyeps;
            return View(itemModel);
        }

        public async Task<IActionResult> ExpiredItems()
        {
            var items = await _referigenatorService.GetExpiredRefrigenationItems();


            //Implement SignalR : If Any Schedular call this end point then send message by with all expired items.
            if (items != null)
            {
                await this._hubContext.Clients.All.SendCoreAsync("ReceiveMessage",
                    new object[] {
                   items.Select(x =>x.ItemName)
                    }).ConfigureAwait(false);
            }
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
