using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HPSocketCS;
using System.Runtime.InteropServices;

namespace HP_SocketTest
{
    public partial class Client : Form
    {
        private delegate void ShowMsgHandler(Msgs state, string msg);
        private ShowMsgHandler ShowMsgDelegate;
        private delegate void UpdateControlHandler(AppState state);
        private UpdateControlHandler UpdateControlDelegate;

        private TcpClient m_client;

        public enum Msgs
        {
            Info, Waring, Error
        }

        public enum AppState
        {
            Starting, Started, Stoping, Stoped, Error
        }

        public Client()
        {
            InitializeComponent();

            this.Load += Client_Load;
            this.FormClosing += Client_FormClosing;

            btnStart.Click += Start_Click;
            btnStop.Click += Stop_Click;
            btnSend.Click += Send_Click;

            rtbSend.KeyUp += Send_KeyUp;
        }

        private void Send_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.Enter)
            {
                Send_Click(null, null);
            }
        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_client.IsStarted)
            {
                Stop_Click(null, null);
            }
        }

        private void Send_Click(object sender, EventArgs e)
        {
            string content = rtbSend.Text;
            if (content.Length == 0)
            {
                AddMsg(Msgs.Waring, "请输入发送的数据");
                return;
            }
            try
            {
                byte[] data = Encoding.Default.GetBytes(content);
                if (m_client.Send(data, data.Length))
                {
                    rtbSend.Text = string.Empty;
                }
                else
                {
                    AddMsg(Msgs.Waring, $"发送失败：{m_client.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                SetControlState(AppState.Error);
                AddMsg(Msgs.Error, ex.Message);
            }
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_client.Stop() == false)
                {
                    AddMsg(Msgs.Waring, $"关闭连接失败：{m_client.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                SetControlState(AppState.Error);
                AddMsg(Msgs.Error, ex.Message);
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            string ip = tbIPAddress.Text.Trim();
            ushort port = ushort.Parse(tbPort.Text.Trim());

            try
            {
                if (m_client.Connect(ip, port))
                {
                    SetControlState(AppState.Started);
                    AddMsg(Msgs.Info, "连接服务器成功");
                }
                else
                {
                    AddMsg(Msgs.Waring, $"连接服务器失败：{m_client.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                SetControlState(AppState.Error);
                AddMsg(Msgs.Error, ex.Message);
            }
        }

        private void Client_Load(object sender, EventArgs e)
        {
            ShowMsgDelegate = AddMsg;
            UpdateControlDelegate = SetControlState;

            m_client = new TcpClient();
            m_client.OnConnect += OnConnect;
            m_client.OnSend += OnSend;
            m_client.OnReceive += OnReceive;
            m_client.OnClose += OnClose;

            SetControlState(AppState.Stoped);
        }

        private HandleResult OnClose(TcpClient sender, SocketOperation enOperation, int errorCode)
        {
            if (errorCode == 0)
            {
                SetControlState(AppState.Stoped);
                AddMsg(Msgs.Waring, $"断开与服务器的连接 Msg:{sender.ErrorMessage} Code:{sender.ErrorCode}");
            }
            return HandleResult.Ok;
        }

        private HandleResult OnReceive(TcpClient sender, byte[] bytes)
        {
            try
            {
                string content = Encoding.Default.GetString(bytes);
                AddMsg(Msgs.Info, content);
            }
            catch (Exception ex)
            {
                AddMsg(Msgs.Error, ex.Message);
            }
            return HandleResult.Ok;
        }

        private HandleResult OnSend(TcpClient sender, byte[] bytes)
        {
            return HandleResult.Ok;
        }

        private HandleResult OnConnect(TcpClient sender)
        {
            return HandleResult.Ok;
        }

        private void AddMsg(Msgs state, string msg)
        {
            if (rtbReceive.InvokeRequired)
            {
                rtbReceive.Invoke(ShowMsgDelegate, state, msg);
                return;
            }
            string strState = "信息";
            switch (state)
            {
                case Msgs.Waring:
                    strState = "警告";
                    break;
                case Msgs.Error:
                    strState = "错误";
                    break;
            }
            rtbReceive.AppendText($"{strState}：{msg}\n");
        }

        private void SetControlState(AppState state)
        {
            if (btnStart.InvokeRequired)
            {
                btnStart.Invoke(UpdateControlDelegate, state);
                return;
            }
            btnStart.Enabled = (state == AppState.Stoped);
            btnStop.Enabled = (state == AppState.Started);
            btnSend.Enabled = (state == AppState.Started);

            tbIPAddress.Enabled = (state == AppState.Stoped);
            tbPort.Enabled = (state == AppState.Stoped);
        }
    }
}
