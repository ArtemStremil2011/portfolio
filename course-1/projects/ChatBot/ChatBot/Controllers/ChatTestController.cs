using ChatBot.Commands;
using ChatBot.Dtos;
using ChatBot.Repositories.Interfaces;
using ChatBot.Repositories.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatBot.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatTestController : ControllerBase
    {
        private readonly IChatApiClient _chatApiClient;
        private readonly TelegramUpdateProcessor _processor;
        private readonly IChatModelRepository _repository;

        public ChatTestController(
            IChatApiClient chatApiClient,
            TelegramUpdateProcessor processor,
            IChatModelRepository repository)
        {
            _chatApiClient = chatApiClient;
            _processor = processor;
            _repository = repository;
        }

        [HttpPost("test")]
        public async Task<IActionResult> Test([FromBody] ChatTestRequest request)
        {
            var answer = await _chatApiClient.SendMessageAsync(
                request.Message,
                new List<OpenApiResponse.Message>
                {
                    new OpenApiResponse.Message
                    {
                        Role = "user",
                        Content = request.Message
                    }
                });

            return Ok(new { answer });
        }

        [HttpPost("bot")]
        public async Task<IActionResult> TestBot([FromBody] ChatTestRequest request)
        {
            var chatId = 123456789L;

            var fakeUpdate = new TelegramUpdate
            {
                UpdateId = new Random().Next(1, 999999),
                Message = new TelegramMessage
                {
                    MessageId = new Random().Next(1, 999999),
                    Chat = new DTO.TelegramChat { Id = chatId },
                    Text = request.Message,
                    Date = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                }
            };

            await _processor.HandleAsync(fakeUpdate);

            return Ok(new { message = "Сообщение обработано ботом", chatId });
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            var history = await _repository.GetHistoryAsync(123456789L);
            return Ok(history);
        }

        [HttpDelete("history")]
        public async Task<IActionResult> ClearHistory()
        {
            await _repository.ClearHistoryAsync(123456789L);
            return Ok(new { message = "История очищена" });
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var stats = await _repository.GetStatsAsync(123456789L);
            return Ok(stats);
        }
    }

    public class ChatTestRequest
    {
        public string Message { get; set; } = string.Empty;
    }
}