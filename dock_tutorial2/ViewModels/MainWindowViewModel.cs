using System.IO;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;
using dock_tutorial2.Models;
using Dock.Model.Controls;
using Dock.Model.Mvvm.Controls;

namespace dock_tutorial2.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public FileDockFactory Factory { get; }
    public IRootDock Layout { get; }

    private IStorageProvider _storageProvider;

    public MainWindowViewModel()
    {
        
    }

    public MainWindowViewModel(IStorageProvider storageProvider):this()
    {
        Factory = new FileDockFactory();
        Layout = Factory.CreateLayout();
        Factory.InitLayout(Layout);

        _storageProvider = storageProvider;
    }

    private void AddDocument(Document doc)
    {
        Factory.DocumentDocks.AddDocument(doc);
    }

    private void AddTool(Tool tool)
    {
        Factory.ToolDocks.AddTool(tool);
    }

    [RelayCommand]
    private async Task OpenFolder()
    {
        var folder = await _storageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions()
        {
            Title = "打开文件夹",
            AllowMultiple = false
        });

        if (folder.Count <= 0) return;

        var tool = new FileMangerViewModel(OpenFileAsync);
        
        foreach (var f in Directory.EnumerateFiles(folder[0].Path.LocalPath))
        {
            var ext = Path.GetExtension(f);
            var isImage = ext.ToLowerInvariant() is ".png" or ".jpg";
            var icon = isImage ? "🪐":"📒";
            
            tool.Files.Add(new FileNode
            {
                Icon = icon,
                Name = Path.GetFileName(f),
                Path = f,
            });
        }

        tool.Title = "Explore";
        tool.IsCollapsable = false;
        
        AddTool(tool);
    }

    private async Task OpenFileDoc(string filePath)
    {
        var text = await File.ReadAllTextAsync(filePath);
        var doc = new FileDocViewModel()
        {
            Title = Path.GetFileName(filePath),
            FilePath = filePath,
            FileContent = text
        };
        
        AddDocument(doc);
    }

    private async Task OpenImgDoc(string filePath)
    {
        var bitmap = new Bitmap(filePath);

        var doc = new ImgDocViewModel()
        {
            Title = Path.GetFileName(filePath),
            FilePath = filePath,
            Bitmap = bitmap,
        };

        AddDocument(doc);
    }

    public async Task OpenFileAsync(string filePath)
    {
        var ext = Path.GetExtension(filePath);
        var isImage = ext.ToLowerInvariant() is ".png" or ".jpg";

        if (isImage)
        {
            await OpenImgDoc(filePath);
        }
        else
        {
            await OpenFileDoc(filePath);
        }

    }

}