using System.Text.Json;

public class JsonScheduleRepository : IScheduleRepository
{
    private readonly string _path;
    private readonly JsonSerializerOptions _opts = new() { PropertyNameCaseInsensitive = true };

    public JsonScheduleRepository(string path)
    {
        _path = path;
        // Создаем пустой файл, если его нет (без стандартного расписания)
        if (!File.Exists(_path))
        {
            var empty = new ScheduleFile();
            File.WriteAllText(_path, JsonSerializer.Serialize(empty, new JsonSerializerOptions { WriteIndented = true }));
        }
    }

    public ScheduleFile Load()
    {
        try
        {
            using var s = File.OpenRead(_path);
            return JsonSerializer.Deserialize<ScheduleFile>(s, _opts) ?? new ScheduleFile();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке расписания: {ex.Message}");
            return new ScheduleFile();
        }
    }

    public void Save(ScheduleFile schedule)
    {
        try
        {
            var json = JsonSerializer.Serialize(schedule, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_path, json);
            Console.WriteLine("Расписание сохранено");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении расписания: {ex.Message}");
            throw;
        }
    }
}