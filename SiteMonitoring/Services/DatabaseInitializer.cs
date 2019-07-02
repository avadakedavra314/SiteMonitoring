using Microsoft.Extensions.Configuration;
using SiteMonitoring.Services.Interfaces;
using System;
using System.Linq;
using SiteMonitoring.Extensions;
using Microsoft.Extensions.DependencyInjection;
using SiteMonitoring.Models;

namespace SiteMonitoring.Services
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly IServiceProvider _provider;
        public IConfiguration _configuration { get; }
        public DatabaseInitializer(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _provider = serviceProvider;
            _configuration = configuration;
        }

        public void Seed()
        {
            var required = _configuration.GetSection("Settings")["DatabaseSeedRequired"].ToBoolean();
            if (required)
            {
                using (IServiceScope scope = _provider.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<SiteMonitoringContext>();

                    User admin = _context.Users.FirstOrDefault
                        (u => u.Login == _configuration.GetSection("DatabaseSeedData")["AdministratorLogin"]);
                    if (admin == null)
                    {
                        _context.Users.Add(new User
                        {
                            Login = _configuration.GetSection("DatabaseSeedData")["AdministratorLogin"],
                            Password = _configuration.GetSection("DatabaseSeedData")["AdministratorPassword"],
                        });
                        _context.SaveChanges();
                    }
                    Site site = _context.Sites.FirstOrDefault
                       (u => u.Url == _configuration.GetSection("DatabaseSeedData")["DefaultSite"]);
                    if (site == null)
                    {
                        _context.Sites.Add(new Site
                        {
                            Url = _configuration.GetSection("DatabaseSeedData")["DefaultSite"],//DefaultTimeSpan
                            IsAvailable = BoolConvertExtension.ToBoolean(_configuration.GetSection("DatabaseSeedData")["DefaultSiteIsAvailable"]),
                        });
                        _context.SaveChanges();
                    }
                    Models.TimeSpan time = _context.TimeSpan.FirstOrDefault();
                    if (time == null)
                    {
                        _context.TimeSpan.Add(new Models.TimeSpan
                        {
                            Minutes = Convert.ToInt32(_configuration.GetSection("DatabaseSeedData")["DefaultTimeSpan"]),
                        });
                        _context.SaveChanges();
                    }

                }
            }
        }
    }
}
