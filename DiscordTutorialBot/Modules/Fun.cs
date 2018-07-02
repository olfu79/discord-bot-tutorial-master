using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Rest;

using DiscordTutorialBot.Core;

namespace DiscordTutorialBot.Modules
{
    public class Fun : ModuleBase
    {
        [Command("8ball")]
        [Summary("Odpowiada na Twoje pytanie.")]
        public async Task Eightball([Remainder] string question = null)
        {
            ulong getId8 = Context.Channel.Id;
            ulong chanId8 = 448884032391086092;

            if (getId8 == chanId8)
            {
                await Context.Message.DeleteAsync();

                string[] ans = new string[] {
                        "Może..",
                        "Nie myśl o tym.",
                        "Bez wachania",
                        "Tak, zdecydowanie",
                        "Mam pewne wątpliwości",
                        "Wydaje mi się że tak",
                        "chyba",
                        "Nie",
                        "Tak",
                        "Jest to prawdopodobne",
                        "Nie wydaje mi się",
                        "Zapytaj ponownie później",
                        "Lepiej żebym nie mówił",
                        "Nie mogę tego przewidzieć",
                        "Ty no nie wiem :D",
                        "Nie licz na to",
                        "Moja odpowiedź brzmi: NIE",
                        "Moje źródła donoszą że nie",
                        "Raczej nie",
                        "Niemożliwe!"
                    };
                EmbedBuilder eb8 = new EmbedBuilder();
                eb8.WithTitle($"*{question}*");
                eb8.Description = $"**{ans[new Random().Next(ans.Length)]}**";
                eb8.WithColor(Color.Red);
                await ReplyAsync("", false, eb8.Build());
            }
        }
        [Command("kostka")]
        [Summary("Losuje liczbę od 1 do 6.")]
        public async Task Kosc()
        {
            ulong getIdko = Context.Channel.Id;
            ulong chanIdko = 448884032391086092;

            if (getIdko == chanIdko)
            {

                string[] ans = new string[] {
                        "1",
                        "2",
                        "3",
                        "4",
                        "5",
                        "6"
                    };
                EmbedBuilder ebk = new EmbedBuilder();
                ebk.WithTitle($"*Rzucam kością...*");
                ebk.Description = "**Wypadło: **" + $"{ans[new Random().Next(ans.Length)]}";
                ebk.WithColor(new Color(255, 255, 255));
                await ReplyAsync("", false, ebk.Build());
            }
        }
        [Command("moneta")]
        [Summary("Rzuca monetą.")]
        public async Task Moneta()
        {
            ulong getIdmo = Context.Channel.Id;
            ulong chanIdmo = 448884032391086092;

            if (getIdmo == chanIdmo)
            {
                string[] ans = new string[] {
                        "Orzeł",
                        "Reszka"
                    };
                EmbedBuilder ebm = new EmbedBuilder();
                ebm.WithTitle($"*Podrzucam monete!*");
                ebm.Description = "**Wypada: **" + $"{ans[new Random().Next(ans.Length)]}";
                ebm.WithColor(Color.Gold);
                await ReplyAsync("", false, ebm.Build());
            }
        }

        [Command("wybierz")]
        public async Task PickOne([Remainder]string message)
        {
            string[] options = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            Random r = new Random();
            string selection = options[r.Next(0, options.Length)];

            var embed = new EmbedBuilder();
            embed.WithTitle("Choice for " + Context.User.Username);
            embed.WithDescription(selection);
            embed.WithColor(new Color(255, 255, 0));
            embed.WithThumbnailUrl("https://orig00.deviantart.net/3033/f/2016/103/0/c/mercy_by_raichiyo33-d9yufl4.jpg");

            await Context.Channel.SendMessageAsync("", false, embed.Build());
            DataStorage.AddPairToStorage(Context.User.Username + DateTime.Now.ToLongDateString(), selection);
        }

