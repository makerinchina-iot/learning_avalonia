using Avalonia;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.Input;

namespace custom_titlebar.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [RelayCommand]
    private void ToggleTheme()
    {
        Application.Current?.RequestedThemeVariant = Application.Current.ActualThemeVariant == ThemeVariant.Dark
            ? ThemeVariant.Light
            : ThemeVariant.Dark;
    }
}