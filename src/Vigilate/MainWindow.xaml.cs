using System.Windows;
using Vigilate.Classes;

namespace VigilateUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Vigilator vigilator;
        private MainWindowBindings bindings;
        public MainWindow()
        {
            InitializeComponent();
            InitTaskbar();
            vigilator = new();
            bindings = new();
            DataContext = bindings;
        }
        private void InitTaskbar()
        {
            TaskbarMenu taskbarMenu = new();
            taskbarIcon.ContextMenu = taskbarMenu;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Left = SystemParameters.WorkArea.Right - Width;
            Top = SystemParameters.WorkArea.Bottom - Height;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vigilator.NoSleep(30000);
            bindings.StartEnabled = false;
            bindings.StopEnabled = true;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            vigilator.SomeSleep();
            bindings.StartEnabled = true;
            bindings.StopEnabled = false;
        }
    }
}
