using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Co.NamingLevelsPlugin.ViewModels.Abstract
{
    public abstract class Notifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
