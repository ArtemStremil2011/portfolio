using ChatBot.Repositories.Interfaces;
using ChatBot.Repositories.Models;
using System.Collections.Concurrent;

namespace ChatBot.Repositories.Implementations
{
    public class ChatModelRepository : IChatModelRepository
    {
        private readonly ConcurrentDictionary<long, List<OpenApiResponse.Message>> _store = new();
        private readonly ConcurrentDictionary<long, string> _userModels = new();
        private const string DEFAULT_MODEL = "openrouter/hunter-alpha";

        public Task AddMessageAsync(long chatId, OpenApiResponse.Message message)
        {
            var list = _store.GetOrAdd(chatId, _ => new List<OpenApiResponse.Message>());
            lock (list)
            {
                list.Add(message);
            }
            return Task.CompletedTask;
        }

        public Task<List<OpenApiResponse.Message>> GetHistoryAsync(long chatId)
        {
            _store.TryGetValue(chatId, out var list);
            return Task.FromResult(list == null ? new List<OpenApiResponse.Message>() : new List<OpenApiResponse.Message>(list));
        }

        public Task<string> GetCurrentModelAsync(long chatId)
        {
            var model = _userModels.GetValueOrDefault(chatId);
            if (string.IsNullOrEmpty(model))
            {
                return Task.FromResult(DEFAULT_MODEL);
            }
            return Task.FromResult(model);
        }

        public Task SetCurrentModelAsync(long chatId, string model)
        {
            _userModels[chatId] = model;
            return Task.CompletedTask;
        }
    }
}