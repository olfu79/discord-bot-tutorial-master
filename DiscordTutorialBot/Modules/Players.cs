using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Rest;

using NReco.ImageGenerator;

using DiscordTutorialBot.Core.UserAccounts;

namespace DiscordTutorialBot.Modules
{
    public class Players : ModuleBase<SocketCommandContext>
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

                //var ch = await Context.Guild.GetTextChannelAsync(448884032391086092);
                IMessageChannel ch = Context.Guild.GetChannel(Convert.ToUInt64(448884032391086092)) as IMessageChannel;
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
        [Command("jaki lvl to")]
        public async Task WhatLevelIs(uint xp)
        {
            await Context.Message.DeleteAsync();
            uint level = (uint)Math.Sqrt(xp / 50);

            EmbedBuilder eb = new EmbedBuilder();
            eb.WithTitle($"Jaki lvl to {xp}xp?");
            eb.WithDescription($"Jest to: {level}lvl");
            eb.WithColor(Color.Blue);
            await ReplyAsync("", false, eb.Build());
        }

        [Command("statystyka")]
        [Alias("staty", "profil", "ja")]
        public async Task MyStats([Remainder]string arg = "")
        {
            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;

            var account = UserAccounts.GetAccount(target);
            string avatar = Context.User.GetAvatarUrl();
            string userlvl = account.LevelNumber.ToString();
            string userexp = account.XP.ToString();
            string username = Context.User.Username;
            string money = account.Money.ToString();
            string NoWarns = account.NumberOfWarnings.ToString();

            string css = "<style>\n" +
            "body\n" +
            "{\n" +
            "background-color: #262626;\n" +
            "color: white;\n" +
            "}\n" +
            "img\n" +
            "{\n" +
            "float: left;\n" +
            "margin-right: 20px;\n" +
            "}\n" +
            ".bag\n" +
            "{\n" +
            "height: 100%;\n" +
            "background-color: #383838;\n" +
            "}\n" +
            ".name\n" +
            "{\n" +
            "font-size: 21px;\n" +
            "font-family: Tahoma, Geneva, sans-serif;\n" +
            "}\n" +
            ".desc\n" +
            "{\n" +
            "font-family: Tahoma, Geneva, sans-serif;\n" +
            "font-size: 20px;\n" +
            "}\n" +
            "</style>\n";

            string html = String.Format("<head>\n   <meta charset=\"utf-8\" />\n</head>\n<body><div class=\"bag\">\n<img src=\"" + avatar + "\"> \n <div class=\"name\">Nick: <b>" + username + "</b></div><div class= \"desc\">Poziom: <b>" + userlvl + " lvl</b></div><div class=\"desc\">EXP: <b>" + userexp + " xp</b></div><div class=\"desc\">Pieniądze: <b>" + money + " $</b></div><div class=\"desc\">Liczba Ostrzeżeń: <b>" + NoWarns + " ostrzeżeń</b></div></div>\n</body>");
            var converter = new HtmlToImageConverter
            {
                Width = 500,
                Height = 145
            };
            var jpgBytes = converter.GenerateImage(css + html, NReco.ImageGenerator.ImageFormat.Jpeg);
            await Context.Channel.SendFileAsync(new MemoryStream(jpgBytes), "level.jpg");
        }
        //help

        //pogoda

        //invie link

        //self role z grami
        //
    }
}
