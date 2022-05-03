using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    interface error_logs
    {
        int error_logs_open();
        void write_error_logs(string error);
        int error_logs_close();
    }
}
