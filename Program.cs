﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.RegularExpressions;

namespace valhallappweb
{
    public class Program
    {

        /*APP INIT */
        static void Main(string[] args)
        {
            Console.WriteLine("Valhalla application start");
            Task.Run(() => new Program().RunBotAsync().GetAwaiter().GetResult());
            CreateWebHostBuilder(args).Build().Run();
        }

        /*WEB APP INIT*/
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
            {
            Console.WriteLine("Valhalla webpage start");
                var port = Environment.GetEnvironmentVariable("PORT");

                return WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .UseUrls($"http://*:{port}");
            }

        /*DISCORD BOT INIT*/
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public async Task RunBotAsync()
        {
            Console.WriteLine("Valhalla bot start");
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            // Logging
            _client.Log += Client_Log;

            // Command init
            await RegisterCommandsAsync();

            // Bot Authentification init
            var token = Environment.GetEnvironmentVariable("TOKEN");
            await _client.LoginAsync(TokenType.Bot, token);

            // Start Discord bot
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        // Logging
        private Task Client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        // Command init
        public async Task RegisterCommandsAsync()
        {
            ReactionHandler reactionHandler = new ReactionHandler(_client);
            MessageEditedHandler editedHandler = new MessageEditedHandler(_client);
            MessageDeleteHandler deleteHandler = new MessageDeleteHandler(_client);
            MessageHandler messageHandler = new MessageHandler(_client, _commands,_services);
            _client.ReactionAdded += reactionHandler.HandleReactionAsync;
            _client.ReactionRemoved += reactionHandler.HandleReactionAsync;
            _client.ReactionsCleared += reactionHandler.HandleReactionClearAsync;
            _client.MessageReceived += messageHandler.HandleCommandAsync;
            _client.MessageDeleted += deleteHandler.HandleDeleteAsync;
            _client.MessageUpdated += editedHandler.HandleEditAsync;
            await _client.SetGameAsync("");
            await _client.SetGameAsync("Valhalla", "https://cdn.discordapp.com/icons/482631363233710106/30ba4ea763063d386248ac9493838776.jpg", ActivityType.CustomStatus);
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }
    }
}