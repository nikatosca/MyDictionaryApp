using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using MyDictionaryApp.Services;
using MyDictionaryApp.ViewModels;

public class DeleteWordViewModel : ViewModelBase
{
    private readonly ApiService _apiService;

    private string _wordToDelete;
    public string WordToDelete
    {
        get => _wordToDelete;
        set => SetProperty(ref _wordToDelete, value);
    }

    private string _resultMessage;
    public string ResultMessage
    {
        get => _resultMessage;
        set => SetProperty(ref _resultMessage, value);
    }

    public ICommand DeleteWordCommand { get; }

    public DeleteWordViewModel(ApiService apiService)
    {
        _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
        DeleteWordCommand = new RelayCommand(async () => await DeleteWordAsync());
    }
    private string _resultMessageColor;
    public string ResultMessageColor
    {
        get => _resultMessageColor;
        set => SetProperty(ref _resultMessageColor, value);
    }
    private async Task DeleteWordAsync()
    {
        if (string.IsNullOrWhiteSpace(WordToDelete))
        {
            ResultMessage = "Введите оригинал слова.";
            return;
        }

        try
        {
            // Вызываем метод API для удаления слова
            await _apiService.DeleteWordAsync(WordToDelete);

            // Если успешный ответ, обновляем сообщение
            ResultMessageColor = "Green";
            ResultMessage = $"Слово '{WordToDelete}' успешно удалено.";
            WordToDelete = string.Empty; // Очистить поле после успешного удаления
        }
        catch (HttpRequestException httpEx)
        {
            ResultMessageColor = "Red";
            // Это исключение будет выброшено, если HTTP запрос не успешен
            ResultMessage = $"Ошибка при удалении слова (HTTP): {httpEx.Message}";
        }
        catch (Exception ex)
        {
            ResultMessageColor = "Red";
            // Это будет ловить другие исключения
            ResultMessage = $"Ошибка при удалении слова: {ex.Message}";
        }
    }

}