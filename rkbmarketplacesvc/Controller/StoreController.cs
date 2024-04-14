using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReferigenatorSvc.dbcontext;
using ReferigenatorSvc.Models;
using ReferigenatorSvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rkbmarketplace.Controller
{
     [Authorize]
     [ApiController]
    [Route("api/v1/[controller]")]
    //[Route("api/v1/[Controller]")]
    public class StoreController : ControllerBase
    {
        private readonly IRefrigenatorService _refrigenatorService;
        public StoreController(IRefrigenatorService refrigenatorService)
        {
            _refrigenatorService = refrigenatorService;
        }

        [HttpGet()]
        [Route("{userId}")]
        public IActionResult HttpGet([FromRoute] Guid userId)
        {
            var response = _refrigenatorService.GetActiveRefrigenationItemsByUserId(userId);
            return Ok(response);
        }

        [HttpGet()]
        [Route("{itemId}/{inclueHistory}")]
        public IActionResult HttpGet([FromRoute] int itemId,[FromRoute] bool inclueHistory)
        {
            var response = _refrigenatorService.GetItemAndHistory(itemId, inclueHistory);
            return Ok(response);
        }

        [HttpGet()]
        [Route("ItemAndHistory/{itemId}")]
        public IActionResult HttpGet([FromRoute] int itemId)
        {
            var response = _refrigenatorService.GetItemAndHistory(itemId);
            return Ok(response);
        }

        [HttpPut]
        [Route("{itemId}")]
        public async Task< IActionResult> UpsertRefrigenratorItems([FromRoute] int itemId, [FromBody] ItemViewModel itemViewModel)
        {
            itemViewModel.Id = itemId;
            var response = await _refrigenatorService.UpsertRefrigenratorItems(itemViewModel.Adapt<ItemsEntity>());
            return Ok(response);
        }

        [HttpPost]
        [Route("{userId}")]
        public async Task< IActionResult> AddItem([FromRoute] Guid userId, [FromBody] ItemViewModel itemViewModel)
        {
            var item = itemViewModel.Adapt<ItemsEntity>();
            item.userId = userId;
            var response = await _refrigenatorService.AddRefrigenationItem(item);
            return Ok(response);
        }
    }
}
