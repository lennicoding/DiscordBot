using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotProject.Services
{
    public class CommandHandler
    {
        public static IServiceProvider _provider;
        public static DiscordSocketClient _discord;
        public static CommandService _commands;
        public static IConfigurationRoot _config;

        public CommandHandler(DiscordSocketClient discord, CommandService commands, IConfigurationRoot config, IServiceProvider provider)
        {
            _provider = provider;
            _discord = discord;
            _commands = commands;
            _config = config;

            _discord.Ready += OnReady;
            _discord.MessageReceived += OnMessageReceived;
        }
        private async Task OnMessageReceived(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;
            var text = arg.Content; //Other tutorial

            
            var context = new SocketCommandContext(_discord, msg);
            

            int pos = 0;
            if (msg.HasStringPrefix(_config["prefix"], ref pos) || msg.HasMentionPrefix(_discord.CurrentUser, ref pos))
            {
                var result = await _commands.ExecuteAsync(context, pos, _provider);

                if (!result.IsSuccess)
                {
                    var reason = result.Error;

                    await context.Channel.SendMessageAsync($"The following error occured:\n{reason}\nTry to type !help for viewing the help window");
                    Console.WriteLine(reason);
                }
            }

            if (arg.Channel is SocketDMChannel)
            {
                string path = @"D:\Coding\Visual Studio\Projects\TripleTreeCodingHelper\TripleTreeCodingHelper\dmMessages.txt";
                string logMessage = arg.Author.Username + " " + DateTime.UtcNow.ToString() + " sent>" + arg.Content;

                Console.WriteLine(logMessage);
                string oldText = File.ReadAllText(path);

                File.WriteAllText(path, oldText + "\n" + logMessage);

                await arg.Channel.SendMessageAsync("I'm the Helper, but you can't chat with me :(");
            }



            //Other
            if (text == "Hallo")
            {
                await arg.Channel.SendMessageAsync("Hallo");
            }

        }

        private Task OnReady()
        {
            Console.WriteLine($"Connected as {_discord.CurrentUser.Username}#{_discord.CurrentUser.Discriminator}");
            return Task.CompletedTask;
        }

    }
}
