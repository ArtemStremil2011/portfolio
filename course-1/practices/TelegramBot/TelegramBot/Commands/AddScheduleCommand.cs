using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Text.Json;

namespace ScheduleBot.Commands;

public class AddScheduleCommand : ICommand
{
    protected readonly IScheduleRepository _scheduleRepository;

    public AddScheduleCommand(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task ExecuteAsync(Update update, ITelegramBotClient botClient, CancellationToken ct)
    {
        var chatId = update.Message!.Chat.Id;
        var text = update.Message!.Text ?? string.Empty;

        // Проверяем формат команды
        if (text.Trim() == "/addschedule")
        {
            await botClient.SendTextMessageAsync(
                chatId,
                "Отправьте расписание в формате:\n" +
                "/addschedule [группа] [день] [время] [предмет] [учитель]\n\n" +
                "Пример: /addschedule 9A Monday 09:00 Математика Иванов\n\n" +
                "Дни недели: Monday, Tuesday, Wednesday, Thursday, Friday",
                cancellationToken: ct);
            return;
        }

        var parts = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 5)
        {
            await botClient.SendTextMessageAsync(
                chatId,
                "Недостаточно данных. Используйте формат:\n" +
                "/addschedule [группа] [день] [время] [предмет] [учитель]",
                cancellationToken: ct);
            return;
        }

        try
        {
            var groupName = parts[1];
            var day = parts[2];
            var time = parts[3];
            var subject = parts[4];
            var teacher = parts.Length > 5 ? string.Join(' ', parts.Skip(5)) : "";

            var schedule = _scheduleRepository.Load();

            // Ищем или создаем группу
            var group = schedule.Groups.FirstOrDefault(g => g.Group.Equals(groupName, StringComparison.OrdinalIgnoreCase));
            if (group == null)
            {
                group = new GroupSchedule { Group = groupName, Days = new List<DaySchedule>() };
                schedule.Groups.Add(group);
            }

            // Ищем или создаем день
            var daySchedule = group.Days.FirstOrDefault(d => d.Day.Equals(day, StringComparison.OrdinalIgnoreCase));
            if (daySchedule == null)
            {
                daySchedule = new DaySchedule { Day = day, Lessons = new List<Lesson>() };
                group.Days.Add(daySchedule);
            }

            // Добавляем урок
            daySchedule.Lessons.Add(new Lesson(time, subject, teacher));

            // Сохраняем изменения через репозиторий
            _scheduleRepository.Save(schedule);

            await botClient.SendTextMessageAsync(
                chatId,
                $"✅ Урок добавлен!\n" +
                $"Группа: {groupName}\n" +
                $"День: {day}\n" +
                $"Время: {time}\n" +
                $"Предмет: {subject}\n" +
                $"Учитель: {teacher}",
                cancellationToken: ct);
        }
        catch (Exception ex)
        {
            await botClient.SendTextMessageAsync(
                chatId,
                $"❌ Ошибка при добавлении расписания: {ex.Message}",
                cancellationToken: ct);
        }
    }
}