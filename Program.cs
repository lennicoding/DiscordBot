using System;
using System.Threading.Tasks;

namespace DiscordBotProject
{
    class Program
    {
        public static async Task Main(string[] args)
            => await Startup.RunAsync(args);
    }
}