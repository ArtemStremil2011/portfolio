using System.ComponentModel.DataAnnotations;
using Telegram.Bot.Types;
using ChatBot.DTO;

namespace ChatBot.Dtos
{
    public class TelegramMessage
    {
        [Required]
        public int MessageId { get; set; }
        public TelegramChat Chat { get; set; } = new TelegramChat();
        [Required]
        [MinLength(2)]
        [MaxLength(200)]
        public string? Text { get; set; }
        [Required]
        public int Date { get; set; }
    }
}
