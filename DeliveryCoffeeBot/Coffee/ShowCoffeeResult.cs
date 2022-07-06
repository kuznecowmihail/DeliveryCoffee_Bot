using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DeliveryCoffeeBot.Coffee
{
    public class ShowCoffeeResultCommand : Command
    {
        public override string Name => "showcoffeeresult";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            if (!ChatCoffeeParticipants.Participants.ContainsKey(chatId)
                || (ChatCoffeeParticipants.Participants[chatId].Date.Year != DateTime.Now.Year
                && ChatCoffeeParticipants.Participants[chatId].Date.Month != DateTime.Now.Month
                && ChatCoffeeParticipants.Participants[chatId].Date.Day != DateTime.Now.Day))
            {
                await client.SendTextMessageAsync(chatId, "Извините, для начала используйте команду '/coffeetime'");
            }
            else if (ChatCoffeeParticipants.Participants[chatId].Participants.Count <= 1)
            {
                await client.SendTextMessageAsync(chatId, "Извините, не набралось необходимое количество участников!");
            }
            else if (ChatCoffeeParticipants.Participants[chatId].IsUsed)
            {
                await client.SendTextMessageAsync(chatId, "Извините, сегодня команда уже использовалась, попробуйте завтра!");
            }
            else
            {
                ChatCoffeeParticipants.Participants[chatId].IsUsed = true;
                var shuffleParticipants = ChatCoffeeParticipants.Participants[chatId].Participants.OrderBy(x => Guid.NewGuid().ToString()).ToList();
                var pairCount = shuffleParticipants.Count / 2;
                var winnerCount = new Random().Next(1, pairCount + 1);
                var resultMessage = new StringBuilder();

                for(var i = 0; i < winnerCount; i++)
                {
                    var winner = !string.IsNullOrWhiteSpace(shuffleParticipants[i * 2].UserName)
                        ? shuffleParticipants[i * 2].UserName
                        : $"tg://user?id={shuffleParticipants[i * 2].Id}";
                    var loser = !string.IsNullOrWhiteSpace(shuffleParticipants[i * 2 + 1].UserName)
                        ? shuffleParticipants[i * 2 + 1].UserName
                        : $"tg://user?id={shuffleParticipants[i * 2 + 1].Id}";
                    resultMessage.Append($"Получает кофе @{winner} от @{loser}\r\n");
                }
                await client.SendTextMessageAsync(chatId, resultMessage.ToString());
            }
        }
    }
}
