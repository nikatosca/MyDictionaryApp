using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using MyDictionaryApp.Models;
using MyDictionaryApp.Services;

namespace MyDictionaryApp.ViewModels;

public class AddWordViewModel : ViewModelBase
{
    private readonly ApiService _apiService;

    public AddWordViewModel(ApiService apiService)
    {
        _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
        AddWordCommand = new RelayCommand(async () => await AddWordAsync());
    }

    public ICommand AddWordCommand { get; }


    private int _id;
    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    private string _original;
    public string Original
    {
        get => _original;
        set => SetProperty(ref _original, value);
    }

    private string _meaning;
    public string Meaning
    {
        get => _meaning;
        set => SetProperty(ref _meaning, value);
    }

    private int _level;
    public int Level
    {
        get => _level;
        set => SetProperty(ref _level, value);
    }
    
    private string _resultMessage;
    public string ResultMessage
    {
        get => _resultMessage;
        set => SetProperty(ref _resultMessage, value);
    }
    
    private string _resultMessageColor;
    public string ResultMessageColor
    {
        get => _resultMessageColor;
        set => SetProperty(ref _resultMessageColor, value);
    }
    public async Task AddWordAsync()
    {
        try
        {
            var newWord = new WordDto
            {
                Id = Id,
                Original = Original,
                Meaning = Meaning,
                Level = Level
            };

            await _apiService.AddNewWordAsync(newWord);
            ResultMessageColor = "Green";
          //  var allWordsViewModel = ServiceProvider.GetRequiredService<AllWordsViewModel>();
        //   allWordsViewModel.Words.Add(newWord);
            ResultMessage = $"Слово '{newWord.Original}' успешно добавлено.";
        }
        catch (HttpRequestException httpEx)
        {
            // Это исключение будет выброшено, если HTTP запрос не успешен
            ResultMessage = $"Ошибка при добавлении слова (HTTP): {httpEx.Message}";
            ResultMessageColor = "Red";
        }
        catch (Exception ex)
        {
            ResultMessageColor = "Red";
            // Это будет ловить другие исключения
            ResultMessage = $"Ошибка при добавлении  слова: {ex.Message}";
        }
    }
}