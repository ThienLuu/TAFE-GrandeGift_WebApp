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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Luu_DiplomaProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.HttpOnly = true;
                }
            );
            services.AddScoped<IDataService<Customer>, DataService<Customer>>();
            services.AddScoped<IDataService<Category>, DataService<Category>>();
            services.AddScoped<IDataService<Hamper>, DataService<Hamper>>();
            services.AddScoped<IDataService<Address>, DataService<Address>>();
            services.AddScoped<IDataService<Item>, DataService<Item>>();
            services.AddScoped<IDataService<Order>,DataService<Order>>();
            services.AddScoped<IDataService<OrderDetail>, DataService<OrderDetail>>();
            services.AddScoped<IDataService<Cart>, DataService<Cart>>();

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
            //services.AddDbContext<MyDbContext>();

            //AZURE - Connection String
            services.AddDbContext<MyDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //AZURE
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

            //Seed - Create data upon first startup with new, empty, database 
            SeedHelper.Seed(app.ApplicationServices).Wait();
        }
    }
}
