using ChatBot.Commands;
using ChatBot.Dtos;
using Telegram.Bot;

public class HelpCommand : IBotCommand
{
    public string Trigger => "/help";
    public async Task ExecuteAsync(TelegramUpdate update, ITelegramBotClient bot, long chatId)
    {
        await bot.SendTextMessageAsync(chatId, "Список доступных команд: \n/start - начало работы.");
    }

}