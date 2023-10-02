using System.Reflection;
using System;
using System.Windows;
using Vigilate.Core;
using NLog;

namespace Vigilate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static ILogger _logger = LogManager.GetCurrentClassLogger();
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _logger.Info("starting vigilate.");
#if DEBUG
            string SettingsFile = Environment.ExpandEnvironmentVariables(@"%APPDATA%\" +
                ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCompanyAttribute), false)).Company + @"\" +
                Assembly.GetExecutingAssembly().GetName().Name +
                @"\Settings\settings_test.json");

#else
            string SettingsFile = Environment.ExpandEnvironmentVariables(@"%APPDATA%\" +
                ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCompanyAttribute), false)).Company + @"\" +
                Assembly.GetExecutingAssembly().GetName().Name +
                @"\Settings\settings.json");
#endif
            Settings<VigilateSettings>.Read(SettingsFile).Wait();

            _ = new MainWindow();
            _logger.Info("vigilate succesfully started.");
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            _logger.Info("vigilate shutting down.");
            Settings<VigilateSettings>.Save().Wait();
            Environment.Exit(0);
        }
    }
}
