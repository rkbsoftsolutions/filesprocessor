using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthenticationSvc.IdentityClasses;
using AuthenticationSvc.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RefrigenatorSvc.Models;

namespace RefrigenatorSvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUsers> userManager;
        private readonly SignInManager<ApplicationUsers> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenProcessor _tokenProcessor;

        public AccountController(UserManager<ApplicationUsers> userManager, SignInManager<ApplicationUsers> signManager, 
            IConfiguration configuration, ITokenProcessor tokenProcessor)
        {
            this.userManager = userManager;
            _configuration = configuration;
            _tokenProcessor = tokenProcessor;
            _signInManager = signManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new AuthenticationModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(AuthenticationModel model)
        {
            var user = await userManager.FindByNameAsync(model.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                model.UserName = model.Email;
                //Create the identity for the user
                var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("Index", "Home");

               
            }
            return RedirectToAction("Index", "Account");
        }


        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Account");
        }
        [HttpGet]
        [Route("register")]
        public IActionResult register()
        {
            return View(new AuthenticationModel());
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(AuthenticationModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User already exists!" });

            ApplicationUsers user = new ApplicationUsers()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email,
                Id = Guid.NewGuid()

            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return View(model);
            var result1 = await userManager.AddToRoleAsync(user, "User");
            return RedirectToActionPermanent("Index", "Account");
        }

        
    }
}