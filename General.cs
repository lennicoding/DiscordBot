using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotProject.Modules
{
    public class General : ModuleBase
    {
        [Command("ping")]
        public async Task Ping()
        {
            await Context.Channel.SendMessageAsync("Pong!");
        }

        [Command("info")]
        public async Task Info()
        {
            var builder = new EmbedBuilder()
                .WithThumbnailUrl(Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl())
                .WithDescription("Some information about yourself:")
                .WithDescription("Your user name: " + Context.User.Username)
                .AddField("User ID", Context.User.Id, true)
                .AddField("Discriminator", Context.User.Discriminator, true)
                .AddField("Created at", Context.User.CreatedAt.ToString("dd/MM/yyyy"), true)
                .AddField("Joined at", (Context.User as SocketGuildUser).JoinedAt.Value.ToString("dd/MM/yyyy"), true)
                .AddField("Roles: ", string.Join(" ", (Context.User as SocketGuildUser).Roles.Select(x => x.Mention)))
                .WithColor(new Color(0, 166, 61))
                .WithCurrentTimestamp();
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
        }

        [Command("server")]
        public async Task Server()
        {
            var builder = new EmbedBuilder()
                .WithThumbnailUrl(Context.Guild.IconUrl)
                .WithDescription("In this message you can find some info about the server")
                .WithTitle($"{Context.Guild.Name} Information")
                .AddField("Created at", Context.Guild.CreatedAt.ToString("dd/MM/yyyy"))
                .WithColor(new Color(0, 166, 61));
            //.AddField("Membercount", (Context.Guild as SocketGuild).MemberCount + " members", true)
            //.AddField("online users: ", (Context as SocketGuild).Users.Where(x => x.Status == UserStatus.Online).Count() + " members", true);
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
        }

        [Command("help")]
        public async Task Help()
        {
            var builder = new EmbedBuilder()
                .WithDescription("Here is the Help window :D")
                .WithDescription("Your user name: " + Context.User.Username)
                .WithDescription("Using !info see info about yourself")
                .WithDescription("With add, sub, durch or mal can you calculate like add 1 2")
                .WithDescription("ONLY FOR ADMINS: Using purge count for deleting messages")
                .WithColor(new Color(0, 166, 61))
                .WithCurrentTimestamp();
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
        }

        [Command("add")]
        public async Task Add(int numberOne, int numberTwo)
        {
            await Context.Channel.SendMessageAsync("Your result: " + (numberOne + numberTwo).ToString()).ConfigureAwait(false);
        }
        [Command("sub")]
        public async Task Subtract(int numberOne, int numberTwo)
        {
            await Context.Channel.SendMessageAsync("Your result: " + (numberOne - numberTwo).ToString()).ConfigureAwait(false);
        }
        [Command("mal")]
        public async Task Mal(int numberOne, int numberTwo)
        {
            await Context.Channel.SendMessageAsync("Your result: " + (numberOne * numberTwo).ToString()).ConfigureAwait(false);
        }
        [Command("durch")]
        public async Task Durch(int numberOne, int numberTwo)
        {
            await Context.Channel.SendMessageAsync("Your result: " + (numberOne / numberTwo).ToString()).ConfigureAwait(false);
        }


        [Command("userinfo")]
        public async Task UserInfo(SocketGuildUser user = null)
        {
            var builder = new EmbedBuilder()
                .WithThumbnailUrl(user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl())
                .WithDescription($"Some information about {user.Username}:")
                .WithDescription("Your user name: " + user.Username)
                .AddField("User ID", user.Id, true)
                .AddField("Discriminator", user.Discriminator, true)
                .AddField("Created at", user.CreatedAt.ToString("dd/MM/yyyy"), true)
                .AddField("Joined at", user.JoinedAt.Value.ToString("dd/MM/yyyy"), true)
                .AddField("Roles: ", string.Join(" ", user.Roles.Select(x => x.Mention)))
                .WithColor(new Color(0, 166, 61))
                .WithCurrentTimestamp();
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
        }

    }
}
