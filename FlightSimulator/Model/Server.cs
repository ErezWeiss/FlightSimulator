using FlightSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    public class Server : BaseNotify
    {
        private static Server m_Instance = null;
        TcpClient _client;
        TcpListener _listener;
        double lon, lat;


        public bool shouldStop
        {
            set;
            get;
        }


        public double Lon
        {
            set
            {
                lon = value;
                NotifyPropertyChanged("Lon");

            }
            get { return lon; }
        }

        public double Lat
        {
            set
            {
                lat = value;
                NotifyPropertyChanged("Lat");
            }
            get { return lat; }
        }

        public static Server Instance => m_Instance == null ? m_Instance = new Server() : m_Instance;

        private Server()
        {

            shouldStop = false;
        }

        // connect to the game
        public void connectServer()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ApplicationSettingsModel.Instance.FlightServerIP),
        ApplicationSettingsModel.Instance.FlightInfoPort);
            _listener = new TcpListener(ep);
            _listener.Start();
            _client = _listener.AcceptTcpClient();
            Console.WriteLine("Connected as client");

            Thread thread = new Thread(() => listen(_client, _listener));
            thread.Start();

        }

        // recieves the lon and lat from the plane
        public void listen(TcpClient _client, TcpListener _listener)
        {
            Byte[] bytes;
            NetworkStream ns = _client.GetStream();
            //recieve message from the simulator until shouldStop.
            while (!shouldStop)
            {
                if (_client.ReceiveBufferSize > 0)
                {
                    bytes = new byte[_client.ReceiveBufferSize];
                    ns.Read(bytes, 0, _client.ReceiveBufferSize);
                    string msg = Encoding.ASCII.GetString(bytes); //the message incoming
                    string[] splitMs = msg.Split(',');
                    if (msg.Contains(","))
                    {
                        Lon = double.Parse(splitMs[0]);
                        Lat = double.Parse(splitMs[1]);
                    }
                }
            }
            ns.Close();
            _client.Close();
            _listener.Stop();
        }


        public void DisConnect()
        {
            shouldStop = true;
        }

        public bool isConnected()
        {
            return (_listener != null);
        }

    }
}