        private static string[] jokes = new[] { "Co robi strażak na siłowni? \nSpala kalorie!", "Przez jakie ryby można skakać? \nPrzez płotki!", "Co robi zaatakowany kucharz? \nWzywa posiłki!", "Jaki jest ulubiony owoc żołnierza? \nGranat!", "Po co ubezpieczyciel przyszedł do lasu? \nPolisa!", "Co mówi ogórek gdy jest zdziwiony? \nSkisłem!", "Jak nazywa się żona pszczelarza? \nUla!", "Co mówi ksiądz po ślubie informatyka? \nPobieranie zakończone!", "Co robi grabarz? \nCzęstochowa!", "Dlaczego długopisy nie chodzą do szkoły? \nBo się wypisały!", "Co mówi ogrodnik do kumpla? \nPrzesadziłeś!", "Jak się nazywa lekarz który leczy pandy? \nPandoktor!", "Dlaczego choinka nie jest głodna? \nBo jodła!" };
        [Command("zart")]
        [Alias("suchar")]
        [Summary("Wysyła losowego suchara.")]
        public async Task Joke()
        {
            var joke = jokes[new Random().Next(jokes.Length)];

            ulong getIdjo = Context.Channel.Id;
            ulong chanIdjo = 449940006031851530;

            if (getIdjo == chanIdjo)
            {
                await Context.Channel.SendMessageAsync(joke);
            }
        }

        private static string[] _imagePaths = new[] { "Pics/1.jpg", "Pics/2.jpg", "Pics/3.png", "Pics/4.png", "Pics/5.jpg", "Pics/6.jpg", "Pics/7.jpg", "Pics/8.jpg", "Pics/9.jpg", "Pics/10.jpg", "Pics/11.jpg", "Pics/12.jpg", "Pics/13.jpg", "Pics/14.jpg", "Pics/15.jpg", "Pics/16.jpg", "Pics/17.jpg", "Pics/18.jpg", "Pics/19.jpg", "Pics/20.jpg", "Pics/21.jpg", "Pics/22.jpg", "Pics/23.jpg", "Pics/24.jpg", "Pics/25.jpg", "Pics/26.jpg", "Pics/27.jpeg" };
        [Command("meme")]
        [Alias("mem")]
        [Summary("Wysyła losowy obrazek.")]
        public async Task Meme()
        {
            var image = _imagePaths[new Random().Next(_imagePaths.Length)];

            ulong getIdme = Context.Channel.Id;
            ulong chanIdme = 449940006031851530;

            if (getIdme == chanIdme)
            {
                await Context.Channel.SendFileAsync(image);
            }
        }
        //rip
        private static string[] _imagePathsRip = new[] { "Fun/Rip/1.png", "Fun/Rip/2.jpg", "Fun/Rip/3.jpg", "Fun/Rip/4.png" };
        [Command("rip")]
        [Alias("śmierć", "dead")]
        [Summary("Wysyła losowy obrazek.")]
        public async Task Rip()
        {
            var image = _imagePathsRip[new Random().Next(_imagePathsRip.Length)];

            ulong getIdme = Context.Channel.Id;
            ulong chanIdme = 449940006031851530;

            if (getIdme == chanIdme)
            {
                await Context.Channel.SendFileAsync(image);
            }
        }

        //powitanie
        private static string[] _imagePathsPowitanie = new[] { "Fun/Powitania/1.jpg", "Fun/Powitania/2.jpg", "Fun/Powitania/3.jpg", "Fun/Powitania/4.jpg", "Fun/Powitania/5.jpg", "Fun/Powitania/6.jpg", "Fun/Powitania/7.jpg", "Fun/Powitania/8.jpg", "Fun/Powitania/9.jpg", "Fun/Powitania/10.jpg", "Fun/Powitania/11.png"};
        [Command("hej")]
        [Alias("siema", "witaj", "elo", "hi", "hello")]
        [Summary("Wysyła losowy obrazek.")]
        public async Task Hello(IGuildUser user)
        {
            var powitanie = _imagePathsPowitanie[new Random().Next(_imagePathsPowitanie.Length)];
            await Context.Channel.SendMessageAsync($"**{Context.User.Mention}** wita **{user.Mention}**");
            await Context.Channel.SendFileAsync(powitanie);
        }

