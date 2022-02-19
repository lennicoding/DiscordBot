using Discord.Commands;
using Discord.WebSocket;
using DiscordBotProject.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotProject
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; } //To past our configuration

        public Startup(string[] args)
        {
            var builder = new ConfigurationBuilder()        //Builder build us our config
                .SetBasePath(AppContext.BaseDirectory)
                .AddYamlFile("_config.yml");
            Configuration = builder.Build();
        }
        public static async Task RunAsync(string[] args)
        {
            var startup = new Startup(args);
            await startup.RunAsync();
        }


        public async Task RunAsync()
        {

            var services = new ServiceCollection(); //Create SeviceCollection
            ConfigureServices(services);            //Function for the Service 

            var provider = services.BuildServiceProvider();
            provider.GetRequiredService<CommandHandler>();

            await provider.GetRequiredService<StartupService>().StartAsync(); //Login our bot
            await Task.Delay(-1);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = Discord.LogSeverity.Verbose, //When Sth goes wrong we get a message
                MessageCacheSize = 1000
            }))
                .AddSingleton(new CommandService(new CommandServiceConfig
                {
                    LogLevel = Discord.LogSeverity.Verbose,
                    DefaultRunMode = RunMode.Async,
                    CaseSensitiveCommands = false,
                }))
                .AddSingleton<CommandHandler>()
                .AddSingleton<StartupService>()
                .AddSingleton(Configuration);
        }
    }


}
