using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace socketClient
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            textIP.Text = "127.0.0.1";
            textPort.Text = "9966";
        }
        Socket socketSend;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse(textIP.Text);
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(textPort.Text));
                socketSend.Connect(point);
                ShowMsg("连接成功");

                Thread th = new Thread(Revice);
                th.IsBackground = true;
                th.Start();
            }
            catch { }
        }
        void ShowMsg(string s)
        {
            textInfo.AppendText(s + "\r\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string str = textMsg.Text.Trim();
                ShowMsg("我：" + str);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
                socketSend.Send(buffer);
                textMsg.Clear();
            }
            catch { }
        }

        void Revice()
        {
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024 * 1024 * 2];
                    int r = socketSend.Receive(buffer);
                    if (r == 0)
                        break;
                    ShowMsg(socketSend.RemoteEndPoint + ":" + Encoding.UTF8.GetString(buffer, 0, r));
                }
                catch { }
                
            }
        }
    }
}
