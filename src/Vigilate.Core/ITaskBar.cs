using System.ComponentModel;

namespace Vigilate.Core;
/// <summary>
/// Interface specification for the taskbar icon context menu.
/// </summary>
public interface ITaskBar
{
    /// <summary>
    /// Exit App button text.
    /// </summary>
    public string ExitApp { get; set; }
    /// <summary>
    /// Show window button text.
    /// </summary>
    public string ShowWindow { get; set; }
    /// <summary>
    /// Start / stop button text.
    /// </summary>
    public string StartStop { get; set; }
    /// <summary>
    /// Event triggered when property is changed.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;
}