using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Vigilate
{
    public partial class Vigilate : ServiceBase
    {
        Timer timer = new Timer();
        public Vigilate()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is started at " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000; //number in milisecinds  
            timer.Enabled = true;
            timer.Start();
        }
        protected override void OnStop()
        {
            OSFunctions.Sleep.AllowSleep();
            WriteToFile("Service stopping " + DateTime.Now + "PC will now be able to sleep.");
        }
        protected override void OnPause()
        {
            base.OnPause();
            timer.Stop();
        }
        protected override void OnContinue()
        {
            base.OnContinue();
            timer.Start();
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile("Setting thread execution state to prevent sleep. " + DateTime.Now);
            OSFunctions.Sleep.PreventSleep();
        }
        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}
