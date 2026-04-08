namespace ChatBot.Settings
{
    public class ChatApiSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;  
        public string DefaultModel { get; set; } = "openrouter/hunter-alpha";  
        public int MaxTokens { get; set; } = 1000;  
        public double Temperature { get; set; } = 0.7;  
    }
}