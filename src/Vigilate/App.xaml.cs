using System.Reflection;
using System;
using System.Windows;
using Vigilate.Classes;

namespace VigilateUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MainWindow MainWin;
        private Settings Settings { get; set; }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
#if DEBUG
            string SettingsFile = Environment.ExpandEnvironmentVariables(@"%APPDATA%\" +
                ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCompanyAttribute), false)).Company + @"\" +
                Assembly.GetExecutingAssembly().GetName().Name +
                @"\Settings\settings_test.xml");

#else
            string SettingsFile = Environment.ExpandEnvironmentVariables(@"%APPDATA%\" +
                ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCompanyAttribute), false)).Company + @"\" +
                Assembly.GetExecutingAssembly().GetName().Name +
                @"\Settings\settings.xml");
#endif
            Settings = new(SettingsFile);

            MainWin = new();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Settings.Dispose();
            Environment.Exit(0);
        }
    }
}
