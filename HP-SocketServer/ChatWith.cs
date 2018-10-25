using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HPSocketCS;

namespace HP_SocketServer
{
    public partial class ChatWith : Form
    {
        private delegate void ShowReceiveHandler(string content);
        private ShowReceiveHandler ShowReceiveDelegate;

        private TcpServer m_server;
        private IntPtr m_clientConnId;

        public ChatWith(TcpServer server, IntPtr connId)
        {
            InitializeComponent();

            ShowReceiveDelegate = Receive;

            m_server = server;
            m_clientConnId = connId;

            btnSend.Click += Send_Click;

            rtbSend.KeyUp += Send_KeyUp;
        }

        private void Send_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.Enter)
            {
                Send_Click(null, null);
            }
        }

        private void Send_Click(object sender, EventArgs e)
        {
            string content = rtbSend.Text;
            if (content.Length == 0)
            {
                Receive("发送数据不能为空");
                return;
            }
            try
            {
                if (m_server.IsStarted)
                {
                    byte[] data = Encoding.Default.GetBytes(content);
                    bool result = m_server.Send(m_clientConnId, data, data.Length);
                    if (result)
                    {
                        rtbSend.Text = string.Empty;
                    }
                    else
                    {
                        Receive($"发送失败：{m_server.ErrorMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                Receive(ex.Message);
            }
        }

        public void Receive(string content)
        {
            if (rtbReceive.InvokeRequired)
            {
                rtbReceive.Invoke(ShowReceiveDelegate, content);
                return;
            }

            rtbReceive.AppendText($"{content}\n");
        }

        public IntPtr GetChatWithConnId()
        {
            return m_clientConnId;
        }
    }
}
