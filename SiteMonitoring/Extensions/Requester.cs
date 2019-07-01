using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SiteMonitoring.Extensions
{
    public static class Requester
    {
        public static async Task<bool> IsUrlAvailable(string url)
        {
            var client = new HttpClient();
            if(Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                var response = await client.GetAsync(url);
                return response.IsSuccessStatusCode;
            }
            return false;
        }
    }
}
