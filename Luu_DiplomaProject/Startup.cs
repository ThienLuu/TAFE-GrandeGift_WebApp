using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
//..
using Microsoft.AspNetCore.Identity;
using Luu_DiplomaProject.Services;
using Luu_DiplomaProject.Models;


namespace Luu_DiplomaProject
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<IDataService<Customer>, DataService<Customer>>();
            //services.AddScoped<IDataService<Customer>, DataService<Customer>>();
            //services.AddScoped<IDataService<Hamper>, DataService<Hamper>>();
            services.AddIdentity<IdentityUser, IdentityRole>
            (
                config =>
                {
                    config.User.RequireUniqueEmail = true;
                    config.Password.RequiredLength = 8;
                    config.Password.RequireDigit = true;
                    config.Password.RequireLowercase = true;
                    config.Password.RequireUppercase = true;
                    config.Password.RequireNonAlphanumeric = true;
                }
            ).AddEntityFrameworkStores<MyDbContext>();
            services.AddDbContext<MyDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

            //seed
            SeedHelper.Seed(app.ApplicationServices).Wait();
        }
    }
}
