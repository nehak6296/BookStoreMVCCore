using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelsLayer;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store_MVC_Core.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountManager accountManager;
        private readonly IConfiguration configuration;
        public AccountController(IAccountManager accountManager, IConfiguration configuration)
        {
            this.accountManager = accountManager;
            this.configuration = configuration;
        }

        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        //public async Task<ActionResult> Login(string returnUrl)
        //{
        //    Login login = new Login()
        //    {
        //        ReturnUrl = returnUrl,
        //        ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
        //    };

        //    return View(login);
        //}

        //[HttpPost]
        //public IActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //    var redirectUrl = Url.Action("ExternalLoginCallBack","Account",new { ReturnUrl = returnUrl });
        //    var properties = signInManager.ConfigureExternalAuthenticationProperties(provider,redirectUrl);
        //    return new ChallengeResult(provider, properties);
        //}

        [HttpPost]
        public JsonResult Login([FromBody]Login login)
        {
            try
            {
                if (IsValid(login))
                {
                    var jwtToken = GenerateJwtToken(login);
                    //return JsonResult();
                    return  Json(new { status = true, Message = "Login Successfull", Data = jwtToken });
                }
                else
                {
                    return  Json(new { status = false, Message = "Login UnSuccessfull" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsValid(Login login)
        {
            return this.accountManager.LoginUser(login);
        }
        [HttpPost]
        public ActionResult Register(Register register)
        {
            try
            {
                var result = this.accountManager.RegisterUser(register);
                ViewBag.Message = "User registered successfully";
                return RedirectToAction("Login");
            }
            catch (Exception)
            {
                return ViewBag.Message = "Unsuccessfull";
            }
        }
        private string GenerateJwtToken(Login user)
        {
            var securityKey = Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]);

            var claims = new Claim[] {
                    new Claim(ClaimTypes.Email,user.Email),

                };

            var credentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
              configuration["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);          

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
