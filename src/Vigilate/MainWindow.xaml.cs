using System.Reflection;
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
            Title = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            vigilator = new();
            vigilator.StateChange += Vigilator_StateChange;
            InitTaskbar();
            bindings = new();
            DataContext = bindings;
            CheckState();
        }
        private void Vigilator_StateChange(object sender, System.EventArgs e)
        {
            bindings.StartEnabled = !Settings.Main.State;
            bindings.StopEnabled = Settings.Main.State;
        }
        private void CheckState()
        {
            if(Settings.Main.State) 
            {
                vigilator.NoSleep();
            }
        }
        private void InitTaskbar()
        {
            TaskbarMenu taskbarMenu = new(vigilator);
            taskbarIcon.ContextMenu = taskbarMenu;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Left = SystemParameters.WorkArea.Right - Width;
            Top = SystemParameters.WorkArea.Bottom - Height;
            bindings.StartEnabled = !Settings.Main.State;
            bindings.StopEnabled = Settings.Main.State;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vigilator.NoSleep();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            vigilator.SomeSleep();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void taskbarIcon_TrayBalloonTipClicked(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("here");
        }
    }
}
