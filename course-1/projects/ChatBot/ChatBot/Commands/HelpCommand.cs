using ChatBot.Dtos;
using Telegram.Bot;

namespace ChatBot.Commands
{
    public class HelpCommand : IBotCommand
    {
        private readonly ILogger<HelpCommand> _logger;

        public HelpCommand(ILogger<HelpCommand> logger)
        {
            _logger = logger;
        }

        public string Trigger => "/help";

        public async Task ExecuteAsync(TelegramUpdate update, ITelegramBotClient bot, long chatId)
        {
            _logger.LogInformation("Команда /help выполнена для чата {ChatId}", chatId);
            var message = "Список доступных команд\n\n" +
            "/start -- начало работы\n" +
            "/help -- показать этот список\n" +
            "/stats -- статистика чата (сообщения, токены)\n" +
            "/clear -- очистить историю переписки\n" +
            "/summarize -- краткий пересказ диалога\n" +
            "/undo -- удалить последнее сообщение\n" +
            "/model -- изменить модель\n" +
            "/system -- добавить системное сообщение";
            await bot.SendTextMessageAsync(chatId, message);
        }
    }
}