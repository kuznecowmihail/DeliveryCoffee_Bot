using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DeliveryCoffeeBot.Coffee
{
    public class CoffeeTimeCommand : Command
    {
        public override string Name => "coffeetime";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;


            if (!ChatCoffeeParticipants.Participants.ContainsKey(chatId))
            {
                ChatCoffeeParticipants.Participants.Add(chatId, new CoffeeParticipants());
            }
            else if (ChatCoffeeParticipants.Participants[chatId].Date.Year == DateTime.Now.Year
                && ChatCoffeeParticipants.Participants[chatId].Date.Month == DateTime.Now.Month
                && ChatCoffeeParticipants.Participants[chatId].Date.Day == DateTime.Now.Day)
            {
                await client.SendTextMessageAsync(chatId, "Извините, сегодня команда уже использовалась, попробуйте завтра!");

                return;
            }
            else
            {
                ChatCoffeeParticipants.Participants[chatId].Participants = new List<Participant>();
                ChatCoffeeParticipants.Participants[chatId].Date = DateTime.Now;
                ChatCoffeeParticipants.Participants[chatId].IsUsed = false;
            }

            await client.SendTextMessageAsync(chatId, "Хорошо! Если ты хочешь кофе, используй команду '/wantcoffee'.");
        }
    }
}
