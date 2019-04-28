using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using FlightSimulator.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private Server modelServer;

        public FlightBoardViewModel(Server server)
        {
            this.modelServer = server;
            modelServer.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName);
            };
        }
        public double Lon
        {
            get { return modelServer.Lon; }
        }

        public double Lat
        {
            get { return modelServer.Lat; }
        }

        private ICommand _settingsCommand;
        public ICommand SettingsCommand
        {
            get
            {
                return _settingsCommand ?? (_settingsCommand = new CommandHandler(() => SettingsClick()));
            }
        }
        private void SettingsClick()
        {
            var settings = new Settings();
            settings.Show();

        }

        private ICommand _connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return _connectCommand ?? (_connectCommand = new CommandHandler(() => ConnectClick()));
            }
        }
        private void ConnectClick()
        {
            Server.Instance.connectServer();
            CommandConnect.Instance.ConnetAsClient();

        }
    }
}