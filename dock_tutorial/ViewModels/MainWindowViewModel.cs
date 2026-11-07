using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using dock_tutorial.Views;

namespace dock_tutorial.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<ViewModelBase> DocksPannels { get; set; } = new();

    private IStorageProvider _storageProvider;

    public MainWindowViewModel()
    {
            
    }

    public MainWindowViewModel(IStorageProvider storageProvider) : this()
    {
        _storageProvider = storageProvider;
    }

    [RelayCommand]
    private async Task OpenFileDocument()
    {
        var files = await _storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions()
        {
            Title = "打开文本文件",
            AllowMultiple = false,
        });
        
        if (files.Count == 0) return;

        await using var stream = await files[0].OpenReadAsync(); 
        using var reader = new StreamReader(stream);
        var fileName = files[0].Name;
        var filePath = files[0].Path.AbsolutePath;
        var fileContent = await reader.ReadToEndAsync();
        
        DocksPannels.Add(new FileDocViewModel()
        {
            Title = fileName,
            FilePath = filePath,
            Content = fileContent
        });
    }

    [RelayCommand]
    private async Task OpenImgDocument()
    {
        var files = await _storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions()
        {
            Title = "打开图片",
            AllowMultiple = false,
        });
        
        if (files.Count == 0) return;

        await using var stream = await files[0].OpenReadAsync(); 
        var fileName = files[0].Name;
        var filePath = files[0].Path.AbsolutePath;
        var bitMapStream = new Bitmap(stream);
        
        DocksPannels.Add(new ImgDocViewModel()
        {
            Title = fileName,
            FilePath = filePath,
            Bitmap = bitMapStream
        });
    }

}