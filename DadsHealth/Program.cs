using DadsHealth.Services;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DadsHealth
{
    class Program
    {

        public static void Main(string[] args)
            => new Program()
            .MainAsync()
            .GetAwaiter()
            .GetResult();

        public async Task MainAsync()
        {

            using (var services = ConfigureServices())
            {
                var _client = services.GetRequiredService<DiscordSocketClient>();

                _client.Log += LogAsync;
                services.GetRequiredService<CommandService>().Log += LogAsync;

                await _client.LoginAsync(TokenType.Bot,
                    Config.Token);
                await _client.StartAsync();

                await services.GetRequiredService<CommandHandlingService>().InitAsync();

                await _client.SetGameAsync("I'm being paternity tested...");

                // Block this task until the program is closed.
                await Task.Delay(-1);
            }
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlingService>()
                .AddSingleton<HttpClient>()
                .AddSingleton<HeartBeatService>()
                .BuildServiceProvider();
        }

        private Task LogAsync(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

    }
}
