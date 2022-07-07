using DeliveryCoffeeBot.Doner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DeliveryCoffeeBot.Doner
{
    public class DonerTimeCommand : Command
    {
        public override string Name => "donertime";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;


            if (!ChatDonerParticipants.Participants.ContainsKey(chatId))
            {
                ChatDonerParticipants.Participants.Add(chatId, new DonerParticipants());
            }
            else if (ChatDonerParticipants.Participants[chatId].Date.Year == DateTime.Now.Year
                && ChatDonerParticipants.Participants[chatId].Date.Month == DateTime.Now.Month
                && ChatDonerParticipants.Participants[chatId].Date.Day == DateTime.Now.Day
                && ChatDonerParticipants.Participants[chatId].IsUsing)
            {
                await client.SendTextMessageAsync(chatId, "Извините, уже решается, кто будет доставщиком шаурмы.");

                return;
            }
            else
            {
                ChatDonerParticipants.Participants[chatId].Participants = new List<Participant>();
            }
            ChatDonerParticipants.Participants[chatId].IsUsing = true;

            await client.SendTextMessageAsync(chatId, "Хорошо! Если вы решили поесть шавы, нажмите на команду '/wantdoner'.");
        }
    }
}
