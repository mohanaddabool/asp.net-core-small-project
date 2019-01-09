using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using DutchTreat.Services;
using DutchTreat.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using AutoMapper;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DutchTreat
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _environment;

        public Startup(IConfiguration config , IHostingEnvironment environment)
        {
            _config = config;
            _environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<StoreUser, IdentityRole>(cfg =>
             {
                 cfg.User.RequireUniqueEmail = true;
             }).AddEntityFrameworkStores<DutchContext>();

            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer( cfg =>
                {
                    cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidIssuer = _config["Tokens:Issuer"],
                        ValidAudience = _config["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]))
                    };
                }
                );

            services.AddDbContext<DutchContext>(cfg =>
            {
                //cfg.UseSqlServer(_config.GetConnectionString("DutchConnectionString"));
                cfg.UseSqlServer(_config["ConnectionStrings:DutchConnectionString"]);
            });
            services.AddAutoMapper();
            services.AddTransient<IMailService, NullMailService>();
            services.AddTransient<Dutchseeder>();
            services.AddScoped<IDutchRepo, DutchRepo>();
            services.AddMvc(opt =>
            {
                if(_environment.IsProduction())
                {
                    opt.Filters.Add(new RequireHttpsAttribute());
                }
            }
            ).AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                      Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot" , @"node_modules")),
                    RequestPath = new PathString("/vendor")
                });
            }

            app.UseAuthentication();

            app.UseMvc(cfg =>
            {
                cfg.MapRoute("Default",
                    "{controller}/{action}/{id?}",
                    new { Controller = "App", Action = "Index" });
            });

            if(env.IsDevelopment())
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetService<Dutchseeder>();
                    seeder.Seeder().Wait();
                }
            }
        }
    }
}
