using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Routing;
using DutchTreat.Test;
using DutchTreat.Services;
using DutchTreat.Data;
using System.Reflection;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace DutchTreat
{
    public class Startup
    {
        private readonly IConfiguration config;

        public Startup(IConfiguration config) {
            this.config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IMailService, DummyMailService>();
            services.AddControllersWithViews().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddTransient<DbSeeder>();
            services.AddScoped<DutchContext>();
            services.AddScoped<IDutchRepository, DutchRepository>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddIdentity<StoreUser, IdentityRole>().AddEntityFrameworkStores<DutchContext>();
            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer( cfg => cfg.TokenValidationParameters = new TokenValidationParameters() {
                    ValidIssuer = this.config["Token:Issuer"],
                    ValidAudience = this.config["Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.config["Token:Key"]))
        });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(AddEndPoints);
                     
        }

        public void AddEndPoints(IEndpointRouteBuilder endpointBuilder) { 
            endpointBuilder.MapControllerRoute("Default","/{controller}/{action}/{id?}",new { controller = "App",action = "Index"});
        }

    }
}
