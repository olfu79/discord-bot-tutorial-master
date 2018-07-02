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
using DiscordTutorialBot.Services;
using static DiscordTutorialBot.Services.Daily;

using NReco.ImageGenerator;

using Newtonsoft.Json;

namespace DiscordTutorialBot.Modules
{
    public class Money : ModuleBase<SocketCommandContext>
    {
        [Command("sklep")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Shop()
        {
            var coin = Emote.Parse("<:coin:462351821910835200>");
            var vip = Emote.Parse("<:supervip:462351820501549066>");
            var svip = Emote.Parse("<:ultravip:462351820308873246>");
            var sponsor = Emote.Parse("<:sponsor:462351820006883340>");

            var one = new Emoji("\u0031\u20e3");
            var two = new Emoji("\u0032\u20e3");
            var three = new Emoji("\u0033\u20e3");
            var four = new Emoji("\u0034\u20e3");
            var five = new Emoji("\u0035\u20e3");
            var six = new Emoji("\u0036\u20e3");
            var seven = new Emoji("\u0037\u20e3");
            var eight = new Emoji("\u0038\u20e3");
            var nine = new Emoji("\u0039\u20e3");

            EmbedBuilder ebShop = new EmbedBuilder();
            ebShop.WithTitle("**SKLEP GGWP**");
            ebShop.WithDescription("czynne 24/7 nawet w niedziele nie handlowe ;)");
            ebShop.AddField($":one: RANGA VIP {vip}", $"KOSZT: 5000 {coin}", true);
            ebShop.AddField($":two: RANGA SVIP {svip}", $"KOSZT: 10000 {coin}", true);
            ebShop.AddField($":three: WYBÓR MUZYKI :musical_note:", $"KOSZT: 3000 {coin}", true);
            ebShop.AddField($":four: AKINATOR 👳", $"KOSZT: 3000 {coin}", true);
            ebShop.AddField($":five: ZMIANA NICKU :label:", $"KOSZT: 750 {coin}", true);
            ebShop.AddField($":six: WŁASNY KANAŁ 🎙️", $"KOSZT: 10000 {coin}", true);
            ebShop.AddField($":seven: MYSTERY BOX 1 🎀", $"KOSZT: 1000 {coin}", true);
            ebShop.AddField($":eight: MYSTERY BOX 2 🎁", $"KOSZT: 2000 {coin}", true);
            ebShop.AddField($":nine: MYSTERY BOX 3 🔮", $"KOSZT: 3000 {coin}", true);
            ebShop.WithColor(Color.Blue);
            RestUserMessage msg = await Context.Channel.SendMessageAsync("", false, ebShop.Build());
            Global.MessageIdToTrack = msg.Id;

            await msg.AddReactionAsync(one);
            await msg.AddReactionAsync(two);
            await msg.AddReactionAsync(three);
            await msg.AddReactionAsync(four);
            await msg.AddReactionAsync(five);
            await msg.AddReactionAsync(six);
            await msg.AddReactionAsync(seven);
            await msg.AddReactionAsync(eight);
            await msg.AddReactionAsync(nine);
        }
        [Command("addMoney")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task AddMoney(uint money, IUser user = null)
        {
            var coin = Emote.Parse("<:coin:462351821910835200>");
            var err = Emote.Parse("<:WrongMark:460770239286870017>");

            if (user == null)
            {
                var account = UserAccounts.GetAccount(Context.User);
                account.Money += money;
                UserAccounts.SaveAccounts();
                await Context.Channel.SendMessageAsync($"Dodano {money} :coin:.");
            }
            if (user.IsBot)
            {
                await Context.Channel.SendMessageAsync($"{err} Nie możesz sprawdzic pieniędzy bota.");
                return;
            }
            var accountTarget = UserAccounts.GetAccount(user);
            ulong moneybalanceTarget = accountTarget.Money;
            moneybalanceTarget += money;
            UserAccounts.SaveAccounts();
            await Context.Channel.SendMessageAsync($"Dodano {money} {coin} graczowi {user.Username}.");
        }

        [Command("money")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task HowMuchMoney(IUser user = null)
        {
            var coin = Emote.Parse("<:coin:462351821910835200>");
            var err = Emote.Parse("<:WrongMark:460770239286870017>");

            if (user == null)
            {
                var account = UserAccounts.GetAccount(Context.User);
                ulong moneybalance = account.Money;
                UserAccounts.SaveAccounts();
                await Context.Channel.SendMessageAsync($"Posiadasz: {moneybalance} {coin}.");
                return;
            }

            if (user.IsBot)
            {
                await Context.Channel.SendMessageAsync($"{err} Nie możesz sprawdzic stanu bota.");
                return;
            }

            var accountTarget = UserAccounts.GetAccount(user);
            ulong moneybalanceTarget = accountTarget.Money;
            UserAccounts.SaveAccounts();
            await Context.Channel.SendMessageAsync($"Gracz {user.Username} posiada {moneybalanceTarget} {coin}.");
        }

        [Command("give")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task GiveMoney(IGuildUser user, ulong moneytogive)
        {
            var coin = Emote.Parse("<:coin:462351821910835200>");
            var err = Emote.Parse("<:WrongMark:460770239286870017>");

            if (user == Context.User)
            {
                await Context.Channel.SendMessageAsync($"{err} Nie możesz dać pieniędzy samemu sobie.");
                return;
            }
            if(user.IsBot)
            {
                await Context.Channel.SendMessageAsync($"{err} Nie możesz dać pieniędzy botowi.");
                return;
            }
            var accountGiver = UserAccounts.GetAccount(Context.User);
            var accountGeter = UserAccounts.GetAccount(user);
            ulong moneybalanceGiver = accountGiver.Money;
            ulong moneybalanceGeter = accountGeter.Money;
            
            if(moneybalanceGiver < moneytogive)
            {
                return;
            }
            else
            {
                accountGeter.Money -= moneytogive;
                accountGeter.Money += moneytogive;
            }

            UserAccounts.SaveAccounts();

            await Context.Channel.SendMessageAsync($"");
        }
        [Command("Daily"), Remarks("Dzienna dawka pieniędzy.")]
        [Alias("Darmowe", "Free")]
        public async Task GetDaily()
        {
            var coin = Emote.Parse("<:coin:462351821910835200>");
            var err = Emote.Parse("<:WrongMark:460770239286870017>");

            var result = Daily.GetDaily(Context.User);

            if (result.Success)
            {
                await Context.Channel.SendMessageAsync($"Proszę {Context.User.Mention}. Oto **250** {coin}, specjalnie dla ciebie...");
            }
            else
            {
                var timeSpanString = string.Format("{0:%h} godzin {0:%m} minut {0:%s} sekund", result.RefreshTimeSpan);
                await Context.Channel.SendMessageAsync($"{err} Hej! {Context.User.Mention}! Już odebrałeś swoją dzienną dawkę pieniędzy, .\nSpróbuj ponownie za: {timeSpanString}.");
            }
        }


    }
}
