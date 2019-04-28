using System;
using FlightSimulator.Model;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    class AutoPilotViewModel : BaseNotify
    {
        private String content = "";
        private bool didSend = false;

        public String BackgroundColor
        {
            get
            {
                if (content != "")
                {
                    if (didSend)
                    {
                        didSend = false;
                        return "White";
                    }
                    return "Pink";
                }
                else
                {
                    return "White";
                }
            }
        }

        public String Content
        {
            get { return content; }

            set
            {
                content = value;
                NotifyPropertyChanged("Content");
                NotifyPropertyChanged("BackgroundColor");
            }
        }

        private ICommand clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                return clearCommand ?? (clearCommand = new CommandHandler(() => ClearClick()));
            }
        }
        private void ClearClick()
        {
            content = "";
            NotifyPropertyChanged("Content");
            NotifyPropertyChanged("BackgroundColor");
        }


        private ICommand okCommand;
        public ICommand OkCommand
        {
            get
            {
                return okCommand ?? (okCommand = new CommandHandler(() => OKClick()));
            }
        }

        private void OKClick()
        {
            CommandConnect.Instance.Send(content);
            didSend = true;
            NotifyPropertyChanged("BackgroundColor");
        }
    }
}
