using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DeliveryCoffeeBot.Coffee
{
    public class WantCoffeeCommand : Command
    {
        public override string Name => "wantcoffee";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            if (!ChatCoffeeParticipants.Participants.ContainsKey(chatId)
                || (ChatCoffeeParticipants.Participants[chatId].Date.Year != DateTime.Now.Year
                && ChatCoffeeParticipants.Participants[chatId].Date.Month != DateTime.Now.Month
                && ChatCoffeeParticipants.Participants[chatId].Date.Day != DateTime.Now.Day))
            {
                await client.SendTextMessageAsync(chatId, "Извините, для начала используйте команду '/coffeetime'");

                return;
            }
            if((!string.IsNullOrEmpty(message.From.Username)
                && ChatCoffeeParticipants.Participants[chatId].Participants.Select(t => t.UserName).Contains(message.From.Username))
                || (!string.IsNullOrEmpty(message.From.Username)
                    && ChatCoffeeParticipants.Participants[chatId].Participants.Select(t => t.Id).Contains(message.From.Id)))
            {
                await client.SendTextMessageAsync(chatId, "Извините, вы уже в деле!");

                return;
            }
            ChatCoffeeParticipants.Participants[chatId].Participants.Add(new Participant(message.From.Username, message.From.Id));

            await client.SendTextMessageAsync(chatId, "Ты в деле!", replyToMessageId: message.MessageId);
        }
    }
}
