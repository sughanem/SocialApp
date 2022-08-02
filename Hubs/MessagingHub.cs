using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SocialAppService.MessagingServer
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MessagingHub : Hub
    {
        public async Task SendMessageAsync(string message)
        {
            var routeObj = JsonSerializer.Deserialize<dynamic>(message);
            string toClient = routeObj.To;
            if (string.IsNullOrEmpty(toClient))
            {
                await Clients.Client(toClient).SendAsync("ReceiveMessage", message);
            }
        }

    }
}