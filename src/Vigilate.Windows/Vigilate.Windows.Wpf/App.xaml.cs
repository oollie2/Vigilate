using System.Reflection;
using System;
using System.Windows;
using Vigilate.Core;
using NLog;
using System.Threading.Tasks;

namespace Vigilate;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    internal static ILogger _logger = LogManager.GetCurrentClassLogger();
    private async void Application_Startup(object sender, StartupEventArgs e)
    {
        _logger.Info("starting vigilate.");
        await LoadSettings();
        _ = new MainWindow();
        _logger.Info("vigilate succesfully started.");
    }

    private async void Application_Exit(object sender, ExitEventArgs e)
    {
        _logger.Info("vigilate shutting down.");
        await Settings<VigilateSettings>.Save();
        Environment.Exit(0);
    }
    private static async Task<bool> LoadSettings()
    {
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
        // Load the settings file
        return await Settings<VigilateSettings>.Read(SettingsFile);
    }
}
