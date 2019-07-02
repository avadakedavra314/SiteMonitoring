using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace SiteMonitoring.Models
{
    public class SiteMonitoringContext : DbContext
    {
        public DbSet<Site> Sites { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TimeSpan> TimeSpan { get; set; }

        public SiteMonitoringContext(DbContextOptions<SiteMonitoringContext> options)
            : base(options)
        { }
    }
}
