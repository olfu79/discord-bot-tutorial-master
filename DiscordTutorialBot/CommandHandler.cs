using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DiscordTutorialBot.Core.LevelingSystem;
using DiscordTutorialBot.Core.UserAccounts;

namespace DiscordTutorialBot
{
    class CommandHandler
    {
        DiscordSocketClient _client;
        CommandService _service;

        public async Task InitializeAsync(DiscordSocketClient client)
        {
            _client = client;
            _service = new CommandService();
            await _service.AddModulesAsync(Assembly.GetEntryAssembly());
            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            if (!(s is SocketUserMessage msg)) return;
            var context = new SocketCommandContext(_client, msg);
            if (context.User.IsBot) return;

            // Mute check
            var userAccount = UserAccounts.GetAccount(context.User);

            if (CheckIfMuted(context.User) == true)
            {
                await context.Message.DeleteAsync();
                return;
            }

            // Leveling up
            Leveling.UserSentMessage((SocketGuildUser)context.User, (SocketTextChannel)context.Channel);

            int argPos = 0;
            if (msg.HasStringPrefix(Config.bot.cmdPrefix, ref argPos)
                || msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var result = await _service.ExecuteAsync(context, argPos);
                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
            // warn


        }
        private bool CheckIfMuted(SocketUser contextUser)
        {
            UserAccount account = UserAccounts.GetAccount(contextUser);
            return account.UnmuteTime - DateTime.Now >= TimeSpan.Zero;
        }
    }
}