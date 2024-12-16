using Avalonia.Controls;


using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MyDictionaryApp.ViewModels;

namespace MyDictionaryApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel; // Привязка ViewModel к окну
        }

     private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
