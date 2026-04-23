namespace ChatBot.Settings
{
    public class ChatApiSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string DefaultModel { get; set; } = "nvidia/nemotron-3-super-120b-a12b:free";
        public int MaxTokens { get; set; } = 1000;  
        public double Temperature { get; set; } = 0.7;  
    }
}