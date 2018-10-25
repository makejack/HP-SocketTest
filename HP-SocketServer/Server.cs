using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HPSocketCS;
using System.Runtime.InteropServices;

namespace HP_SocketServer
{
    public partial class Server : Form
    {
        private delegate void ShowMsgHandler(Msgs state, string msg);
        private ShowMsgHandler AddMsgDelegate;
        private delegate void UpdateDataGridViewHandler(DataTable dt);
        private UpdateDataGridViewHandler UpdateDataGridViewDelegate;

        TcpServer m_server;
        Extra<ClientInfo> m_extra;

        Hashtable m_recordClient;
        Hashtable m_recordChatWith;

        public enum Msgs
        {
            Info,
            Error,
            Waring
        }

        public enum AppState
        {
            Starting, Started, Stoping, Stoped, Error
        }

        public Server()
        {
            InitializeComponent();

            this.Load += Service_Load;
            this.FormClosing += Server_FormClosing;

            btnStart.Click += Start_Click;
            btnStop.Click += Stop_Click;
            btnDisconn.Click += Disconn_Click;
            btnChartWith.Click += CharWith_Click;

            dgvClient.SelectionChanged += DgvClient_SelectionChanged;
        }

        private void CharWith_Click(object sender, EventArgs e)
        {
            if (dgvClient.CurrentRow != null)
            {
                IntPtr connId = (IntPtr)Convert.ToInt32(dgvClient.CurrentRow.Cells["connid"].Value.ToString());
                ClientInfo clientInfo = m_extra.Get(connId);
                ChatWith form;
                if (m_recordChatWith.ContainsKey(connId))
                {
                    form = m_recordChatWith[connId] as ChatWith;
                }
                else
                {
                    form = new ChatWith(m_server,connId);
                    form.Text = $"IP:{clientInfo.IpAddress}";
                    form.FormClosing += ChatWith_FormClosing;
                    m_recordChatWith.Add(connId, form);
                }
                form.Show();
            }
        }

        private void ChatWith_FormClosing(object sender, FormClosingEventArgs e)
        {
            ChatWith form = sender as ChatWith;
            IntPtr connId = form.GetChatWithConnId();
            m_recordChatWith.Remove(connId);
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (m_server.IsStarted)
                {
                    Stop_Click(null, null);
                }
            }
            catch
            {

            }
        }

        private void DgvClient_SelectionChanged(object sender, EventArgs e)
        {
            bool enabled = false;
            if (dgvClient.CurrentRow != null)
            {
                if (m_server != null && m_server.IsStarted)
                {
                    enabled = true;
                }
            }
            btnDisconn.Enabled = enabled;
            btnChartWith.Enabled = enabled;
        }

