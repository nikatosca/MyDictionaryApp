using System;
using System.Windows.Input;


using System;
using System.Windows.Input;

namespace MyDictionaryApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    // Текущая ViewModel для отображения
    private ViewModelBase _currentViewModel;
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set => SetProperty(ref _currentViewModel, value);
    }

    // Команды для кнопок
    public ICommand ShowAllWordsCommand { get; }
    public ICommand AddWordCommand { get; }
    public ICommand DeleteWordCommand { get; }
    public ICommand StartTestingCommand { get; }

    // Конструктор с параметрами
    public MainWindowViewModel(
        AllWordsViewModel allWordsViewModel,
        AddWordViewModel addWordViewModel,
        DeleteWordViewModel deleteWordViewModel,
        TestViewModel testingViewModel,
        WelcomeViewModel welcomeViewModel)
    {
        // Инициализация ViewModel для каждого раздела
        _currentViewModel = welcomeViewModel;
        
        ShowAllWordsCommand = new RelayCommand(() => CurrentViewModel = allWordsViewModel);
        AddWordCommand = new RelayCommand(() => CurrentViewModel = addWordViewModel);
        DeleteWordCommand = new RelayCommand(() => CurrentViewModel = deleteWordViewModel);
        StartTestingCommand = new RelayCommand(() => CurrentViewModel = testingViewModel);
    }
}






/*public class MainWindowViewModel : ViewModelBase
{
    
    // Текущая ViewModel для отображения
    private ViewModelBase _currentViewModel;
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set => SetProperty(ref _currentViewModel, value);
    }

    
    // Другие ViewModel для разделов
    private readonly ViewModelBase _allWordsViewModel;
    private readonly ViewModelBase _addWordViewModel;
    private readonly ViewModelBase _deleteWordViewModel;
    private readonly ViewModelBase _testingViewModel;

    // Команды для кнопок
    public ICommand ShowAllWordsCommand { get; }
    public ICommand AddWordCommand { get; }
    public ICommand DeleteWordCommand { get; }
    public ICommand StartTestingCommand { get; }

    // Конструктор с параметрами
    public MainWindowViewModel(
        AllWordsViewModel allWordsViewModel,
        AddWordViewModel addWordViewModel,
        DeleteWordViewModel deleteWordViewModel,
        TestViewModel testingViewModel,
        WelcomeViewModel welcomeViewModel)
    {
        // Инициализация ViewModel для каждого раздела
        _currentViewModel = welcomeViewModel;
        
        ShowAllWordsCommand = new RelayCommand(() => CurrentViewModel = allWordsViewModel);
        AddWordCommand = new RelayCommand(() => CurrentViewModel = addWordViewModel);
        DeleteWordCommand = new RelayCommand(() => CurrentViewModel = deleteWordViewModel);
        StartTestingCommand = new RelayCommand(() => CurrentViewModel = testingViewModel);
    }
}*/
