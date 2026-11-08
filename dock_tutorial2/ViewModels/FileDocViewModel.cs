using CommunityToolkit.Mvvm.ComponentModel;
using Dock.Model.Mvvm.Controls;

namespace dock_tutorial2.ViewModels;

public partial class FileDocViewModel:Document
{
    [ObservableProperty] private string _filePath;
    [ObservableProperty] private string _fileContent;
}