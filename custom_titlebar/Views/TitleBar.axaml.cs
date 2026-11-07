using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;

namespace custom_titlebar.Views;

public partial class TitleBar : UserControl
{
    private Window? _window;
    
    public TitleBar()
    {
        InitializeComponent();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        
        _window = TopLevel.GetTopLevel(this) as Window;
    }
    
    private void OnCloseButtonClick(object sender, RoutedEventArgs e)
    {
        e.Handled = true;
        _window?.Close();
    }

    private void OnSearchPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        e.Handled = true;
    }
    
    private bool HitsInteractive(object? source)
    {
        if (source is not Visual v) return false;
        Visual? cur = v;
        while (cur is not null && !ReferenceEquals(cur, this))
        {
            if (cur is MenuItem or Menu or Button or TextBox or ComboBox)
                return true;
            cur = cur.GetVisualParent();
        }
        return false;
    }

    private void OnTitleBarPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (HitsInteractive(e.Source)) return;
        
        if ((e.ClickCount == 1) && (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed))
        {
            _window?.BeginMoveDrag(e);
        }
    }
}