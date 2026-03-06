using ScheduleBot.Commands;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

class Program
{
    private const string ScheduleJson = "schedule.json";

    public static async Task Main()
    {
        Console.WriteLine("Запуск бота...");

        var token = "8269579124:AAHneGoIOlAlyC1_RXMk_W7yTBSFEDN3ouk";
        var botClient = new TelegramBotClient(token);

        // Очищаем вебхук перед запуском
        await botClient.DeleteWebhookAsync();

        var scheduleRepository = new JsonScheduleRepository(ScheduleJson);

        // Создаём диспетчер и регистрируем команды
        var dispatcher = new CommandDispatcher();

        // Регистрация всех команд
        dispatcher.Register("/start", new StartCommand());
        dispatcher.Register("/help", new HelpCommand());
        dispatcher.Register("/week", new WeekCommand(scheduleRepository));
        dispatcher.Register("/today", new TodayCommand(scheduleRepository));
        dispatcher.Register("/addschedule", new AddScheduleCommand(scheduleRepository));

        // Выводим список зарегистрированных команд для проверки
        Console.WriteLine("Зарегистрированные команды: /start, /help, /week, /today, /addschedule");

        using var cts = new CancellationTokenSource();
        var receiverOptions = new ReceiverOptions { AllowedUpdates = Array.Empty<UpdateType>() };

        botClient.StartReceiving(
            async (client, update, ct) => await dispatcher.DispatchAsync(update, client, ct),
            HandleErrorAsync,
            receiverOptions,
            cts.Token);

        var me = await botClient.GetMeAsync();
        Console.WriteLine($"Бот запущен: @{me.Username}");
        Console.ReadLine();
        cts.Cancel();
    }

    static Task HandleErrorAsync(ITelegramBotClient bot, Exception ex, CancellationToken ct)
    {
        Console.WriteLine($"Ошибка: {ex.Message}");
        return Task.CompletedTask;
    }
}