        //uderzenie
        private static string[] _imagePathsUderzenie = new[] { "Fun/Punch/1.svg", "Fun/Punch/2.jpg", "Fun/Punch/3.jpg", "Fun/Punch/4.jpg", "Fun/Punch/5.jpg", "Fun/Punch/6.jpg", "Fun/Punch/7.jpg", "Fun/Punch/8.jpg", "Fun/Punch/9.jpg" };
        [Command("uderz")]
        [Alias("walnij", "bij", "hit", "punch")]
        [Summary("Wysyła losowy obrazek.")]
        public async Task Punch(IGuildUser user)
        {
            var punch = _imagePathsUderzenie[new Random().Next(_imagePathsUderzenie.Length)];
            await Context.Channel.SendMessageAsync($"**{Context.User.Mention}** uderza **{user.Mention}**");
            await Context.Channel.SendFileAsync(punch);
        }

        //dobranoc
        private static string[] _imagePathsDobranoc = new[] { "Fun/Dobranoc/1.jpg", "Fun/Dobranoc/2.jpg", "Fun/Dobranoc/3.jpg", "Fun/Dobranoc/4.jpg", "Fun/Dobranoc/5.jpg", "Fun/Dobranoc/6.jpg", "Fun/Dobranoc/7.jpg", "Fun/Dobranoc/8.jpg" };
        [Command("dobranoc")]
        [Alias("idespać", "śpij", "sen", "zzz")]
        [Summary("Wysyła losowy obrazek.")]
        public async Task Sleep()
        {
            var sleep = _imagePathsDobranoc[new Random().Next(_imagePathsDobranoc.Length)];
            await Context.Channel.SendMessageAsync($"**{Context.User.Mention}** idzie spać.");
            await Context.Channel.SendFileAsync(sleep);
        }

        //facepalm
        private static string[] _imagePathsFacep = new[] { "Fun/Facepalm/1.jpg", "Fun/Facepalm/2.jpg", "Fun/Facepalm/3.png", "Fun/Facepalm/4.jpg", "Fun/Facepalm/5.png", "Fun/Facepalm/6.gif", "Fun/Facepalm/7.jpg", "Fun/Facepalm/8.png", "Fun/Facepalm/9.png", "Fun/Facepalm/10.jpg", "Fun/Facepalm/11.jpg" };
        [Command("facepalm")]
        [Summary("Wysyła losowy obrazek.")]
        public async Task Facepalm()
        {
            var facep = _imagePathsFacep[new Random().Next(_imagePathsFacep.Length)];
            await Context.Channel.SendMessageAsync($"**{Context.User.Mention}** walnął facepalma.");
            await Context.Channel.SendFileAsync(facep);
        }

        //przytul
        private static string[] _imagePathsHug = new[] { "Fun/Hug/1.jpg", "Fun/Hug/2.jpg", "Fun/Hug/3.jpg", "Fun/Hug/4.jpg", "Fun/Hug/5.jpg", "Fun/Hug/6.jpg", "Fun/Hug/7.jpg" };
        [Command("przytul")]
        [Alias("hug")]
        [Summary("Wysyła losowy obrazek.")]
        public async Task Hugalm(IGuildUser user)
        {
            var Hug = _imagePathsHug[new Random().Next(_imagePathsHug.Length)];
            await Context.Channel.SendMessageAsync($"**{Context.User.Mention}** przytula **{user.Mention}**.");
            await Context.Channel.SendFileAsync(Hug);
        }

        //triger

        //avatar
        [Command("avatar")]
        [Alias("logo")]
        [Summary("Wysyła logo.")]
        public async Task Avatar(IGuildUser user)
        {
            if (user == null)
            {
                string avatar = Context.User.GetAvatarUrl();
                await ReplyAsync($"{avatar}");
            }
            if (user.IsBot)
            {
                return;
            }
            string useravatar = user.GetAvatarUrl();
            await ReplyAsync($"{useravatar}");
        }
        //emojify

        }
    }
