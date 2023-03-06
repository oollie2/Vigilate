using NLog;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Vigilate.Core
{
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
            AVigilator.TaskBarBindings = Bindings;
            AWindow = windowToMinimise;
            Init();
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
        }
        private void ToolStripStartStop_Click(object sender, RoutedEventArgs e)
        {
            if(Settings<VigilateSettings>.Main.State)
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
            Application.Current.Shutdown();
        }
    }
    public class TaskBarBindings: INotifyPropertyChanged
    {
        public TaskBarBindings()
        {
            TaskBarValues = new();
        }
        private readonly TaskBarValues TaskBarValues;
        public string ShowWindow
        {
            get { return TaskBarValues.ShowWindow; }
            set { TaskBarValues.ShowWindow = value; OnPropertyChanged(nameof(ShowWindow)); }
        }
        public string ExitApp
        {
            get { return TaskBarValues.ExitApp; }
            set { TaskBarValues.ExitApp = value; OnPropertyChanged(nameof(ExitApp)); }
        }
        public string StartStop
        {
            get { return TaskBarValues.StartStop; }
            set { TaskBarValues.StartStop = value; OnPropertyChanged(nameof(StartStop)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    sealed class TaskBarValues
    {
        public string ShowWindow { get; set; } = "Show";
        public string ExitApp { get; set; } = "Exit";
        public string StartStop { get; set; } = "Start";
    }
}
