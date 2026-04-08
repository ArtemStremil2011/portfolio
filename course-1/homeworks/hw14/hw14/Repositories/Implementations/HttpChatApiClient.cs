using ChatBot.Repositories.Interfaces;
using ChatBot.Repositories.Models;
using ChatBot.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ChatBot.Repositories.Implementations
{
    public class HttpChatApiClient : IChatApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ChatApiSettings _chatSettings;

        public HttpChatApiClient(HttpClient httpClient, IOptions<ChatApiSettings> chatOptions)
        {
            _chatSettings = chatOptions.Value;
            _httpClient = httpClient;

            // Устанавливаем базовый URL
            _httpClient.BaseAddress = new Uri(_chatSettings.BaseUrl);

            // Авторизация
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _chatSettings.ApiKey);

            // Обязательные заголовки для OpenRouter
            _httpClient.DefaultRequestHeaders.Add("HTTP-Referer", "http://localhost:5000");
            _httpClient.DefaultRequestHeaders.Add("X-Title", "Telegram Bot");
        }

        public async Task<string> SendMessageAsync(string userMessage, IEnumerable<OpenApiResponse.Message> history)
        {
            // Формируем список сообщений, включая новое
            var messages = new List<object>();

            // Добавляем историю
            foreach (var msg in history)
            {
                messages.Add(new { role = msg.Role, content = msg.Content });
            }

            // Добавляем новое сообщение пользователя
            messages.Add(new { role = "user", content = userMessage });

            // Создаем payload для OpenRouter
            var payload = new
            {
                model = _chatSettings.DefaultModel,
                messages = messages,
                max_tokens = _chatSettings.MaxTokens,
                temperature = _chatSettings.Temperature
            };

            try
            {
                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Отправляем запрос (путь зависит от BaseUrl)
                string endpoint = _chatSettings.BaseUrl.EndsWith("/v1") ? "chat/completions" : "v1/chat/completions";
                var response = await _httpClient.PostAsync(endpoint, content);

                var responseText = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return $"Ошибка API ({response.StatusCode}): {responseText}";
                }

                var doc = JsonDocument.Parse(responseText);
                var root = doc.RootElement;

                if (root.TryGetProperty("choices", out var choices) && choices.GetArrayLength() > 0)
                {
                    var choice = choices[0];
                    if (choice.TryGetProperty("message", out var messageObj) &&
                        messageObj.TryGetProperty("content", out var contentProp))
                    {
                        return contentProp.GetString() ?? "Нет ответа";
                    }
                }

                return "Не удалось получить ответ от AI";
            }
            catch (Exception ex)
            {
                return $"Ошибка: {ex.Message}";
            }
        }

        public async Task<string> SendMessageAsync(string message)
        {
            return await SendMessageAsync(message, new List<OpenApiResponse.Message>());
        }
    }
}