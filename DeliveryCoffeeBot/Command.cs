using Telegram.Bot;
using Telegram.Bot.Types;

namespace DeliveryCoffeeBot
{
    public abstract class Command
    {
        public abstract string Name { get; }
        public abstract void Execute(Message message, TelegramBotClient client);
        public bool Contains(string command)
        {
            return !string.IsNullOrEmpty(command)
                ? command.Contains(this.Name)
                : false;// && command.Contains(AppSettings.Name);
        }
    }
}
