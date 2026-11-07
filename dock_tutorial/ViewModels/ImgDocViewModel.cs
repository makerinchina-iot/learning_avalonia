
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace dock_tutorial.ViewModels;

public partial class ImgDocViewModel:ViewModelBase
{
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _filePath;
    [ObservableProperty] private Bitmap _bitmap;
}