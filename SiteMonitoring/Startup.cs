using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SiteMonitoring.Models;
using Microsoft.EntityFrameworkCore;
using SiteMonitoring.Services.Interfaces;
using SiteMonitoring.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SiteMonitoring
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
            services.AddDbContext<SiteMonitoringContext>(options => options
                        .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => //CookieAuthenticationOption
            {
                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Index");
            });

            // Services registration
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            services.AddHostedService<TimedHostedService>();
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDatabaseInitializer databaseInitializer)
        {
            app.UseDeveloperExceptionPage();
            app.UseBrowserLink();
            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            databaseInitializer.Seed();
        }
    }
}
