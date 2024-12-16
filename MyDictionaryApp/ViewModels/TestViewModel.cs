using System;
using MyDictionaryApp.Models;
using MyDictionaryApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;


namespace MyDictionaryApp.ViewModels;



    public class TestViewModel : ViewModelBase
    {
        private readonly ApiService _apiService;
        private List<WordDto> _testWords;
        private WordDto _currentWord;
        private string _userAnswer;
        private string _resultMessage;

        // Свойства для привязки
        public WordDto CurrentWord
        {
            get => _currentWord;
            set => SetProperty(ref _currentWord, value);
        }

        public string UserAnswer
        {
            get => _userAnswer;
            set => SetProperty(ref _userAnswer, value);
        }

        public string ResultMessage
        {
            get => _resultMessage;
            set => SetProperty(ref _resultMessage, value);
        }

        // Команды
        public ICommand CheckAnswerCommand { get; }
        public ICommand FinishTestCommand { get; }

        public TestViewModel(ApiService apiService)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            CheckAnswerCommand = new RelayCommand(CheckAnswer);
            FinishTestCommand = new RelayCommand(async () => await FinishTestAsync());
            LoadTestWordsAsync();
        }

        private async Task LoadTestWordsAsync()
        {
            var words = await _apiService.GetAllWordsAsync();
            _testWords = PrepareTestWords(words.ToList());
            NextWord();
        }

        private void CheckAnswer()
        {
            if (string.Equals(UserAnswer, CurrentWord.Meaning, StringComparison.OrdinalIgnoreCase))
            {
                ResultMessageColor = "Green";
                ResultMessage = "Правильно!";
                if (CurrentWord.Level < 3)
                {
                    CurrentWord.Level++; // Увеличиваем уровень
                }
            }
            else
            {
                ResultMessageColor = "Red";
                ResultMessage = $"Неверно! Правильный ответ: {CurrentWord.Meaning}";
                if (CurrentWord.Level > 1)
                {
                    CurrentWord.Level--; // Увеличиваем уровень
                }
            }
            
            _apiService.UpdateWordAsync(CurrentWord.Id, CurrentWord);

            UserAnswer = string.Empty; // Сбрасываем поле для ответа
            NextWord();
        }

        private void NextWord()
        {
            if (_testWords.Any())
            {
                CurrentWord = _testWords.First();
                _testWords.RemoveAt(0);
            }
            else
            {
                ResultMessage = "Тест завершен!";
                CurrentWord = null;
            }
        }

        private async Task FinishTestAsync()
        {
            // Отправляем измененные слова обратно на сервер
            foreach (var word in _testWords.Where(w => w.Level > 0))
            {
                await _apiService.UpdateWordAsync(word.Id, word);
            }

            // Завершаем тест
            ResultMessage = "Изменения сохранены. Тестирование завершено.";
        }

        private string _resultMessageColor;

        public string ResultMessageColor
        {
            get => _resultMessageColor;
            set => SetProperty(ref _resultMessageColor, value);
        }

        private List<WordDto> PrepareTestWords(List<WordDto> words)
        {
            var testWords = new List<WordDto>();

            foreach (var word in words)
            {
                var frequency = 4 - word.Level; // Уровень 1 — добавляется 3 раза, 2 — 2 раза, 3 — 1 раз
                for (int i = 0; i < frequency; i++)
                {
                    testWords.Add(word);
                }
            }

            return testWords.OrderBy(_ => Guid.NewGuid()).ToList(); // Перемешиваем
        }

    }
