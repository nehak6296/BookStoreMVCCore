using Book_Store_MVC_Core.Models;
using Facebook;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store_MVC_Core.Controllers
{
    public class HomeController : Controller
    {
        string appid = string.Empty;
        string appsecret= string.Empty;
        public HomeController()
        {
            var configuration = GetConfiguration();
            appid = configuration.GetSection("AppId").Value;
            appsecret = configuration.GetSection("AppSecret").Value;
        }
        public IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("application.json",optional:true,reloadOnChange:true);
            return builder.Build();
        }
        private Uri RedirectUri
        {
            get
                {
                var uriBuilder = new UriBuilder(Request.Headers["Referer"].ToString());
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallBack");
                return uriBuilder.Uri;
                }
        }
        
        public IActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginurl = fb.GetLoginUrl(new
            {
                client_id = "1515725062105904",
                client_secret = "72e00ef543c0f0c7a8d39de784dac293",
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope="email"
            }) ;
            return Redirect(loginurl.AbsoluteUri);
        }
        public IActionResult FacebookCallBack(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = "1515725062105904",
                client_secret = "72e00ef543c0f0c7a8d39de784dac293",
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });
            var accesstoken=result.access_token;
            fb.AccessToken = accesstoken;
            dynamic data = fb.Get("me?fields=first_name,last_name,email,picture");
            TempData["email"] = data.email;
            TempData["name"] = data.first_name + data.last_name;
            TempData["picture"] = data.picture;
            return RedirectToAction("GetAllBooks", "Books");
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
