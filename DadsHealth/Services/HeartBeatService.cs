using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DadsHealth.Services
{
    public class HeartBeatService
    {
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _services;
        public HeartBeatService(DiscordSocketClient client, IServiceProvider services)
        {
            _client = services.GetRequiredService<DiscordSocketClient>();
            _services = services;
        }
    }
}
