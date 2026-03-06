using Telegram.Bot;
using Telegram.Bot.Types;

namespace ScheduleBot.Commands;

public class TodayCommand : ICommand
{
    protected readonly IScheduleRepository _scheduleRepository;

    public TodayCommand(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task ExecuteAsync(Update update, ITelegramBotClient botClient, CancellationToken ct)
    {
        var chatId = update.Message!.Chat.Id;
        var text = update.Message!.Text ?? string.Empty;

        // ожидаем формат: /today 9A
        var parts = text.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 2)
        {
            await botClient.SendTextMessageAsync(
                chatId,
                "Использование: /today [группа]\nНапример: /today 9A",
                cancellationToken: ct);
            return;
        }

        var groupName = parts[1].Trim();

        var schedule = _scheduleRepository.Load();
        var group = schedule.Groups
            .FirstOrDefault(g => string.Equals(g.Group, groupName, StringComparison.OrdinalIgnoreCase));

        if (group == null)
        {
            await botClient.SendTextMessageAsync(
                chatId,
                $"Для группы {groupName} нет расписания.",
                cancellationToken: ct);
            return;
        }

        // Определяем текущий день недели на русском
        var today = GetTodayRussian();

        var todaySchedule = group.Days
            .FirstOrDefault(d => string.Equals(d.Day, today, StringComparison.OrdinalIgnoreCase));

        if (todaySchedule == null)
        {
            await botClient.SendTextMessageAsync(
                chatId,
                $"Для группы {groupName} нет расписания на {today}.",
                cancellationToken: ct);
            return;
        }

        var lines = new List<string> { $"Расписание для {groupName} на {today}:" };

        if (todaySchedule.Lessons == null || todaySchedule.Lessons.Count == 0)
        {
            lines.Add("нет уроков");
        }
        else
        {
            lines.AddRange(
                todaySchedule.Lessons.Select(
                    (l, i) => $" {i + 1}. {l.Time} -- {l.Subject} {(string.IsNullOrEmpty(l.Teacher) ? "" : "(" + l.Teacher + ")")}"
                )
            );
        }

        await botClient.SendTextMessageAsync(chatId, string.Join('\n', lines), cancellationToken: ct);
    }

    private string GetTodayRussian()
    {
        var today = DateTime.Now.DayOfWeek;

        return today switch
        {
            DayOfWeek.Monday => "Monday",
            DayOfWeek.Tuesday => "Tuesday",
            DayOfWeek.Wednesday => "Wednesday",
            DayOfWeek.Thursday => "Thursday",
            DayOfWeek.Friday => "Friday",
            DayOfWeek.Saturday => "Saturday",
            DayOfWeek.Sunday => "Sunday",
            _ => "Monday"
        };
    }
}