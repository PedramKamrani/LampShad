using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHost.Hubs
{
    public class ViewHub :Hub
    {
        public static int ViewCount { get; set; } = 0;
        public async Task Nottifywatching()
        {
            ViewCount++;

            await Clients.All.SendAsync("ViewCountUpdate", ViewCount);
        }
    }
}
