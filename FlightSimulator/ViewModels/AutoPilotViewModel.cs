using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    class AutoPilotViewModel : BaseNotify
    {
        string a;
        public string Commands {
            get {
                return "hello";
            }
            set
            {
                a = value;
                NotifyPropertyChanged("ServerIP");
                Console.WriteLine("Hello World!");
            }
        }
    }
}
