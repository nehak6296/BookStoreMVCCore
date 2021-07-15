using ManagerLayer.Interfaces;
using ManagerLayer.Manager;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Book_Store_MVC_Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllersWithViews();
            services.AddMvc();
            services.AddTransient<IAccountManager, AccountManager>();
            services.AddTransient<IAccountRepo, AccountRepo>();
            services.AddTransient<IBooksManager, BooksManager>();
            services.AddTransient<IBooksRepo, BooksRepo>();
            services.AddTransient<ICartManager, CartManager>();
            services.AddTransient<ICartRepo, CartRepo>();
            services.AddTransient<ICustomerManager, CustomerManager>();
            services.AddTransient<ICustomerRepo, CustomerRepo>();
            services.AddTransient<IOrdersManager, OrdersManager>();
            services.AddTransient<IOrdersRepo, OrdersRepo>();
            services.AddTransient<IWishListManager, WishListManager>();
            services.AddTransient<IWishListRepo, WishListRepo>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            var key = Encoding.UTF8.GetBytes(Configuration["Jwt:Secret"]);
           
            services.AddAuthentication(x =>
            {               
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidIssuers = new string[] { Configuration["Jwt:Issuer"] },
                    ValidAudiences = new string[] { Configuration["Jwt:Issuer"] },
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true
                };
            });
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultChallengeScheme = FacebookDefaults.AuthenticationScheme;
            //    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //})
            //    .AddFacebook(options =>
            //    {
            //        options.AppId = "1515725062105904";
            //        options.AppSecret = "72e00ef543c0f0c7a8d39de784dac293";
            //    });
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
