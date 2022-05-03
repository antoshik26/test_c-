using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    class Server
    {
        private TcpListener listener;
        Clients[] pollclitnts;

        public Server(int port) 
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
        }

        ~Server()
        {
            if (listener != null)
            {
                listener.Stop();
            }
        }

        public void Weark_serv()
        {
            while (true)
            {
                TcpClient Client = listener.AcceptTcpClient();
                Thread Thread = new Thread(new ParameterizedThreadStart(ClientThread));
                Thread.Start(Client);
            }
        }

        static void ClientThread(Object StateInfo)
        {
            new Clients((TcpClient)StateInfo);
        }

    }
}
