using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw13
{
    namespace ChatBot.Settings
    {
        public static class ChatApiSettings
        {
            public static string BaseUrl { get; } = "https://openrouter.ai/api/v1/chat/completions";
            public static string ApiKey { get; } = "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";
            public static string DefaultModel { get; } = "google/gemma-3-1b-it:free";

        }
    }
}
