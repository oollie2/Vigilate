using Microsoft.Extensions.Logging;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Vigilate
{
    public class KeepAwake
    {
        private bool m_KeepAwake { get;set; }   
        public KeepAwake(ILogger logger)
        {
            m_KeepAwake = true;
            while(m_KeepAwake)
            {
                OSFunctions.Sleep.PreventSleep();
                logger.LogInformation("Vigilate running to prevent sleep");
                Thread.Sleep(30000);
            }
            OSFunctions.Sleep.AllowSleep();
        }
        public void Stop(ILogger logger)
        {
            m_KeepAwake = false;
            logger.LogInformation("Vigilate stopping");
        }
    }
    public static class OSFunctions
    {
        public static class Sleep
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);
            public static void PreventSleep()
            {
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS
                    | EXECUTION_STATE.ES_DISPLAY_REQUIRED
                    | EXECUTION_STATE.ES_SYSTEM_REQUIRED);
            }
            public static void AllowSleep()
            {
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
            }

            [Flags]
            private enum EXECUTION_STATE : uint
            {
                ES_SYSTEM_REQUIRED = 0x00000001,
                ES_DISPLAY_REQUIRED = 0x00000002,
                ES_AWAYMODE_REQUIRED = 0x00000040,
                ES_CONTINUOUS = 0x80000000,
            }
        }
    }
}
