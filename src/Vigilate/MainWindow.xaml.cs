using System;
using System.Reflection;
using System.Windows;
using Vigilate.Core;

namespace Vigilate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Vigilator vigilator;
        private readonly PropertyStatus bindings;
        public MainWindow()
        {
            InitializeComponent();
            Version v = Assembly.GetExecutingAssembly().GetName().Version;
            Title = string.Format("{0}.{1}.{2}", v.Major, v.Minor, v.Build);
            vigilator = new();
            vigilator.StateChange += Vigilator_StateChange;
            InitTaskbar();
            bindings = new();
            DataContext = bindings;
            CheckState();
        }
        private void Vigilator_StateChange(object sender, System.EventArgs e)
        {
            bindings.StartEnabled = !Settings<VigilateSettings>.Main.State;
            bindings.StopEnabled = Settings<VigilateSettings>.Main.State;
        }
        private void CheckState()
        {
            if (Settings<VigilateSettings>.Main.State)
            {
                vigilator.NoSleep();
            }
        }
        private void InitTaskbar()
        {
            TaskbarMenu taskbarMenu = new(vigilator, this);
            taskbarIcon.ContextMenu = taskbarMenu;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Left = SystemParameters.WorkArea.Right - Width;
            Top = SystemParameters.WorkArea.Bottom - Height;
            bindings.StartEnabled = !Settings<VigilateSettings>.Main.State;
            bindings.StopEnabled = Settings<VigilateSettings>.Main.State;
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

        private void TaskbarIcon_TrayBalloonTipClicked(object sender, RoutedEventArgs e)
        { }
    }
}
