using SearchAThing.Wpf.Toolkit;
using System.ComponentModel;

namespace StatusBarTaskDispatcher
{

    public class Global : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        static Global instance;
        public static Global Instance
        {
            get
            {
                if (instance == null) instance = new Global();

                return instance;
            }
        }

        StatusManager statusManager;
        public StatusManager StatusManager
        {
            get
            {
                if (statusManager == null) statusManager = new StatusManager();
                return statusManager;
            }
        }

    }

}
