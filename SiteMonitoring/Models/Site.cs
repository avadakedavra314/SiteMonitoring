using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMonitoring.Models
{
    public class Site
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsAvailable { get; set; }
    }
}
