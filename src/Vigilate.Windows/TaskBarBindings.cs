using System.ComponentModel;

namespace Vigilate.Core.Windows;
/// <summary>
/// TaskBar bindings for windows.
/// </summary>
public class TaskBarBindings : INotifyPropertyChanged, ITaskBar
{
    /// <summary>
    /// Create new task bar bindings.
    /// </summary>
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
