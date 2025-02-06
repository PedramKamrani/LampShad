using _0_FrameWork.BaseClass;
using _0_FrameWork.BaseClass.Email;
using _0_FrameWork.BaseClass.Sms;
using _0_FrameWork.BaseClass.ZarinPal;
using _0_FrameWork.RepositoryBase;
using AccountManagment.Configure;
using BlogManagnentConfigureService;
using Comment.Mangment.Configure;
using ConfigurationLayer;
using ConfigurationService;
using IM.ManagmentConfigure;
using InventoryManagement.Presentation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceHost.Controllers;
using ServiceHost.Hubs;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using WebMarkupMin.AspNetCore5;

namespace ServiceHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddHttpContextAccessor();
            services.AddWebMarkupMin(options =>
                    {
                        options.AllowMinificationInDevelopmentEnvironment = true;
                        options.AllowCompressionInDevelopmentEnvironment = true;
                    })
                    .AddHtmlMinification()
                    .AddXmlMinification();
            //DBContext Service
            var connctionstring = Configuration.GetConnectionString("LamshadeDB");
            ShopManagmentBootstraper.Configure(services, connctionstring);
            DiscountManagmentBootStraper.Configure(services, connctionstring);
            InventoryManagementsBootstraper.Configure(services, connctionstring);
            BlogmanammentBootStraper.Configure(services, connctionstring);
            CommentManagmentBootstraper.Configure(services, connctionstring);
            AccountManagementBootstrapper.Configure(services, connctionstring);

            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
            services.AddSingleton<IPasswordHasher,PasswordHasher>();
            services.AddTransient<IFileUploader,FileUploader>();
            services.AddTransient<IAuthHelper, AuthHelper>();
            services.AddTransient<IZarinPalFactory, ZarinPalFactory>();
            services.AddTransient<ISmsService, SmsService>();
            services.AddTransient<IEmailService, EmailService>();
            

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.LoginPath = new PathString("/Account");
                    o.LogoutPath = new PathString("/Account");
                    o.AccessDeniedPath = new PathString("/AccessDenied");
                    o.ExpireTimeSpan = TimeSpan.FromDays(2);
                    o.Cookie.HttpOnly = true;
                    
                });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminArea",
                    builder => builder.RequireRole(new List<string> { Roles.Administrator, Roles.SystemUser }));

                options.AddPolicy("Shop",
                    builder => builder.RequireRole(new List<string> { Roles.Administrator }));

                options.AddPolicy("Discount",
                    builder => builder.RequireRole(new List<string> { Roles.Administrator }));

                options.AddPolicy("Account",
                    builder => builder.RequireRole(new List<string> { Roles.Administrator }));
            });

            services.AddCors(option => option.AddPolicy("MyApi", builder =>
               builder.WithOrigins("https://www.pkshop2020.ir")
               .AllowAnyHeader()
               .AllowAnyMethod()));
            
            services.AddRazorPages()
                 .AddMvcOptions(options => options.Filters.Add<SecurityPageFilter>())
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeAreaFolder("Admin", "/", "AdminArea");
                    options.Conventions.AuthorizeAreaFolder("Admin", "/Shop", "Shop");
                    options.Conventions.AuthorizeAreaFolder("Admin", "/Discounts", "Discount");
                    options.Conventions.AuthorizeAreaFolder("Admin", "/Accounts", "Account");
                }).AddApplicationPart(typeof(ProductController).Assembly)
                .AddApplicationPart(typeof(InventoryController).Assembly)
                .AddNewtonsoftJson();
            services.AddSignalR();
            services.AddProgressiveWebApp();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                 app.UseExceptionHandler("/Error");
                app.UseDeveloperExceptionPage();
                app.UseHsts();
            }
            
            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseWebMarkupMin();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("MyApi");
            //app.UseCors(build=> 
            //{
            //    build.WithOrigins("https://www.pkshop2020.ir/")
            //    .WithMethods("GET", "POSt")
            //    .AllowCredentials();
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapHub<ViewHub>("/Hubs/View");
            });
        }
    }
}
