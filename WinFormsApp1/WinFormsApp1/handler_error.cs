using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    class handler_error : error_logs
    {
        private string _file_error_logs { get; set; }

        public  FileStream fstream_file { get; set; }

        public handler_error(string file_error_logs)
        {
            _file_error_logs = file_error_logs;
        }

        public int error_logs_open()
        {
            FileInfo fileInfo = new FileInfo(_file_error_logs);
            if (!fileInfo.Exists)
            {
                fstream_file = new FileStream(_file_error_logs, FileMode.OpenOrCreate);
            }
            else
                fstream_file = new FileStream(_file_error_logs, FileMode.Open);
            return (0);
        }

        public void write_error_logs(string error)
        {
            byte[] buffer = Encoding.Default.GetBytes(error + "\n");
            fstream_file.Write(buffer);
        }

        public int error_logs_close()
        {
            fstream_file.Close();
            return (0);
        }
    }
}