        private void Disconn_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvClient.CurrentRow == null)
                {
                    AddMsg(Msgs.Waring, "未选择连接的客户端");
                    return;
                }

                IntPtr connId = (IntPtr)Convert.ToInt32(dgvClient.CurrentRow.Cells["connid"].Value.ToString());
                if (m_server.Disconnect(connId))
                {
                    AddMsg(Msgs.Info, $"断开客户端：{connId} 成功");
                }
                else
                {
                    AddMsg(Msgs.Waring, $"断开客户端失败：{m_server.ErrorMessage} Code：{m_server.ErrorCode}");
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
                if (m_server.Stop())
                {
                    SetControlState(AppState.Stoped);
                    AddMsg(Msgs.Info, "停止服务");
                }
                else
                {
                    AddMsg(Msgs.Waring, $"停止服务异常：{m_server.ErrorMessage} Code：{m_server.ErrorCode}");
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
                m_server.IpAddress = ip;
                m_server.Port = port;

                if (m_server.Start())
                {
                    SetControlState(AppState.Started);
                    AddMsg(Msgs.Info, $"服务开启正常：{ip} {port}");
                }
                else
                {
                    SetControlState(AppState.Stoped);
                    AddMsg(Msgs.Waring, $"服务开启异常：{m_server.ErrorMessage} Code：{m_server.ErrorCode}");
                }
            }
            catch (Exception ex)
            {
                SetControlState(AppState.Error);
                AddMsg(Msgs.Error, ex.Message);
            }
        }

        private void Service_Load(object sender, EventArgs e)
        {
            AddMsgDelegate = AddMsg;
            UpdateDataGridViewDelegate = UpdateDataGridView;

            m_recordClient = new Hashtable();
            m_recordChatWith = new Hashtable();
            m_extra = new Extra<ClientInfo>();

            m_server = new TcpServer();
            m_server.OnPrepareListen += OnPrepareListen;
            m_server.OnAccept += OnAccept;
            m_server.OnReceive += OnReceive;
            m_server.OnSend += OnSend;
            m_server.OnClose += OnClose;
            m_server.OnShutdown += OnShutdown;
            
            SetControlState(AppState.Stoped);
        }

        private HandleResult OnShutdown()
        {
            return HandleResult.Ok;
        }

        private HandleResult OnClose(IntPtr connId, SocketOperation enOperation, int errorCode)
        {
            try
            {
                ClientInfo clientInfo = m_extra.Get(connId);
                if (errorCode == 0)
                {
                    AddMsg(Msgs.Info, $"关闭客户端：{connId} IP地址：{clientInfo.IpAddress} 端口：{clientInfo.Port}");
                }
                else
                {
                    AddMsg(Msgs.Waring, $"关闭客户端：{connId} 异常：{m_server.ErrorMessage} Code：{m_server.ErrorCode}");
                }
                m_recordClient.Remove(connId);
                if (m_extra.Remove(connId))
                {
                    DataTable dt = dgvClient.DataSource as DataTable;
                    dt.Rows.Clear();

                    foreach (ClientInfo item in m_recordClient.Values)
                    {
                        DataRow row = dt.NewRow();
                        row["connid"] = item.ConnId;
                        row["ip"] = item.IpAddress;
                        row["port"] = item.Port;
                        dt.Rows.Add(row);
                    }

                    UpdateDataGridView(dt);
                }
                else
                {
                    AddMsg(Msgs.Waring, $"移除客户端失败");
                }
            }
            catch (Exception ex)
            {
                SetControlState(AppState.Error);
                AddMsg(Msgs.Error, ex.Message);
            }

            return HandleResult.Ok;
        }

        private HandleResult OnSend(IntPtr connId, byte[] bytes)
        {
            return HandleResult.Ok;
        }

        private HandleResult OnReceive(IntPtr connId, byte[] bytes)
        {
            try
            {
                string content = Encoding.Default.GetString(bytes);

                ClientInfo clientInfo = m_extra.Get(connId);
                if (m_recordChatWith.ContainsKey(clientInfo.ConnId))
                {
                    ChatWith form = m_recordChatWith[clientInfo.ConnId] as ChatWith;
                    form.Receive(content);
                }
                else
                {
                    AddMsg(Msgs.Info, $"IP：{clientInfo.IpAddress} 内容：{content}");
                }
            }
            catch (Exception ex)
            {
                AddMsg(Msgs.Error, ex.Message);
            }
            return HandleResult.Ok;
        }

        private HandleResult OnAccept(IntPtr connId, IntPtr pClient)
        {
            string ip = string.Empty;
            ushort port = 0;

            try
            {
                if (m_server.GetRemoteAddress(connId, ref ip, ref port))
                {
                    AddMsg(Msgs.Info, $"连接客户端：IP地址：{ip} 端口：{port}");
                }
                else
                {
                    AddMsg(Msgs.Waring, $"连接客户端异常：{m_server.ErrorMessage} Code：{m_server.ErrorCode}");
                }

                ClientInfo clientInfo = new ClientInfo()
                {
                    ConnId = connId,
                    IpAddress = ip,
                    Port = port
                };

                m_recordClient.Add(connId, clientInfo);
                if (m_extra.Set(connId, clientInfo))
                {
                    DataTable dt = dgvClient.DataSource as DataTable;
                    if (dt == null)
                    {
                        dt = new DataTable();
                        dt.Columns.Add("connid", typeof(string));
                        dt.Columns.Add("ip", typeof(string));
                        dt.Columns.Add("port", typeof(string));
                    }

                    DataRow row = dt.NewRow();
                    row["connid"] = connId;
                    row["ip"] = ip;
                    row["port"] = port;

                    dt.Rows.Add(row);

                    UpdateDataGridView(dt);
                }
                else
                {
                    AddMsg(Msgs.Waring, $"连接客户端：{connId} 设置连接附加信息异常");
                }
            }
            catch (Exception ex)
            {
                SetControlState(AppState.Error);
                AddMsg(Msgs.Error, ex.Message);
            }

            return HandleResult.Ok;
        }

        private HandleResult OnPrepareListen(IntPtr soListen)
        {
            return HandleResult.Ok;
        }

        private void SetControlState(AppState state)
        {
            btnStart.Enabled = (state == AppState.Stoped);
            btnStop.Enabled = (state == AppState.Started);
            tbIPAddress.Enabled = (state == AppState.Stoped);
            tbPort.Enabled = (state == AppState.Stoped);
        }

        private void AddMsg(Msgs state, string msg)
        {
            if (rtbMsg.InvokeRequired)
            {
                rtbMsg.Invoke(AddMsgDelegate, state, msg);
                return;
            }
            string strState = "信息";
            switch (state)
            {
                case Msgs.Error:
                    strState = "错误";
                    break;
                case Msgs.Waring:
                    strState = "警告";
                    break;
            }
            rtbMsg.AppendText($"{strState}：{msg}\n");
            rtbMsg.ScrollToCaret();
        }

        private void UpdateDataGridView(DataTable dt)
        {
            if (dgvClient.InvokeRequired)
            {
                dgvClient.Invoke(UpdateDataGridViewDelegate, dt);
                return;
            }

            dgvClient.DataSource = dt;
        }
    }


    public class ClientInfo
    {
        public IntPtr ConnId { get; set; }

        public string IpAddress { get; set; }

        public ushort Port { get; set; }
    }
}
