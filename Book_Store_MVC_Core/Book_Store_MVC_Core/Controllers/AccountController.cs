using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
