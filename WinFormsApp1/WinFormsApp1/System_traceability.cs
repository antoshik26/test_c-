using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    interface System_traceability
    {
        Socket Connect_socket(IPAddress ip, Int32 port);
        int weark_communications(Socket socket, baracod baracod);
        int Disconnect(Socket socket);
    }
}
