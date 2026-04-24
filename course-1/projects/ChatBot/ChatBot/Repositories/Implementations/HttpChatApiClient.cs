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

            _httpClient.BaseAddress = new Uri("https://openrouter.ai/api/v1/");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _chatSettings.ApiKey);

            _httpClient.DefaultRequestHeaders.Add("HTTP-Referer", "http://localhost:5000");
            _httpClient.DefaultRequestHeaders.Add("X-Title", "Telegram Bot");
        }

        public async Task<string> SendMessageAsync(string userMessage, IEnumerable<OpenApiResponse.Message> history)
        {
            var payload = new OpenApiRequest()
            {
                Model = _chatSettings.DefaultModel,
                Messages = history.ToList(),
                MaxTokens = _chatSettings.MaxTokens
            };

            var response = await _httpClient.PostAsJsonAsync("chat/completions", payload);

            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadFromJsonAsync<OpenApiResponse?>();

            if (body.Choices != null && body.Choices.Length > 0)
            {
                return body.Choices[0].Message.Content;
            }

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> SendMessageAsync(string message)
        {
            return await SendMessageAsync(message, new List<OpenApiResponse.Message>());
        }
    }
}