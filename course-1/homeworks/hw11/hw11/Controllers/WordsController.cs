using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class WordsController : ControllerBase
{
    private static List<string> words = new List<string>
    {
        "apple",
        "banana",
        "cherry",
        "date",
        "elderberry",
        "fig",
        "grape"
    };

    [HttpGet]
    public List<string> GetAllWords()
    {
        return words;
    }

    [HttpGet("item")] // GET: api/words/item?index=3
    public string GetByIndex([FromQuery] int index)
    {
        if (index < 0 || index >= words.Count)
            return "Index out of range";

        return words[index];
    }

    [HttpPost]
    public List<string> AddWord([FromQuery] string word)
    {
        words.Add(word);
        return words;
    }

    [HttpDelete]
    public List<string> DeleteWord([FromQuery] int index)
    {
        if (index < 0 || index >= words.Count)
            return words;

        words.RemoveAt(index);
        return words;
    }

    [HttpPut]
    public List<string> EditWord([FromQuery] int index, [FromQuery] string new_word)
    {
        if (index < 0 || index >= words.Count)
            return words;

        words[index] = new_word;
        return words;
    }
}