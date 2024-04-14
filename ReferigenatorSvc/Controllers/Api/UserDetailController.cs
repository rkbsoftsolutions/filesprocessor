using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthenticationSvc.IdentityClasses;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ReferigenatorSvc.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserDetailController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUsers> _userManager;
        public UserDetailController(ILogger<HomeController> logger, UserManager<ApplicationUsers> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        private Guid userId => HttpContext.User.Claims.Any(x => x.Type == ClaimTypes.NameIdentifier)
              ?
             Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value) :
             Guid.Empty;

        [HttpGet]
       public async Task<IActionResult> GetAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Name);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return Ok(new { user, email, userId });
        }
    }
}
