using Avalonia.Controls;
using dock_tutorial2.ViewModels;

namespace dock_tutorial2.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new MainWindowViewModel(StorageProvider);
    }
}