using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO.Ports;
using EasyModbus;
using Microsoft.VisualBasic.Devices;
using System.Net.NetworkInformation;
using System.Net;
using System.IO;

namespace WinFormsApp1
{
    class connect_to_serv : Form1, System_traceability
    {
        private string _ipadress { get; set; }

        private string _ipadress_communications { get; set; }
        private int _port { get; set; }

        private int _port_communications { get; set; }

        private ushort startAddress;
        
        private ushort quantity;

        private baracod baracod_read_now;

        private ModbusClient tcpClient;


        public connect_to_serv(string ipadress, int port, string ipadress_communications, int port_communications)
        {
            _ipadress = ipadress;
            _port = port;
            _ipadress_communications = ipadress_communications;
            _port_communications = port_communications;
        }

        public int connect()
        {
            try
            {
                tcpClient = new ModbusClient(_ipadress, _port);
                tcpClient.ConnectionTimeout = 500;
                tcpClient.LogFileFilename = "logs.txt";
                tcpClient.Connect();
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                //return (-1);
            }
            return (0);
        }

        public int tread_weark_communications()
        {
            Socket socket = Connect_socket(IPAddress.Parse(_ipadress_communications), _port_communications);
            int result = 0;

            while (result == 0)
            {
                result = weark_communications(socket, baracod_read_now);
                if (result == -1)
                {
                    label9.Text = "True";
                    Disconnect(socket);
                    return (-1);
                }
                else
                {
                    label9.Text = "False";
                }
                if (read_identifier(IPAddress.Parse(_ipadress_communications)) == 0)
                {
                    result = 0;
                    label7.Text = "Cистемы прослеживаемости подключена";
                }
                else
                {
                    result = -1;
                    label7.Text = "Cистемы прослеживаемости не подключена";
                }

            }
            return (0);
        }

        public int tread_read_baracod(int startAddress, int count)
        {
            Sqllite db_connect_table_baracod = new Sqllite();
            bool connect = true;

            while (connect == true)
            {
                int [] baracod = tcpClient.ReadInputRegisters(startAddress, count);
                label4.Text = Convert.ToString(baracod);
                DateTime date = DateTime.Now;
                baracod_read_now = new baracod(Convert.ToString(baracod), date);
                db_connect_table_baracod.baracods_values.Add(baracod_read_now);
                db_connect_table_baracod.SaveChanges();
                if (read_identifier(IPAddress.Parse(_ipadress)) == 0)
                {
                    connect = true;
                    label6.Text = "Баракод ридер подключен";
                }
                else
                {
                    connect = false;
                    label6.Text = "Баракод ридер не подключен";
                }
            }
            return (0);
        }

        private int read_identifier(IPAddress ip_connect)
        {
            Ping p1 = new Ping();
            PingReply reply = p1.Send(ip_connect, 500);
            if (reply.Status == IPStatus.Success)
                return (0);
            return (-1);
        }

        public int disconect()
        {
            tcpClient.Disconnect();
            return (0);
        }

        public Socket Connect_socket(IPAddress ip, Int32 port)
        {
            IPEndPoint ipPoint = new IPEndPoint(ip, port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipPoint);
            return (socket);
        }

        public int weark_communications(Socket socket, baracod baracod)
        {
            byte[] data = Encoding.Unicode.GetBytes(baracod._barcod_value + " " + baracod._date.ToString());
            socket.Send(data);
            data = new byte[256];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = socket.Receive(data, data.Length, 0);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (socket.Available > 0);
            if (builder.ToString() == "false")
            {
                return (-1);
            }
            return (0);
        }

        public int Disconnect(Socket socket)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            return (0);
        }
    }   
}
