using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    class JoystickWindowViewModel : BaseNotify
    {
        public double Throttle
        {
            get;
            set;
        }
        public double Rudder
        {
            get;
            set;
        }
    }
}
