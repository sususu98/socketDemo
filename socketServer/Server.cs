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

namespace socketDemo
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            textPort.Text = "9966";
            textIP.Text = "127.0.0.1";
        }

        void ShowMsg(string s)
        {
            textInfo.AppendText(s+"\r\n");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Any;
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(textPort.Text));
                socketWatch.Bind(point);
                ShowMsg("监听成功");
                socketWatch.Listen(10);
                Thread th = new Thread(Listen);
                th.IsBackground = true;
                th.Start(socketWatch);
            }
            catch { }
            
        }
        Socket socketSend;
        private void Listen(object o)
        {
            Socket socketWatch = o as Socket;
            while (true)
            {
                try
                {
                    socketSend = socketWatch.Accept();
                    ShowMsg(socketSend.RemoteEndPoint.ToString() + ":" + "连接成功");
                    Thread th = new Thread(Recive);
                    th.IsBackground = true;
                    th.Start(socketSend);
                }
                catch { }
                
            }
            
            
        }
        void Recive(object o)
        {
            Socket socketSend = o as Socket;
            while(true)
            {
                try
                {
                    byte[] buffer = new byte[1024 * 1024 * 2];
                    int r = socketSend.Receive(buffer);
                    if (r == 0)
                        break;
                    string str = Encoding.UTF8.GetString(buffer, 0, r);
                    if (str == "exit")
                        break;
                    ShowMsg(socketSend.RemoteEndPoint + ":" + str + "");
                }
                catch { }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string str = textSend.Text;
                ShowMsg("我："+str);
                byte[] buffer = Encoding.UTF8.GetBytes(str);
                socketSend.Send(buffer);
                textSend.Clear();
            }
            catch { }
        }
    }
}
