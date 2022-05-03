using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        connect_to_serv connect;
        handler_error error_logs = new handler_error("./file_error_logs.txt");
        public Form1()
        {
            error_logs.error_logs_open();
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                    error_logs.write_error_logs("textbox empty");
                else
                {
                    string ipaddress_reader = textBox1.Text;
                    int port_reader = int.Parse(textBox2.Text);
                    string ipaddress_communications = textBox3.Text;
                    int port_communications = int.Parse(textBox4.Text);

                    connect = new connect_to_serv(ipaddress_reader, port_reader, ipaddress_communications, port_communications);
                    connect.connect();
                    button1.Enabled = false;
                    button2.Enabled = true;
                    Task.Run(() => connect.tread_read_baracod(0, 3));
                    Task.Run(() => connect.tread_weark_communications());
                }
            }
            catch(Exception ex)
            {
                string error = ex.Message;
                error_logs.write_error_logs(error);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            connect.disconect();
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void MainWindow_FormClosing(object sender, EventArgs e)
        {
            //connect.disconect();
            error_logs.error_logs_close();
        }
    }
}
