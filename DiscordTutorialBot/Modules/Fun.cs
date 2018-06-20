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

        //powitanie

        //uderzenie

        //dobranoc

        //dzieńdobry

        //papa / narka

        //facepalm

        //przytul

        //slub

        //triger

        //avatar

        //emojify

        // POŻEGANNIE

        //
    }
}
