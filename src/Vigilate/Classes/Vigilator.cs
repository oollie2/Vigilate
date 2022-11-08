using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Vigilate.Classes
{
    internal class Vigilator
    {
        public TaskBarBindings TaskBarBindings { get; set; }
        public event EventHandler StateChange;
        internal Vigilator() { }
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);
        [Flags]
        private enum EXECUTION_STATE : uint
        {
            ES_SYSTEM_REQUIRED = 0x00000001,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
        }
        private readonly AutoResetEvent _event = new(false);
        public void NoSleep()
        {
            TaskBarBindings.StartStop = "Stop";
            Settings.Main.State = true;
            StateChange?.Invoke(this, EventArgs.Empty);
            new TaskFactory().StartNew(async () =>
            {
                while (Settings.Main.State)
                {
                    SetThreadExecutionState(
                        EXECUTION_STATE.ES_CONTINUOUS
                        | EXECUTION_STATE.ES_DISPLAY_REQUIRED
                        | EXECUTION_STATE.ES_SYSTEM_REQUIRED);
                    await Task.Delay(Settings.Main.PollPeriodMs);
                }
                _event.WaitOne();
            },
            TaskCreationOptions.LongRunning);
        }
        public void SomeSleep()
        {
            TaskBarBindings.StartStop = "Start";
            Settings.Main.State = false;
            StateChange?.Invoke(this, EventArgs.Empty);
            _event.Set();
        }
    }
}
