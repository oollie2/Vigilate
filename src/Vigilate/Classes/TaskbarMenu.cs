using System;
using System.Windows.Controls;
using VigilateUI;

namespace Vigilate.Classes
{
    internal class TaskbarMenu : ContextMenu
    {
        private MenuItem toolStripShow;
        private MenuItem toolStripHide;
        internal TaskbarMenu()
        {
            Init();
        }
        private void Init()
        {
            toolStripShow = new();
            toolStripShow.Header = "Show";
            toolStripShow.Click += ToolStripShow_Click;
            toolStripHide = new();
            toolStripHide.Header = "Exit";
            toolStripHide.Click += ToolStripHide_Click;

            Items.Add(toolStripShow);
            Items.Add(toolStripHide);
        }
        private void ToolStripShow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (App.MainWin.WindowState != System.Windows.WindowState.Minimized)
                App.MainWin.Show();
        }
        private void ToolStripHide_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
