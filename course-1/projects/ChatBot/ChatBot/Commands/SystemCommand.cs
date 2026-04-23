using ChatBot.Dtos;
using ChatBot.Repositories.Interfaces;
using ChatBot.Repositories.Models;
using Telegram.Bot;

namespace ChatBot.Commands
{
    public class SystemCommand : IBotCommand
    {
        private readonly IChatModelRepository _chatModelRepository;

        public SystemCommand(IChatModelRepository chatModelRepository)
        {
            _chatModelRepository = chatModelRepository;
        }

        public string Trigger => "/system";

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
                await bot.SendTextMessageAsync(chatId,
                    "📝 Использование: /system <текст системного сообщения>\n\n" +
                    "Пример:\n" +
                    "/system Ты профессиональный переводчик. Отвечай только на русском языке.\n\n" +
                    "Системные сообщения задают поведение бота и сохраняются в истории.");
                return;
            }

            var systemMessage = parts[1].Trim();

            await _chatModelRepository.AddMessageAsync(chatId, new OpenApiResponse.Message
            {
                Role = "system",
                Content = systemMessage
            });

            await bot.SendTextMessageAsync(chatId, $"✅ Системное сообщение добавлено:\n\n{systemMessage}");
        }
    }
}