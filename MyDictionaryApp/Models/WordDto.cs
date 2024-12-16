using System.Text.Json.Serialization;

namespace MyDictionaryApp.Models;

public class WordDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("original")]
    public string Original { get; set; }  

    [JsonPropertyName("meaning")]
    public string Meaning { get; set; }
    [JsonPropertyName("level")]
    public int Level { get; set; }       // Уровень сложности
}
