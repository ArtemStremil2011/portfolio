using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

    [HttpPost]
    public List<string> AddWord(string world)
    {
        words.Add(world);
        return words;
    }

    [HttpDelete]
    public List<string> DeleteWord(int index)
    {
        words.RemoveAt(index);
        return words;
    }
}