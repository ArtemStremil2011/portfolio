using ChatBot.Repositories.Models;

namespace ChatBot.Repositories.Interfaces
{
    public interface IChatModelRepository
    {
        Task<List<OpenApiResponse.Message>> GetHistoryAsync(long chatId);
        Task AddMessageAsync(long chatId, OpenApiResponse.Message message);

        Task<string> GetCurrentModelAsync(long chatId);
        Task SetCurrentModelAsync(long chatId, string model);
    }
}