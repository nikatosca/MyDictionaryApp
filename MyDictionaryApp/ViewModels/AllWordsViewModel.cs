using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MyDictionaryApp.Models;
using MyDictionaryApp.Services;

namespace MyDictionaryApp.ViewModels
{
    public class AllWordsViewModel : ViewModelBase
    {
        private readonly ApiService _apiService;

        private ObservableCollection<WordDto> _words;
        public ObservableCollection<WordDto> Words
        {
            get => _words;
            set => SetProperty(ref _words, value);
        }

        public ICommand RefreshWordsCommand { get; }

        public AllWordsViewModel(ApiService apiService)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            Words = new ObservableCollection<WordDto>();
            RefreshWordsCommand = new RelayCommand(async () => await LoadWordsAsync());
            _ = LoadWordsAsync();
        }

        private async Task LoadWordsAsync()
        {
            try
            {
                var wordsFromApi = await _apiService.GetAllWordsAsync();
                Words = new ObservableCollection<WordDto>(wordsFromApi);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
                Words = new ObservableCollection<WordDto> { new WordDto { Original = "Ошибка", Meaning = "Не удалось загрузить данные" } };
            }
        }
    }
}