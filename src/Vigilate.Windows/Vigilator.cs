using NLog;
using System.Runtime.InteropServices;

namespace Vigilate.Core.Windows;

public partial class Vigilator : IVigilator
{
    internal static ILogger _logger = LogManager.GetCurrentClassLogger();
    public event Action<bool> StateChange;
    public Vigilator() { }
    [LibraryImport("kernel32.dll", StringMarshalling = StringMarshalling.Utf8, SetLastError = true)]
    private static partial EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);
    [Flags]
    private enum EXECUTION_STATE : uint
    {
        ES_SYSTEM_REQUIRED = 0x00000001,
        ES_DISPLAY_REQUIRED = 0x00000002,
        ES_AWAYMODE_REQUIRED = 0x00000040,
        ES_CONTINUOUS = 0x80000000,
    }
    private readonly AutoResetEvent _event = new(false);
    public async void NoSleep()
    {
        Settings<VigilateSettings>.Main.State = true;
        StateChange?.Invoke(true);
        await new TaskFactory().StartNew(async () =>
        {
            while (Settings<VigilateSettings>.Main.State)
            {
                _logger.Info("setting thread execution state as " +
                    EXECUTION_STATE.ES_CONTINUOUS + " " +
                    EXECUTION_STATE.ES_DISPLAY_REQUIRED + " " +
                    EXECUTION_STATE.ES_SYSTEM_REQUIRED);
                SetThreadExecutionState(
                    EXECUTION_STATE.ES_CONTINUOUS
                    | EXECUTION_STATE.ES_DISPLAY_REQUIRED
                    | EXECUTION_STATE.ES_SYSTEM_REQUIRED);
                await Task.Delay(Settings<VigilateSettings>.Main.PollPeriodMs);
            }
            _event.WaitOne();
        },
        TaskCreationOptions.LongRunning);
        await Settings<VigilateSettings>.Save();
    }
    public async void SomeSleep()
    {
        Settings<VigilateSettings>.Main.State = false;
        await Settings<VigilateSettings>.Save();
        StateChange?.Invoke(false);
        _event.Set();
        _logger.Info("vigilate has been disabled, computer can sleep.");
    }
}
