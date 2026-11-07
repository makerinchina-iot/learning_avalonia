using CommunityToolkit.Mvvm.ComponentModel;

namespace dock_tutorial.ViewModels;

public partial class FileDocViewModel:ViewModelBase
{
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _filePath;
    [ObservableProperty] private string _content;
}