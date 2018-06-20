using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using DiscordTutorialBot.Core;

namespace DiscordTutorialBot.Modules
{
    public class Players : ModuleBase
    {
        string time = DateTime.Now.ToString("dd.MM.yyyy");

        [Command("mezczyzna")]
        [Alias("m")]
        [Summary("Ustawia płeć.")]
        public async Task MezczyznaSet()
        {
            ulong getId = Context.Channel.Id;
            ulong chanId = 448884032391086092;

            if (getId == chanId)
            {
                await Context.Message.DeleteAsync();

                var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Mężczyzna");
                var usun = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Kobieta");

                var guildUser = (SocketGuildUser)Context.User;

                await guildUser.AddRoleAsync(role);
                await guildUser.RemoveRoleAsync(usun);
            }
        }

        [Command("kobieta")]
        [Alias("k")]
        [Summary("Ustawia płeć.")]
        public async Task KobietaSet()
        {
            ulong getId = Context.Channel.Id;
            ulong chanId = 448884032391086092;

            if (getId == chanId)
            {
                await Context.Message.DeleteAsync();

                var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Kobieta");
                var usun = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Mężczyzna");

                var guildUser = (SocketGuildUser)Context.User;

                await guildUser.AddRoleAsync(role);
                await guildUser.RemoveRoleAsync(usun);
            }
        }

        [Command("akceptuje")]
        [Alias("accept")]
        [Summary("Akceptuje regulamin.")]
        public async Task Akceptowanie()
        {
            ulong getId = Context.Channel.Id;
            ulong chanId = 448884032391086092;

            if (getId == chanId)
            {
                await Context.Message.DeleteAsync();

                var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Gracz");
                var guildUser = (SocketGuildUser)Context.User;
                await guildUser.AddRoleAsync(role);

                var ch = await Context.Guild.GetTextChannelAsync(448884032391086092);
                await ch.SendMessageAsync("Witaj " + Context.User.Mention + " na serwerze ◄◉►GGWP◄◉► ! Mamy nadzieje że będziesz się tu dobrze bawił/a :smile:");
            }
        }
        [Command("odrzucam")]
        [Alias("deny")]
        [Summary("Odrzuca regulamin.")]
        public async Task Odrzucanie()
        {
            ulong getId = Context.Channel.Id;
            ulong chanId = 448884032391086092;

            if (getId == chanId)
            {
                await Context.Message.DeleteAsync();

                var guildUser = (SocketGuildUser)Context.User;
                await guildUser.KickAsync();
            }
        }
        [Command("reklama")]
        [Alias("rek")]
        [Remarks("!rek [msg]")]
        [Summary("Wysyła reklama.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Rek([Remainder] string usr_msg = null)
        {
            ulong getId = Context.Channel.Id;
            ulong chanId = 449940006031851530;

            if (getId == chanId)
            {
                await Context.Message.DeleteAsync();

                EmbedBuilder ebr = new EmbedBuilder();
                ebr.WithTitle($"Reklama użytkownika {Context.User.Username}:");
                ebr.WithDescription($"{usr_msg}");
                ebr.WithFooter($"Reklama • {time}");
                ebr.WithColor(Color.Purple);
                await ReplyAsync("", false, ebr.Build());

                await Task.Delay(86400000);
            }
        }

        //help

        //pogoda

        //invie link

        //self role z grami
    }
}
