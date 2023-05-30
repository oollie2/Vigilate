namespace Vigilate.Core;
/// <summary>
/// Interface specification for a Vigilator class which keeps the operating system alive.
/// </summary>
public interface IVigilator
{
    /// <summary>
    /// Triggered when the vigilator state has changed. If contains true, the vigilator is running.
    /// </summary>
    public event Action<bool> StateChange;
    /// <summary>
    /// Stop the OS from sleeping.
    /// </summary>
    public void NoSleep();
    /// <summary>
    /// Allow the OS to sleep.
    /// </summary>
    public void SomeSleep();
}