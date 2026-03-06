using Telegram.Bot;
using Telegram.Bot.Types;

namespace ScheduleBot.Commands;

public class HelpCommand : ICommand
{
    public async Task ExecuteAsync(Update update, ITelegramBotClient botClient, CancellationToken ct)
    {
        var chatId = update.Message!.Chat.Id;

        string text = "📚 **Доступные команды:**\n\n" +
                     "**Основные команды:**\n" +
                     "/start - приветствие и информация о боте\n" +
                     "/help - показать это сообщение\n\n" +

                     "**Просмотр расписания:**\n" +
                     "/week [группа] - расписание на всю неделю\n" +
                     "  Пример: /week 9A\n\n" +

                     "/today [группа] - расписание на сегодня\n" +
                     "  Пример: /today 9A\n\n" +

                     "**Добавление расписания:**\n" +
                     "/addschedule - добавить новый урок\n" +
                     "  Формат: /addschedule [группа] [день] [время] [предмет] [учитель]\n" +
                     "  Пример: /addschedule 9A Monday 09:00 Математика Иванов\n\n" +

                     "**Дни недели:**\n" +
                     "• Monday - Понедельник\n" +
                     "• Tuesday - Вторник  \n" +
                     "• Wednesday - Среда\n" +
                     "• Thursday - Четверг\n" +
                     "• Friday - Пятница\n\n" +

                     "💡 **Совет:** Сначала добавьте расписание через /addschedule, затем смотрите его через /week или /today";

        await botClient.SendTextMessageAsync(
            chatId,
            text,
            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
            cancellationToken: ct);
    }
}