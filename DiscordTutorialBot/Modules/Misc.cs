using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Rest;

using DiscordTutorialBot.Core.UserAccounts;

using NReco.ImageGenerator;
using Newtonsoft.Json;


namespace DiscordTutorialBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        string time = DateTime.Now.ToString("dd.MM.yyyy");

        [Command("warn")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task WarnUser(IGuildUser user, [Remainder] string reason = "Brak.")
        {
            await Context.Message.DeleteAsync();
            var userAccount = UserAccounts.GetAccount((SocketUser)user);
            userAccount.NumberOfWarnings++;
            UserAccounts.SaveAccounts();

            if (userAccount.NumberOfWarnings >= 6)
            {
                await user.Guild.AddBanAsync(user, 5);

                EmbedBuilder eb6 = new EmbedBuilder();
                eb6.WithTitle($"Ostrzeżenie, Banicja.");
                eb6.WithDescription("");
                eb6.AddField("Karany:", $"{user}");
                eb6.AddField("Powód:", $"{reason}");
                eb6.AddField("Przez:", $"{Context.User.Username}");
                eb6.AddField("Liczba ostrzeżeń:", "6 na 5 - ban");
                eb6.WithColor(Color.Gold);
                eb6.WithFooter($"Ostrzeżenie, Banicja • {time}");
                await ReplyAsync("", false, eb6.Build());
            }
            else if (userAccount.NumberOfWarnings == 5)
            {
                var guildUser = (SocketGuildUser)Context.User;
                await guildUser.KickAsync();

                EmbedBuilder eb5 = new EmbedBuilder();
                eb5.WithTitle($"Ostrzeżenie, Wyrzucenie z serwera.");
                eb5.WithDescription("");
                eb5.AddField("Karany:", $"{user}");
                eb5.AddField("Powód:", $"{reason}");
                eb5.AddField("Przez:", $"{Context.User.Username}");
                eb5.AddField("Liczba ostrzeżeń:", "5 na 5");
                eb5.WithColor(Color.Gold);
                eb5.WithFooter($"Ostrzeżenie, Wyrzucenie • {time}");
                await ReplyAsync("", false, eb5.Build());
            }
            else if (userAccount.NumberOfWarnings == 4)
            {
                EmbedBuilder eb4 = new EmbedBuilder();
                eb4.WithTitle($"Ostrzeżenie.");
                eb4.WithDescription("");
                eb4.AddField("Karany:", $"{user}");
                eb4.AddField("Powód:", $"{reason}");
                eb4.AddField("Przez:", $"{Context.User.Username}");
                eb4.AddField("Liczba ostrzeżeń:", "4 na 5");
                eb4.WithColor(Color.Gold);
                eb4.WithFooter($"Ostrzeżenie • {time}");
                await ReplyAsync("", false, eb4.Build());
            }
            else if (userAccount.NumberOfWarnings == 3)
            {
                EmbedBuilder eb3 = new EmbedBuilder();
                eb3.WithTitle($"Ostrzeżenie.");
                eb3.WithDescription("");
                eb3.AddField("Karany:", $"{user}");
                eb3.AddField("Powód:", $"{reason}");
                eb3.AddField("Przez:", $"{Context.User.Username}");
                eb3.AddField("Liczba ostrzeżeń:", "3 na 5");
                eb3.WithColor(Color.Gold);
                eb3.WithFooter($"Ostrzeżenie • {time}");
                await ReplyAsync("", false, eb3.Build());
            }
            else if (userAccount.NumberOfWarnings == 2)
            {
                EmbedBuilder eb2 = new EmbedBuilder();
                eb2.WithTitle($"Ostrzeżenie.");
                eb2.WithDescription("");
                eb2.AddField("Karany:", $"{user}");
                eb2.AddField("Powód:", $"{reason}");
                eb2.AddField("Przez:", $"{Context.User.Username}");
                eb2.AddField("Liczba ostrzeżeń:", "2 na 5");
                eb2.WithColor(Color.Gold);
                eb2.WithFooter($"Ostrzeżenie • {time}");
                await ReplyAsync("", false, eb2.Build());
            }
            else if (userAccount.NumberOfWarnings == 1)
            {
                EmbedBuilder eb = new EmbedBuilder();
                eb.WithTitle($"Ostrzeżenie.");
                eb.WithDescription("");
                eb.AddField("Karany:", $"{user}");
                eb.AddField("Powód:", $"{reason}");
                eb.AddField("Przez:", $"{Context.User.Username}");
                eb.AddField("Liczba ostrzeżeń:", "1 na 5");
                eb.WithColor(Color.Gold);
                eb.WithFooter($"Ostrzeżenie • {time}");
                await ReplyAsync("", false, eb.Build());
            }
        }

        [Command("kick")]
        [Remarks("!kick [user] [reason]")]
        [Summary("Wyrzuca użytkownika z serwera.")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task KickUser(IGuildUser user, [Remainder] string reason = "Brak.")
        {
            ulong getIdk = Context.Channel.Id;
            ulong chanIdk = 452902079917326337;

            if (getIdk == chanIdk)
            {
                await Context.Message.DeleteAsync();
                await user.KickAsync(reason);

                EmbedBuilder ebk = new EmbedBuilder();
                ebk.WithTitle($"Wyrzucenie");
                ebk.WithDescription("");
                ebk.AddField("Karany:", $"{user}");
                ebk.AddField("Powód:", $"{reason}");
                ebk.AddField("Przez:", $"{Context.User.Username}");
                ebk.WithColor(Color.Orange);
                ebk.WithFooter($"Wyrzucenie • {time}");
                await ReplyAsync("", false, ebk.Build());
            }
        }

        [Command("ban")]
        [Remarks("!ban [user] [reason]")]
        [Summary("Banuje użytkownika z serwera.")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task BanUser(IGuildUser user, [Remainder] string reason = "Brak.")
        {
            ulong getIdb = Context.Channel.Id;
            ulong chanIdb = 452902079917326337;

            if (getIdb == chanIdb)
            {
                await Context.Message.DeleteAsync();
                await user.Guild.AddBanAsync(user, 5, reason);

                EmbedBuilder ebb = new EmbedBuilder();
                ebb.WithTitle($"Ban");
                ebb.WithDescription("");
                ebb.AddField("Karany:", $"{user}");
                ebb.AddField("Powód:", $"{reason}");
                ebb.AddField("Przez:", $"{Context.User.Username}");
                ebb.WithColor(Color.DarkRed);
                ebb.WithFooter($"Banicja • {time}");
                await ReplyAsync("", false, ebb.Build());
            }
        }

        [Command("addrole")]
        [Alias("dodajrole")]
        [Remarks("!dodajrole [user] [role]")]
        [Summary("Dodaje role danemu użytkownikowi.")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task AddRoleUser(SocketGuildUser socketGuildUser, IRole iRole)
        {
            ulong getIdad = Context.Channel.Id;
            ulong chanIdad = 452902079917326337;

            if (getIdad == chanIdad)
            {
                await Context.Message.DeleteAsync();
                await socketGuildUser.AddRoleAsync(iRole);

                EmbedBuilder ebad = new EmbedBuilder();
                ebad.WithTitle($"Użytkownik *{socketGuildUser}* otrzymał nową rangę!");
                ebad.WithDescription("Gratulacje");
                ebad.AddField("Ranga:", $"{iRole}");
                ebad.AddField("Nadana przez", $"{Context.User.Username}");
                ebad.WithColor(Color.DarkGreen);
                await ReplyAsync("", false, ebad.Build());
            }
        }

        [Command("removerole")]
        [Alias("usunrole", "delrole")]
        [Remarks("!usunrole [user] [role]")]
        [Summary("Usuwa role danemu użytkownikowi.")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task RemoveRoleUser(SocketGuildUser socketGuildUser, IRole iRole)
        {
            ulong getIdrr = Context.Channel.Id;
            ulong chanIdrr = 452902079917326337;

            if (getIdrr == chanIdrr)
            {
                await Context.Message.DeleteAsync();
                await socketGuildUser.RemoveRoleAsync(iRole);

                EmbedBuilder ebrr = new EmbedBuilder();
                ebrr.WithTitle($"Użytkownik *{socketGuildUser}* stracił rangę!");
                ebrr.WithDescription("");
                ebrr.AddField("Ranga:", $"{iRole}");
                ebrr.AddField("Usunięta przez", $"{Context.User.Username}");
                ebrr.WithColor(Color.Red);
                await ReplyAsync("", false, ebrr.Build());
            }
        }

        [Command("mute")]
        [Remarks("!mute [user]")]
        [Summary("Wycisza użytkownika.")]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        public async Task MuteUser(SocketUser socketUser, int timeInSeconds = 0, [Remainder] string reason = "Brak.")
        {
            ulong getIdm = Context.Channel.Id;
            ulong chanIdm = 452902079917326337;
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Wyciszony");

            if (getIdm == chanIdm)
            {
                await Context.Message.DeleteAsync();
                if (socketUser == Context.Guild.GetUser(282506757694029824))
                {
                    await ReplyAsync("Nie możesz zmutować bota.");
                    return;
                }
                var account = UserAccounts.GetAccount(socketUser);
                account.UnmuteTime = DateTime.Now.Add(TimeSpan.FromSeconds(timeInSeconds));
                UserAccounts.SaveAccounts();

                EmbedBuilder ebm = new EmbedBuilder();
                ebm.WithTitle($"Wyciszenie");
                ebm.WithDescription("");
                ebm.AddField("Karany:", $"{socketUser}");
                ebm.AddField("Czas:", $"{timeInSeconds}" + " sekund");
                ebm.AddField("Przez:", $"{Context.User.Username}");
                ebm.AddField("Powód:", $"{reason}");

                ebm.WithColor(Color.DarkerGrey);
                await ReplyAsync("", false, ebm.Build());
            }
        }

        [Command("react")]
        public async Task HandleReactionMessage()
        {
            RestUserMessage msg = await Context.Channel.SendMessageAsync("React to me!");
            Global.MessageIdToTrack = msg.Id;
            var cs = Emote.Parse("<:CSGO2:460770281020063746>");
            var lol = Emote.Parse("<:LeageOfLegends:460770233326501898>");
            var pubg = Emote.Parse("<:pubg:460770295620304916>");
            var fortn = Emote.Parse("<:fortnite:460770233574227969>");
            var ov = Emote.Parse("<:overwatch:460770233297141771>");
            var roblx = Emote.Parse("<:roblox:460770244605116426>");
            var gta = Emote.Parse("<:gta:461586476182798336>");
            var mc = Emote.Parse("<:minecraft:461586466263531520>");
            var sims = Emote.Parse("<:Sims:461586518214180864>");
            var rocklg = Emote.Parse("<:Rocketleague:461586464787136512>");
            var unturned = Emote.Parse("<:Unturned:461586456419368970>");
            await msg.AddReactionAsync(cs);
            await msg.AddReactionAsync(lol);
            await msg.AddReactionAsync(fortn);
            await msg.AddReactionAsync(roblx);
            await msg.AddReactionAsync(pubg);
            await msg.AddReactionAsync(ov);
            await msg.AddReactionAsync(gta);
            await msg.AddReactionAsync(mc);
            await msg.AddReactionAsync(sims);
            await msg.AddReactionAsync(rocklg);
            await msg.AddReactionAsync(unturned);
        }

        [Command("dodajXP")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task AddXP(uint xp)
        {
            var account = UserAccounts.GetAccount(Context.User);
            account.XP += xp;
            UserAccounts.SaveAccounts();
            await Context.Channel.SendMessageAsync($"You gained {xp} XP.");
        }

        [Command("botstatus")]
        [Alias("ustawstatus")]
        [Remarks("!botstatus [status]")]
        [Summary("Pozwala na zmianę statusu bota na dowolny")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task SetBotStatus([Remainder] string botStatus = "")
        {
            await Context.Message.DeleteAsync();

            if(botStatus == "")
            {
                Context.Client.SetGameAsync("GGWP BOT");
                return;
            }

            Context.Client.SetGameAsync(botStatus);
            
            await Task.Delay(5000);
        }

        [Command("secret")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task RevealSecret([Remainder]string arg = "")
        {
            if (!UserIsSecretOwner((SocketGuildUser)Context.User))
            {
                await Context.Channel.SendMessageAsync(":x: You need the SecretOwner role to do that. " + Context.User.Mention);
                return;
            }
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync(Utilities.GetAlert("SECRET"));
        }
        
        private bool UserIsSecretOwner(SocketGuildUser user)
        {
            string targetRoleName = "SecretOwner";
            var result = from r in user.Guild.Roles
                         where r.Name == targetRoleName
                         select r.Id;
            ulong roleID = result.FirstOrDefault();
            if (roleID == 0) return false;
            var targetRole = user.Guild.GetRole(roleID);
            return user.Roles.Contains(targetRole);
        }
        
        [Command("data")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task GetData()
        {
            await Context.Channel.SendMessageAsync("Data Has " + DataStorage.GetPairsCount() + " pairs.");
        }

        [Command("ogloszenie")]
        [Alias("ogl")]
        [Remarks("!ogl [msg]")]
        [Summary("Wysyła ogłoszenie.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Ogl([Remainder] string usr_msg = "")
        {
            ulong getId = Context.Channel.Id;
            ulong chanId = 452902132782465035;
            string time = DateTime.Now.ToString("dd.MM.yyyy");

            if (getId == chanId)
            {
                await Context.Message.DeleteAsync();

                await ReplyAsync("@everyone " + time + "\n" + usr_msg);
                await Task.Delay(0);
            }
        }
        [Command("purge")]
        [Alias("czysc", "usun", "clear")]
        [Remarks("!clear [num]")]
        [Summary("Pozwala na wyczyszczenie danej ilości wiadomości.")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task ClearMessages([Remainder] int num = 0)
        {
            await Context.Message.DeleteAsync();

            await ReplyAsync("***🗑️ Usuwam " + $"{num}" + " wiadomości... Proszę czekać.***");

            var messages = await this.Context.Channel.GetMessagesAsync((int)num + 1).FlattenAsync();

            var channel = Context.Message.Channel as SocketTextChannel;
            await channel.DeleteMessagesAsync(messages);

            const int delay = 5000;
            await Task.Delay(delay);
        }
    }
}
