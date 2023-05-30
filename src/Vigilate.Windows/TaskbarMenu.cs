using NLog;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Vigilate.Core.Windows;

public class TaskbarMenu : ContextMenu
{
    internal static ILogger _logger = LogManager.GetCurrentClassLogger();
    private TaskBarBindings Bindings { get; set; }
    private Vigilator AVigilator { get; set; }
    private Window AWindow { get; set; }
    public TaskbarMenu(Vigilator vigilator, Window windowToMinimise)
    {
        Bindings = new();
        AVigilator = vigilator;
        AVigilator.StateChange += AVigilator_StateChange;
        AWindow = windowToMinimise;
        Init();
    }

    private void AVigilator_StateChange(bool obj)
    {
        if (obj)
            Bindings.StartStop = "Stop";
        else
            Bindings.StartStop = "Start";
    }

    private void Init()
    {
        _logger.Info("initialising the taskbar functions.");
        MenuItem toolStripShow = new();
        toolStripShow.SetBinding(HeaderedItemsControl.HeaderProperty, new Binding()
        {
            Path = new PropertyPath("ShowWindow"),
            Source = Bindings,
        });
        toolStripShow.Click += ToolStripShow_Click;

        MenuItem toolStripHide = new();
        toolStripHide.SetBinding(HeaderedItemsControl.HeaderProperty, new Binding()
        {
            Path = new PropertyPath("ExitApp"),
            Source = Bindings,
        });
        toolStripHide.Click += ToolStripHide_Click;

        MenuItem toolStripStartStop = new();
        toolStripStartStop.SetBinding(HeaderedItemsControl.HeaderProperty, new Binding()
        {
            Path = new PropertyPath("StartStop"),
            Source = Bindings,
        });
        toolStripStartStop.Click += ToolStripStartStop_Click;

        Items.Add(toolStripShow);
        Items.Add(toolStripStartStop);
        Items.Add(toolStripHide);

        _logger.Info("all taskbar functions added.");
    }
    private void ToolStripStartStop_Click(object sender, RoutedEventArgs e)
    {
        if (Settings<VigilateSettings>.Main.State)
        {
            _logger.Info("taskbar button clicked to disable vigilate");
            AVigilator.SomeSleep();
        }
        else
        {
            _logger.Info("taskbar button clicked to enable vigilate");
            AVigilator.NoSleep();
        }
    }
    private void ToolStripShow_Click(object sender, RoutedEventArgs e)
    {
        if (AWindow.WindowState != WindowState.Minimized)
            AWindow.Show();
    }
    private void ToolStripHide_Click(object sender, RoutedEventArgs e)
    {
        _logger.Info($"shutting down {sender}.");
        Application.Current.Shutdown();
    }
}
