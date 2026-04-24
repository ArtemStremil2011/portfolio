using ChatBot.Dtos;
using ChatBot.Repositories.Interfaces;
using ChatBot.Repositories.Models;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace ChatBot.Commands
{
    public class TelegramUpdateProcessor
    {
        private readonly IEnumerable<IBotCommand> _commands;
        private readonly ITelegramBotClient _botClient;
        private readonly IChatApiClient _chatClient;
        private readonly IChatModelRepository _chatModelRepository;
        private readonly ILogger<TelegramUpdateProcessor> _logger;

        public TelegramUpdateProcessor(
            IEnumerable<IBotCommand> commands,
            ITelegramBotClient botClient,
            IChatApiClient chatClient,
            IChatModelRepository chatModelRepository,
            ILogger<TelegramUpdateProcessor> logger)
        {
            _commands = commands;
            _botClient = botClient;
            _chatClient = chatClient;
            _chatModelRepository = chatModelRepository;
            _logger = logger;
        }

        public async Task HandleAsync(TelegramUpdate update)
        {
            if (update.Message == null)
                return;

            var chatId = update.Message.Chat.Id;
            var text = update.Message.Text.Trim();
            _logger.LogInformation($"Получено сообдение от чата {chatId}: {text}");

            if (text.StartsWith("/"))
            {
                var cmd = text.Split(' ', 2)[0];
                var command = _commands.FirstOrDefault(c => c.Trigger.Equals(cmd, StringComparison.OrdinalIgnoreCase));
                if (command != null)
                {
                    await command.ExecuteAsync(update, _botClient, chatId);
                    return;
                }
                else
                {
                    return;
                }
            }

            await _chatModelRepository.AddMessageAsync(chatId, new OpenApiResponse.Message { Role = "user", Content = text });
            var history = await _chatModelRepository.GetHistoryAsync(chatId);
            try
            {
                var result = await _chatClient.SendMessageAsync(text, history);
                _logger.LogInformation($"Получен ответ {result}");
                await _chatModelRepository.AddMessageAsync(chatId, new OpenApiResponse.Message { Role = "assistant", Content = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при вызове Chat API");
            }
        }
    }
}