using Dock.Model.Controls;
using Dock.Model.Core;
using Dock.Model.Mvvm;
using Dock.Model.Mvvm.Controls;

namespace dock_tutorial2.ViewModels;

public class FileDockFactory:Factory
{
    public DocumentDock DocumentDocks { get; set; }
    public ToolDock ToolDocks { get; set; }

    public override IRootDock CreateLayout()
    {
        DocumentDocks = new DocumentDock()
        {
            Id = "MainDocs",
            Title = "Docs",
            CanCreateDocument = false,
            IsCollapsable = false,
            VisibleDockables = CreateList<IDockable>()
        };
        
        //tool
        ToolDocks = new ToolDock
        {
            Id = "ToolDock",
            Title = "Tools",
            Alignment = Alignment.Left,
            Proportion = 0.2,
            VisibleDockables = CreateList<IDockable>()
        };

        var mainLayout = new ProportionalDock
        {
            Id = "Mainlayout",
            Orientation = Orientation.Horizontal,
            VisibleDockables = CreateList<IDockable>(ToolDocks, new ProportionalDockSplitter(), DocumentDocks),
            ActiveDockable = DocumentDocks
        };

        var root = CreateRootDock();
        root.Id = "Root";
        root.VisibleDockables = CreateList<IDockable>(mainLayout);
        root.DefaultDockable = mainLayout;
        root.ActiveDockable = mainLayout;
        
        return root;
    }
}