using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

namespace FlightSimulator.Model
{
    class CommandConnect
    {
        TcpClient client;
        private Mutex mutex;
        private static CommandConnect m_Instance = null;
        public static CommandConnect Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new CommandConnect();
                }
                return m_Instance;
            }
        }

        private CommandConnect()
        {
            isConnected = false;
            mutex = new Mutex();
        }


        private bool isConnected;
        public bool IsConnected
        {
            get;
            set;
        }

        public void ConnetAsClient()
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ApplicationSettingsModel.Instance.FlightServerIP),
                ApplicationSettingsModel.Instance.FlightCommandPort);
            client = retTcpClient();
            client.Connect(iPEndPoint);
            isConnected = true;
            Console.WriteLine("You are connected in Command channel");
        }

        private TcpClient retTcpClient()
        {
            return new TcpClient();
        }

        public void DisConnect()
        {
            isConnected = false;
            client.Close();
        }

        public void Send(string message)
        {
            string[] commands = message.Split(new[] { Environment.NewLine },
            StringSplitOptions.None);
            mutex.WaitOne();
            Thread thread = new Thread(() => RunSend(commands, client));
            thread.Start();
            mutex.ReleaseMutex();

        }

        private void RunSend(string[] commands, TcpClient client)
        {
            if (!isConnected) return;
            NetworkStream stream = client.GetStream();

            foreach (string command in commands)
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(command + "\r\n");
                stream.Write(data, 0, data.Length);
                System.Threading.Thread.Sleep(2000);
            }
        }
        
    }
}
