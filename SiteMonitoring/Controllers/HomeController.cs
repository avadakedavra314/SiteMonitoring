using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SiteMonitoring.Models;
using Microsoft.EntityFrameworkCore;

namespace SiteMonitoring.Controllers
{
    public class HomeController : Controller
    {
        private readonly SiteMonitoringContext _context;

        public HomeController(SiteMonitoringContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var sites = _context.Sites;
            return View(await sites.ToListAsync());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
