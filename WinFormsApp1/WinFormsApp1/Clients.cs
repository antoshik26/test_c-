using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace WinFormsApp1
{
    class Clients
    {
        public Clients(TcpClient Client)
        {
            while (true)
            {
                string Request = "";
                byte[] Buffer = new byte[1024];
                int Count = 0;
                while ((Count = Client.GetStream().Read(Buffer, 0, Buffer.Length)) > 0)
                {
                    Request += Encoding.ASCII.GetString(Buffer, 0, Count);
                    if (Request.IndexOf("\r\n\r\n") >= 0 || Request.Length > 4096)
                    {
                        string Send = "";
                        Sqllite db_connect_table_baracod = new Sqllite();
                        baracod baracod = db_connect_table_baracod.baracods_values.Where(p => p._barcod_value == Request).First();
                        Send += baracod._date.ToString();
                        byte[] Buffer_send = Encoding.ASCII.GetBytes(Send);
                        Client.GetStream().Write(Buffer_send, 0, Buffer_send.Length);
                    }
                    if ((Count = Client.GetStream().Read(Buffer, 0, Buffer.Length)) > 0)
                    {
                        if (Request.IndexOf("false") >=0)
                        {
                            Client.Close();
                            break;
                            //индикатор falues отправки
                        }
                        else
                        {
                            //индикатор true отправки
                        }
                    }
                }
                if (read_identifier(((IPEndPoint)Client.Client.RemoteEndPoint).Address)  == -1)
                {
                    Client.Close();
                    break;
                    //индикатор falues подключения
                }
                else
                {
                    //индикатор true подключения
                }
            }

        }
        private int read_identifier(IPAddress ip_connect)
        {
            Ping p1 = new Ping();
            PingReply reply = p1.Send(ip_connect, 500);
            if (reply.Status == IPStatus.Success)
                return (0);
            return (-1);
        }
    }
}
