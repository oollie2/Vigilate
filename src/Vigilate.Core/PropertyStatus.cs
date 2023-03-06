using System.ComponentModel;

namespace Vigilate.Core
{
    public class PropertyStatus: INotifyPropertyChanged
    {
        public PropertyStatus()
        {
            main = new()
            {
                StartEnabled = true,
                StopEnabled = false
            };
        }
        public static Properties main;
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
    public class Properties
    {
        public bool StartEnabled { get; set; }
        public bool StopEnabled { get; set; }   
    }
}
