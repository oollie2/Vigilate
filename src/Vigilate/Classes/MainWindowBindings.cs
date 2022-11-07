using System.ComponentModel;

namespace Vigilate.Classes
{
    sealed class MainWindowBindings: INotifyPropertyChanged
    {
        public MainWindowBindings()
        {
            main = new()
            {
                StartEnabled = true,
                StopEnabled = false
            };
        }
        public static MainWindowData main;
        public bool StartEnabled
        {
            get { return main.StartEnabled; }
            set { main.StartEnabled = value; OnPropertyChanged(nameof(StartEnabled)); }
        }
        public bool StopEnabled
        {
            get { return main.StopEnabled; }
            set { main.StopEnabled = value; OnPropertyChanged(nameof(StopEnabled)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    sealed class MainWindowData
    {
        public bool StartEnabled { get; set; }
        public bool StopEnabled { get; set; }   
    }
}
