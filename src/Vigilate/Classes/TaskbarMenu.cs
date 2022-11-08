using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using VigilateUI;

namespace Vigilate.Classes
{
    internal class TaskbarMenu : ContextMenu
    {
        private TaskBarBindings bindings;
        private Vigilator vigilator;
        internal TaskbarMenu(Vigilator _vigilator)
        {
            bindings = new();
            vigilator = _vigilator;
            vigilator.TaskBarBindings = bindings;
            Init();
        }
        private void Init()
        {
            MenuItem toolStripShow = new();
            toolStripShow.SetBinding(HeaderedItemsControl.HeaderProperty, new Binding()
            {
                Path = new System.Windows.PropertyPath("ShowWindow"),
                Source = bindings,
            });
            toolStripShow.Click += ToolStripShow_Click;

            MenuItem toolStripHide = new();
            toolStripHide.SetBinding(HeaderedItemsControl.HeaderProperty, new Binding()
            {
                Path = new System.Windows.PropertyPath("ExitApp"),
                Source = bindings,
            });
            toolStripHide.Click += ToolStripHide_Click;

            MenuItem toolStripStartStop = new();
            toolStripStartStop.SetBinding(HeaderedItemsControl.HeaderProperty, new Binding()
            {
                Path = new System.Windows.PropertyPath("StartStop"),
                Source = bindings,
            });
            toolStripStartStop.Click += ToolStripStartStop_Click;

            Items.Add(toolStripShow);
            Items.Add(toolStripStartStop);
            Items.Add(toolStripHide);
        }
        private void ToolStripStartStop_Click(object sender, RoutedEventArgs e)
        {
            if(Settings.Main.State)
            {
                vigilator.SomeSleep();
            }
            else
            {
                vigilator.NoSleep();
            }
        }
        private void ToolStripShow_Click(object sender, RoutedEventArgs e)
        {
            if (App.MainWin.WindowState != WindowState.Minimized)
                App.MainWin.Show();
        }
        private void ToolStripHide_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
    sealed class TaskBarBindings: INotifyPropertyChanged
    {
        public TaskBarBindings()
        {
            TaskBarValues = new();
        }
        private TaskBarValues TaskBarValues;
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
