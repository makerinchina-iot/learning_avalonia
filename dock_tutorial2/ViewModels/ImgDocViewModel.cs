using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using Dock.Model.Mvvm.Controls;

namespace dock_tutorial2.ViewModels;

public partial class ImgDocViewModel:Document
{
    [ObservableProperty] private string _filePath;
    [ObservableProperty] private Bitmap _bitmap;
}