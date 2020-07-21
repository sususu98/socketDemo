using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
            textIP.Text = "127.0.0.1";
            textPort.Text = "9966";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(textIP.Text);
            IPEndPoint point =new IPEndPoint(ip, Convert.ToInt32(textPort.Text));
            socketSend.Connect(point);
            ShowMsg("连接成功");
        }
        void ShowMsg(string s)
        {
            textInfo.AppendText(s + "\r\n");
        }
    }
}
