using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationSvc.IdentityClasses;
using AuthenticationSvc.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RefrigenatorSvc.Models;

namespace RefrigenatorSvc.Controllers.Api
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly ITokenProcessor _tokenProcessor;
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger, UserManager<ApplicationUsers> userManager, SignInManager<ApplicationUsers> signManager,
            IConfiguration configuration, ITokenProcessor tokenProcessor)
        {
            _logger = logger;
            _userManager = userManager;
            _tokenProcessor = tokenProcessor;
        }


        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AuthenticationModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                model.UserName = model.Email;
                model.UserId = user.Id;
              var token =  _tokenProcessor.GetToken(model, new List<string> { "Admin" });
                return Ok(token);
            }
            _logger.LogError($"{model.Email} not found");
            return Unauthorized();
        }
    }
}