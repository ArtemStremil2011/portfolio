using ChatBot.Dtos;
using ChatBot.Repositories.Interfaces;
using Telegram.Bot;
using System.Collections.Concurrent;

namespace ChatBot.Commands
{
    public class ModelCommand : IBotCommand
    {
        public string Trigger => "/model";

        private static readonly ConcurrentDictionary<long, string> _userModels = new();

        public async Task ExecuteAsync(TelegramUpdate update, ITelegramBotClient bot, long chatId)
        {
            if (update.Message == null || string.IsNullOrWhiteSpace(update.Message.Text))
            {
                await bot.SendTextMessageAsync(chatId, "Ошибка: сообщение не содержит текст");
                return;
            }

            var parts = update.Message.Text.Split(' ', 2);

            if (parts.Length < 2 || string.IsNullOrWhiteSpace(parts[1]))
            {
                var currentModel = _userModels.GetOrAdd(chatId, "gpt-3.5-turbo");
                await bot.SendTextMessageAsync(chatId,
                    $"Использование: /model <название_модели>\n" +
                    $"Текущая модель: {currentModel}\n\n" +
                    $"Примеры:\n" +
                    $"/model openrouter/hunter-alpha\n" +
                    $"/model gpt-3.5-turbo\n" +
                    $"/model claude-2");
                return;
            }

            var newModel = parts[1].Trim();
            _userModels.AddOrUpdate(chatId, newModel, (key, oldValue) => newModel);
            await bot.SendTextMessageAsync(chatId, $"Модель изменена на: {newModel}");
        }
    }
}