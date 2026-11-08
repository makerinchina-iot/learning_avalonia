using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using dock_tutorial2.Models;
using Dock.Model.Mvvm.Controls;

namespace dock_tutorial2.ViewModels;

public partial class FileMangerViewModel:Tool
{
    public ObservableCollection<FileNode> Files { get; set; } = new();

    [ObservableProperty] private FileNode? _selectedItem;

    private readonly Func<string, Task> _openFileAsync;

    public FileMangerViewModel(Func<string, Task> openFileAsync)
    {
        _openFileAsync = openFileAsync;
    }

    partial void OnSelectedItemChanged(FileNode? value)
    {
        if (value is null) return;

        _openFileAsync(value.Path);
    }
}