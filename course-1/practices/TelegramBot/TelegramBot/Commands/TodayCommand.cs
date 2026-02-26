using Telegram.Bot;
using Telegram.Bot.Types;

namespace ScheduleBot.Commands;

public class TodayCommand : ICommand
{
    protected readonly IScheduleRepository _scheduleRepository;

    // Словарь для перевода русских дней в английские
    private readonly Dictionary<string, string> _ruToEnDays = new()
    {
        ["понедельник"] = "Monday",
        ["вторник"] = "Tuesday",
        ["среда"] = "Wednesday",
        ["четверг"] = "Thursday",
        ["пятница"] = "Friday",
        ["суббота"] = "Saturday",
        ["воскресенье"] = "Sunday"
    };

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

        // Получаем сегодняшний день в разных форматах
        var todayEnglish = GetTodayEnglish();
        var todayRussian = GetTodayRussian();

        // Ищем расписание на сегодня (пробуем разные варианты)
        var todaySchedule = FindTodaySchedule(group, todayEnglish, todayRussian);

        if (todaySchedule == null)
        {
            await botClient.SendTextMessageAsync(
                chatId,
                $"Для группы {groupName} нет расписания на {todayRussian}.",
                cancellationToken: ct);
            return;
        }

        var lines = new List<string> { $"Расписание для {groupName} на {todayRussian}:" };

        if (todaySchedule.Lessons == null || todaySchedule.Lessons.Count == 0)
        {
            lines.Add("нет уроков");
        }
        else
        {
            lines.AddRange(
                todaySchedule.Lessons.OrderBy(l => l.Time).Select(
                    (l, i) => $" {i + 1}. {l.Time} -- {l.Subject} {(string.IsNullOrEmpty(l.Teacher) ? "" : "(" + l.Teacher + ")")}"
                )
            );
        }

        await botClient.SendTextMessageAsync(chatId, string.Join('\n', lines), cancellationToken: ct);
    }

    private DaySchedule? FindTodaySchedule(GroupSchedule group, string todayEnglish, string todayRussian)
    {
        // Сначала ищем точное совпадение с английским названием
        var schedule = group.Days.FirstOrDefault(d =>
            string.Equals(d.Day, todayEnglish, StringComparison.OrdinalIgnoreCase));

        if (schedule != null) return schedule;

        // Пробуем найти по русскому названию
        schedule = group.Days.FirstOrDefault(d =>
            string.Equals(d.Day, todayRussian, StringComparison.OrdinalIgnoreCase));

        if (schedule != null) return schedule;

        // Пробуем найти через словарь (если в JSON русское название, но в другом падеже)
        if (_ruToEnDays.TryGetValue(todayRussian.ToLower(), out string? englishDay))
        {
            schedule = group.Days.FirstOrDefault(d =>
                string.Equals(d.Day, englishDay, StringComparison.OrdinalIgnoreCase));
        }

        return schedule;
    }

    private string GetTodayEnglish()
    {
        var today = DateTime.Now.DayOfWeek;
        return today.ToString();
    }

    private string GetTodayRussian()
    {
        var today = DateTime.Now.DayOfWeek;

        return today switch
        {
            DayOfWeek.Monday => "Понедельник",
            DayOfWeek.Tuesday => "Вторник",
            DayOfWeek.Wednesday => "Среда",
            DayOfWeek.Thursday => "Четверг",
            DayOfWeek.Friday => "Пятница",
            DayOfWeek.Saturday => "Суббота",
            DayOfWeek.Sunday => "Воскресенье",
            _ => "Понедельник"
        };
    }
}