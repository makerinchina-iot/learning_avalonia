using Avalonia.Controls;
using dock_tutorial.ViewModels;

namespace dock_tutorial.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new MainWindowViewModel(storageProvider: StorageProvider);
    }
}