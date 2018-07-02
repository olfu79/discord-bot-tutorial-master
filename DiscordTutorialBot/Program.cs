using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordTutorialBot.Core;
using DiscordTutorialBot.Modules;
using DiscordTutorialBot.Core.UserAccounts;

namespace DiscordTutorialBot
{
    class Program
    {
        DiscordSocketClient _client;
        CommandHandler _handler;


        static void Main(string[] args)
        => new Program().StartAsync().GetAwaiter().GetResult();

        public async Task StartAsync()
        {
            if (Config.bot.token == "" || Config.bot.token == null) return;
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });
            _client.Log += Log;
            _client.Ready += RepeatingTimer.StartTimer;
            _client.ReactionAdded += OnReactionAdded;
            await _client.LoginAsync(TokenType.Bot, Config.bot.token);
            await _client.StartAsync();
            Global.Client = _client;
            _client.Ready += SetGame;
            _handler = new CommandHandler();
            await _handler.InitializeAsync(_client);
            await ConsoleInput();
            await Task.Delay(-1);
        }
        private async Task UserJoined(SocketGuildUser user)
        {
            var role = user.Guild.Roles.FirstOrDefault(x => x.Name == "Nowy");
            await user.AddRoleAsync(role);
        }

        private async Task UserLeft(SocketGuildUser user)
        {
            var channel = user.Guild.GetTextChannel(448884032391086092);
            await channel.SendMessageAsync("Żegnaj " + user.Nickname + " Będzie nam Ciebie brakować.. :frowning:");
        }

        private async Task SetGame()
        {
            await _client.SetGameAsync("GGWP BOT");
        }

        private async Task ConsoleInput()
        {
            var input = string.Empty;
            while (input.Trim().ToLower() != "block")
            {
                input = Console.ReadLine();
                if (input.Trim().ToLower() == "message")
                    ConsoleSendMessage();
            }
        }

        private async void ConsoleSendMessage()
        {
            Console.WriteLine("Select the guild:");
            var guild = GetSelectedGuild(_client.Guilds);
            var textChannel = GetSelectedTextChannel(guild.TextChannels);
            var msg = string.Empty;
            while (msg.Trim() == string.Empty)
            {
                Console.WriteLine("Your message:");
                msg = Console.ReadLine();
            }

            await textChannel.SendMessageAsync(msg);
        }

        private SocketTextChannel GetSelectedTextChannel(IEnumerable<SocketTextChannel> channels)
        {
            var textChannels = channels.ToList();
            var maxIndex = textChannels.Count - 1;
            for (var i = 0; i <= maxIndex; i++)
            {
                Console.WriteLine($"{i} - {textChannels[i].Name}");
            }

            var selectedIndex = -1;
            while (selectedIndex < 0 || selectedIndex > maxIndex)
            {
                var success = int.TryParse(Console.ReadLine().Trim(), out selectedIndex);
                if (!success)
                {
                    Console.WriteLine("That was an invalid index, try again.");
                    selectedIndex = -1;
                }
            }

            return textChannels[selectedIndex];
        }

        private SocketGuild GetSelectedGuild(IEnumerable<SocketGuild> guilds)
        {
            var socketGuilds = guilds.ToList();
            var maxIndex = socketGuilds.Count - 1;
            for(var i = 0; i <= maxIndex; i++)
            {
                Console.WriteLine($"{i} - {socketGuilds[i].Name}");
            }

            var selectedIndex = -1;
            while (selectedIndex < 0 || selectedIndex > maxIndex)
            {
                var success = int.TryParse(Console.ReadLine().Trim(), out selectedIndex);
                if (!success)
                {
                    Console.WriteLine("That was an invalid index, try again.");
                    selectedIndex = -1;
                }
            }

            return socketGuilds[selectedIndex];
        }

        private async Task OnReactionAdded(Cacheable<IUserMessage, ulong> cache, ISocketMessageChannel channel, SocketReaction reaction)
        {
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

            var coin = Emote.Parse("<:coin:462351821910835200>");
            var err = Emote.Parse("<:WrongMark:460770239286870017>");

            if (reaction.MessageId == Global.MessageIdToTrack)
            {
                var guildUserR = (SocketGuildUser)reaction.User;
                var guildChannel = channel as IGuildChannel;
                var guild = guildChannel.Guild as SocketGuild;

                var account = UserAccounts.GetAccount(reaction.User.Value);
                ulong balnace = account.Money;
                ulong NoWarnings = account.NumberOfWarnings;

                var VIProle = guild.Roles.FirstOrDefault(x => x.Name == "VIP");
                var SVIProle = guild.Roles.FirstOrDefault(x => x.Name == "SVIP");
                var Musicrole = guild.Roles.FirstOrDefault(x => x.Name == "🎵");
                var Nickrole = guild.Roles.FirstOrDefault(x => x.Name == "🏷️");
                var Akirole = guild.Roles.FirstOrDefault(x => x.Name == "👳");
                var Chanrole = guild.Roles.FirstOrDefault(x => x.Name == "🎙️");

                var msg = await cache.GetOrDownloadAsync();

                if (reaction.User.Value.IsBot)
                {
                    return;
                }
                else
                {
                    if (reaction.Emote.Name == "CSGO2")
                    {
                        var role1 = guild.Roles.FirstOrDefault(x => x.Name == "CS:GO");
                        var guildUser1 = (SocketGuildUser)reaction.User;
                        await guildUser1.AddRoleAsync(role1);
                    }
                    else if (reaction.Emote.Name == "LeageOfLegends")
                    {
                        var role2 = guild.Roles.FirstOrDefault(x => x.Name == "LOL");
                        var guildUser2 = (SocketGuildUser)reaction.User;
                        await guildUser2.AddRoleAsync(role2);
                    }
                    else if (reaction.Emote.Name == "pubg")
                    {
                        var role3 = guild.Roles.FirstOrDefault(x => x.Name == "PUBG");
                        var guildUser3 = (SocketGuildUser)reaction.User;
                        await guildUser3.AddRoleAsync(role3);
                    }
                    else if (reaction.Emote.Name == "fortnite")
                    {
                        var role4 = guild.Roles.FirstOrDefault(x => x.Name == "FORTNITE");
                        var guildUser4 = (SocketGuildUser)reaction.User;
                        await guildUser4.AddRoleAsync(role4);
                    }
                    else if (reaction.Emote.Name == "overwatch")
                    {
                        var role5 = guild.Roles.FirstOrDefault(x => x.Name == "OVERWATCH");
                        var guildUser5 = (SocketGuildUser)reaction.User;
                        await guildUser5.AddRoleAsync(role5);
                    }
                    else if (reaction.Emote.Name == "roblox")
                    {
                        var role6 = guild.Roles.FirstOrDefault(x => x.Name == "ROBLOX");
                        var guildUser6 = (SocketGuildUser)reaction.User;
                        await guildUser6.AddRoleAsync(role6);
                    }
                    else if (reaction.Emote.Name == "gta")
                    {
                        var role7 = guild.Roles.FirstOrDefault(x => x.Name == "GTA V");
                        var guildUser7 = (SocketGuildUser)reaction.User;
                        await guildUser7.AddRoleAsync(role7);
                    }
                    else if (reaction.Emote.Name == "minecraft")
                    {
                        var role8 = guild.Roles.FirstOrDefault(x => x.Name == "MINECRAFT");
                        var guildUser8 = (SocketGuildUser)reaction.User;
                        await guildUser8.AddRoleAsync(role8);
                    }
                    else if (reaction.Emote.Name == "Sims")
                    {
                        var role9 = guild.Roles.FirstOrDefault(x => x.Name == "SIMS");
                        var guildUser9 = (SocketGuildUser)reaction.User;
                        await guildUser9.AddRoleAsync(role9);
                    }
                    else if (reaction.Emote.Name == "Rocketleague")
                    {
                        var role10 = guild.Roles.FirstOrDefault(x => x.Name == "ROCKET LEAGUE");
                        var guildUser10 = (SocketGuildUser)reaction.User;
                        await guildUser10.AddRoleAsync(role10);
                    }
                    else if (reaction.Emote.Name == "Unturned")
                    {
                        var role11 = guild.Roles.FirstOrDefault(x => x.Name == "UNTURNED");
                        var guildUser11 = (SocketGuildUser)reaction.User;
                        await guildUser11.AddRoleAsync(role11);
                    }
                    else if (reaction.Emote.Name == "\u0031\u20e3")
                    {
                        if(balnace < 5000)
                        {
                            await channel.SendMessageAsync($"{err} Masz za mało pieniędzy.");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        if(guildUserR.Roles.Contains(VIProle))
                        {
                            await channel.SendMessageAsync("Masz już rangę **VIP**.");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        account.Money -= 5000;
                        UserAccounts.SaveAccounts();
                        var roleVIP = guild.Roles.FirstOrDefault(x => x.Name == "VIP");
                        var guildUserVIP = (SocketGuildUser)reaction.User;
                        await guildUserVIP.AddRoleAsync(roleVIP);
                        await channel.SendMessageAsync("Pomyślnie zakupiono: **RANGA VIP**.");
                    }
                    else if (reaction.Emote.Name == "\u0032\u20e3")
                    {
                        if (balnace < 10000)
                        {
                            await channel.SendMessageAsync($"{err} Masz za mało pieniędzy.");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        if (guildUserR.Roles.Contains(SVIProle))
                        {
                            await channel.SendMessageAsync("Masz już rangę **SVIP**.");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        account.Money -= 10000;
                        UserAccounts.SaveAccounts();
                        var roleSVIP = guild.Roles.FirstOrDefault(x => x.Name == "SVIP");
                        var guildUserSVIP = (SocketGuildUser)reaction.User;
                        await guildUserSVIP.AddRoleAsync(roleSVIP);
                        await channel.SendMessageAsync("Pomyślnie zakupiono: **RANGA SVIP**.");
                    }
                    else if (reaction.Emote.Name == "\u0033\u20e3")
                    {
                        if (balnace < 3000)
                        {
                            await channel.SendMessageAsync($"{err} Masz za mało pieniędzy.");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        if (guildUserR.Roles.Contains(Musicrole))
                        {
                            await channel.SendMessageAsync("Masz już: **DOSTĘP DO ZMIANY MUZYKI**.");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }

                        account.Money -= 3000;
                        UserAccounts.SaveAccounts();
                        var roleMusic = guild.Roles.FirstOrDefault(x => x.Name == "🎵");
                        var guildUserMusic = (SocketGuildUser)reaction.User;
                        await guildUserMusic.AddRoleAsync(roleMusic);
                        await channel.SendMessageAsync("Pomyślnie zakupiono: **DOSTĘP DO ZMIANY MUZYKI**.");
                    }
                    else if (reaction.Emote.Name == "\u0034\u20e3")
                    {
                        if (balnace < 3000)
                        {
                            await channel.SendMessageAsync($"{err} Masz za mało pieniędzy.");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        if (guildUserR.Roles.Contains(Akirole))
                        {
                            await channel.SendMessageAsync("Masz już: **DOSTĘP DO AKINATORA**.");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }

                        account.Money -= 3000;
                        UserAccounts.SaveAccounts();
                        var roleAki = guild.Roles.FirstOrDefault(x => x.Name == "👳");
                        var guildUserAki = (SocketGuildUser)reaction.User;
                        await guildUserAki.AddRoleAsync(roleAki);
                        await channel.SendMessageAsync("Pomyślnie zakupiono: **DOSTĘP DO AKINATORA**.");
                    }
                    else if (reaction.Emote.Name == "\u0035\u20e3")
                    {
                        if (balnace < 750)
                        {
                            await channel.SendMessageAsync($"{err} Masz za mało pieniędzy.");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        if (guildUserR.Roles.Contains(Nickrole))
                        {
                            await channel.SendMessageAsync("Masz już: **ZMIANĘ NICKU**.");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }

                        account.Money -= 750;
                        UserAccounts.SaveAccounts();
                        var roleNi = guild.Roles.FirstOrDefault(x => x.Name == "🏷️");
                        var guildUserNi = (SocketGuildUser)reaction.User;
                        await guildUserNi.AddRoleAsync(roleNi);
                        await channel.SendMessageAsync("Pomyślnie zakupiono: **ZMIANĘ NICKU**.");
                    }
                    else if (reaction.Emote.Name == "\u0036\u20e3")
                    {
                        if (balnace < 10000)
                        {
                            await channel.SendMessageAsync($"{err} Masz za mało pieniędzy.");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        if (guildUserR.Roles.Contains(Chanrole))
                        {
                            await channel.SendMessageAsync("Masz już: **PRYWATNY KANAŁ**.");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        Random rnd = new Random();
                        int channelName = rnd.Next(100000000, 1999999999);

                        var CreateChannel = await guild.CreateVoiceChannelAsync(channelName.ToString());

                        var perms = new OverwritePermissions(readMessages: PermValue.Deny);
                        await CreateChannel.AddPermissionOverwriteAsync(guild.EveryoneRole, perms);

                        perms = new OverwritePermissions(readMessages: PermValue.Allow);
                        await CreateChannel.AddPermissionOverwriteAsync(guildUserR, perms);

                        var category = guild.CategoryChannels.FirstOrDefault(chan => chan.Name == "CHANNELS");
                        CreateChannel.ModifyAsync(chan => chan.CategoryId = category.Id);

                        account.Money -= 10000;
                        UserAccounts.SaveAccounts();
                        var roleCh = guild.Roles.FirstOrDefault(x => x.Name == "🎙️");
                        var guildUserCh = (SocketGuildUser)reaction.User;
                        await guildUserCh.AddRoleAsync(roleCh);
                        await channel.SendMessageAsync("Pomyślnie zakupiono: **PRYWATNY KANAŁ**.");
                    }
                    else if (reaction.Emote.Name == "\u0037\u20e3")
                    {
                        if (balnace < 1000)
                        {
                            await channel.SendMessageAsync($"{err} Masz za mało pieniędzy.");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        account.Money -= 1000;
                        UserAccounts.SaveAccounts();

                        Random rand1 = new Random();
                        int chanceSmallBox = rand1.Next(0, 100);
                        //nic
                        if(chanceSmallBox <= 40)
                        {
                            await channel.SendMessageAsync(":cry: Nic nie wygrałeś!");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        // kasa od 500 do 1000
                        if (chanceSmallBox <= 70)
                        {
                            Random randSmSm = new Random();
                            int SmallBoxSm = randSmSm.Next(500, 1001);
                            ulong RewardSm = (ulong)(int)SmallBoxSm;

                            account.Money += RewardSm;

                            await channel.SendMessageAsync($":confetti_ball: Gratulacje! WYGRAŁEŚ MAŁĄ NAGRODĘ: **{RewardSm}**{coin}");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        //kasa od 1000 do 2000
                        if (chanceSmallBox <= 100)
                        {
                            Random randSmBg = new Random();
                            int SmallBoxBg = randSmBg.Next(1000, 2001);
                            ulong RewardBg = (ulong)(int)SmallBoxBg;

                            account.Money += RewardBg;

                            await channel.SendMessageAsync($":confetti_ball: Gratulacje! WYGRAŁEŚ DUŻĄ NAGRODE: **{RewardBg}**{coin}");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                    }
                    else if (reaction.Emote.Name == "\u0038\u20e3")
                    {
                        if (balnace < 2000)
                        {
                            await channel.SendMessageAsync($"{err} Masz za mało pieniędzy.");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        account.Money -= 2000;
                        UserAccounts.SaveAccounts();

                        Random rand2 = new Random();
                        int chanceMediumBox = rand2.Next(0, 100);
                        //nic
                        if (chanceMediumBox <= 20)
                        {
                            await channel.SendMessageAsync(":cry: Nic nie wygrałeś!");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        // kasa od 1000 do 2000
                        if (chanceMediumBox <= 40)
                        {
                            Random randMdSm = new Random();
                            int MediumBoxSm = randMdSm.Next(1000, 2001);
                            ulong RewardSm = (ulong)(int)MediumBoxSm;

                            account.Money += RewardSm;

                            await channel.SendMessageAsync($":confetti_ball: Gratulacje! WYGRAŁEŚ MAŁĄ NAGRODĘ: **{RewardSm}**{coin}");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        //kasa od 2000 do 4000
                        if (chanceMediumBox <= 60)
                        {
                            Random randMdBg = new Random();
                            int MediumBoxBg = randMdBg.Next(2000, 4001);
                            ulong RewardBg = (ulong)(int)MediumBoxBg;

                            account.Money += RewardBg;

                            await channel.SendMessageAsync($":confetti_ball: Gratulacje! WYGRAŁEŚ DUŻĄ NAGRODE: **{RewardBg}**{coin}");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        //vip
                        if(chanceMediumBox <= 70)
                        {
                            var roleMdVip = guild.Roles.FirstOrDefault(x => x.Name == "VIP");
                            var guildUserMdVip = (SocketGuildUser)reaction.User;
                            await guildUserMdVip.AddRoleAsync(roleMdVip);

                            await channel.SendMessageAsync($":confetti_ball: Gratulacje! WYGRAŁEŚ **RANGE VIP!**");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        //zmiana nicku
                        if(chanceMediumBox <= 90)
                        {
                            var roleNi = guild.Roles.FirstOrDefault(x => x.Name == "🏷️");
                            var guildUserNi = (SocketGuildUser)reaction.User;
                            await guildUserNi.AddRoleAsync(roleNi);

                            await channel.SendMessageAsync($":confetti_ball: Gratulacje! WYGRAŁEŚ **ZMIANĘ NICKU**");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        //usuniecie warna
                        if(chanceMediumBox <= 100)
                        {
                            NoWarnings -= 1;

                            await channel.SendMessageAsync($":confetti_ball: Gratulacje! WYGRAŁEŚ **USUNIĘCIE 1 OSTRZEŻENIA**");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                    }
                    else if (reaction.Emote.Name == "\u0039\u20e3")
                    {
                        if (balnace < 3000)
                        {
                            await channel.SendMessageAsync($"{err} Masz za mało pieniędzy.");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        account.Money -= 3000;
                        UserAccounts.SaveAccounts();

                        Random rand3 = new Random();
                        int chanceBigBox = rand3.Next(0, 100);
                        //nic 10
                        if(chanceBigBox <= 10)
                        {
                            await channel.SendMessageAsync(":cry: Nic nie wygrałeś!");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        //vip 8
                        if (chanceBigBox <= 18)
                        {
                            var roleBigVip = guild.Roles.FirstOrDefault(x => x.Name == "VIP");
                            var guildUserBigVip = (SocketGuildUser)reaction.User;
                            await guildUserBigVip.AddRoleAsync(roleBigVip);

                            await channel.SendMessageAsync($":confetti_ball: Gratulacje! WYGRAŁEŚ **RANGE VIP!**");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        //svip 5
                        if (chanceBigBox <= 23)
                        {
                            var roleBigSVip = guild.Roles.FirstOrDefault(x => x.Name == "SVIP");
                            var guildUserBigSVip = (SocketGuildUser)reaction.User;
                            await guildUserBigSVip.AddRoleAsync(roleBigSVip);

                            await channel.SendMessageAsync($":confetti_ball: Gratulacje! WYGRAŁEŚ **RANGE VIP!**");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        //money SM 20
                        if (chanceBigBox <= 43)
                        {
                            Random randBgSm = new Random();
                            int BgBoxSm = randBgSm.Next(2000, 3001);
                            ulong RewardSm = (ulong)(int)BgBoxSm;

                            account.Money += RewardSm;

                            await channel.SendMessageAsync($":confetti_ball: Gratulacje! WYGRAŁEŚ MAŁĄ NAGRODE: **{RewardSm}**{coin}");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        //money BG 15
                        if (chanceBigBox <= 58)
                        {
                            Random randBgBg = new Random();
                            int BgBoxBg = randBgBg.Next(3000, 5001);
                            ulong RewardBg = (ulong)(int)BgBoxBg;

                            account.Money += RewardBg;

                            await channel.SendMessageAsync($":confetti_ball: Gratulacje! WYGRAŁEŚ DUŻĄ NAGRODE: **{RewardBg}**{coin}");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        //jackpot 1
                        if (chanceBigBox == 59)
                        {
                            const int jackpot = 50000;
                            account.Money += jackpot;
                            await channel.SendMessageAsync($":confetti_ball: :confetti_ball: :confetti_ball: **WYGRAŁEŚ JACKPOT: {jackpot}** {coin} :confetti_ball: :confetti_ball: :confetti_ball:");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;

                        }
                        //akinator 10
                        if (chanceBigBox <= 69)
                        {
                            var roleAki = guild.Roles.FirstOrDefault(x => x.Name == "👳");
                            var guildUserAki = (SocketGuildUser)reaction.User;
                            await guildUserAki.AddRoleAsync(roleAki);
                            await channel.SendMessageAsync($":confetti_ball: Gratulacje! WYGRAŁEŚ: **DOSTĘP DO AKINATORA**");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        //muzyka 10
                        if (chanceBigBox <= 79)
                        {
                            var roleMusic = guild.Roles.FirstOrDefault(x => x.Name == "🎵");
                            var guildUserMusic = (SocketGuildUser)reaction.User;
                            await guildUserMusic.AddRoleAsync(roleMusic);
                            await channel.SendMessageAsync($":confetti_ball: Gratulacje! WYGRAŁEŚ: **DOSTĘP DO ZMIANY MUZYKI**");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        //własny kanał 2
                        if (chanceBigBox <= 81)
                        {
                            Random rnd = new Random();
                            int channelName = rnd.Next(100000000, 1999999999);

                            var CreateChannel = await guild.CreateVoiceChannelAsync(channelName.ToString());

                            var perms = new OverwritePermissions(readMessages: PermValue.Deny);
                            await CreateChannel.AddPermissionOverwriteAsync(guild.EveryoneRole, perms);

                            perms = new OverwritePermissions(readMessages: PermValue.Allow);
                            await CreateChannel.AddPermissionOverwriteAsync(guildUserR, perms);

                            var category = guild.CategoryChannels.FirstOrDefault(chan => chan.Name == "CHANNELS");
                            CreateChannel.ModifyAsync(chan => chan.CategoryId = category.Id);

                            account.Money -= 10000;
                            UserAccounts.SaveAccounts();
                            var roleCh = guild.Roles.FirstOrDefault(x => x.Name == "🎙️");
                            var guildUserCh = (SocketGuildUser)reaction.User;
                            await guildUserCh.AddRoleAsync(roleCh);
                            await channel.SendMessageAsync($":confetti_ball: Gratulacje! WYGRAŁEŚ: **PRYWATNY KANAŁ**");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                        //zmiana nicku 19
                        if (chanceBigBox <= 100)
                        {
                            var roleNi = guild.Roles.FirstOrDefault(x => x.Name == "🏷️");
                            var guildUserNi = (SocketGuildUser)reaction.User;
                            await guildUserNi.AddRoleAsync(roleNi);

                            await channel.SendMessageAsync($":confetti_ball: Gratulacje! WYGRAŁEŚ **ZMIANĘ NICKU**");

                            await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);

                            return;
                        }
                    }
                    if (reaction.User.Value.IsBot)
                    {
                        return;
                    }
                    else
                    {
                        await msg.RemoveReactionAsync(reaction.Emote, reaction.User.Value);
                    }
                }
            }
        }

        private async Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.Message);
        }
    }
}
