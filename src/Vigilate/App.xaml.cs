using System.Windows;

namespace VigilateUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MainWindow MainWin;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWin = new();
        }
    }
}
