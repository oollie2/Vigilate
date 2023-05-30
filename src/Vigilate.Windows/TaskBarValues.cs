namespace Vigilate.Core.Windows;
sealed record TaskBarValues
{
    public string ShowWindow { get; set; } = "Show";
    public string ExitApp { get; set; } = "Exit";
    public string StartStop { get; set; } = "Start";
}
