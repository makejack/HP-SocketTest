namespace HP_SocketServer
{
    partial class Server
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDisconn = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbIPAddress = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rtbMsg = new System.Windows.Forms.RichTextBox();
            this.dgvClient = new System.Windows.Forms.DataGridView();
            this.connid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.port = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnChartWith = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClient)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnChartWith);
            this.panel1.Controls.Add(this.btnDisconn);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbPort);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbIPAddress);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 50);
            this.panel1.TabIndex = 0;
            // 
            // btnDisconn
            // 
            this.btnDisconn.Enabled = false;
            this.btnDisconn.Location = new System.Drawing.Point(483, 15);
            this.btnDisconn.Name = "btnDisconn";
            this.btnDisconn.Size = new System.Drawing.Size(75, 23);
            this.btnDisconn.TabIndex = 6;
            this.btnDisconn.Text = "Disconnection";
            this.btnDisconn.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(402, 14);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(321, 14);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "端口";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(215, 15);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(100, 21);
            this.tbPort.TabIndex = 2;
            this.tbPort.Text = "61234";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP地址";
            // 
            // tbIPAddress
            // 
            this.tbIPAddress.Location = new System.Drawing.Point(59, 15);
            this.tbIPAddress.Name = "tbIPAddress";
            this.tbIPAddress.Size = new System.Drawing.Size(100, 21);
            this.tbIPAddress.TabIndex = 0;
            this.tbIPAddress.Text = "127.0.0.1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rtbMsg);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 350);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 100);
            this.panel2.TabIndex = 1;
            // 
            // rtbMsg
            // 
            this.rtbMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbMsg.Location = new System.Drawing.Point(0, 0);
            this.rtbMsg.Name = "rtbMsg";
            this.rtbMsg.Size = new System.Drawing.Size(800, 100);
            this.rtbMsg.TabIndex = 0;
            this.rtbMsg.Text = "";
            // 
            // dgvClient
            // 
            this.dgvClient.AllowUserToAddRows = false;
            this.dgvClient.AllowUserToDeleteRows = false;
            this.dgvClient.AllowUserToResizeRows = false;
            this.dgvClient.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvClient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClient.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.connid,
            this.ip,
            this.port});
            this.dgvClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvClient.Location = new System.Drawing.Point(0, 50);
            this.dgvClient.MultiSelect = false;
            this.dgvClient.Name = "dgvClient";
            this.dgvClient.ReadOnly = true;
            this.dgvClient.RowTemplate.Height = 23;
            this.dgvClient.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClient.Size = new System.Drawing.Size(800, 300);
            this.dgvClient.TabIndex = 2;
            // 
            // connid
            // 
            this.connid.DataPropertyName = "connid";
            this.connid.HeaderText = "connid";
            this.connid.Name = "connid";
            this.connid.ReadOnly = true;
            this.connid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ip
            // 
            this.ip.DataPropertyName = "ip";
            this.ip.HeaderText = "ip";
            this.ip.Name = "ip";
            this.ip.ReadOnly = true;
            this.ip.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // port
            // 
            this.port.DataPropertyName = "port";
            this.port.HeaderText = "port";
            this.port.Name = "port";
            this.port.ReadOnly = true;
            this.port.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btnChartWith
            // 
            this.btnChartWith.Enabled = false;
            this.btnChartWith.Location = new System.Drawing.Point(564, 15);
            this.btnChartWith.Name = "btnChartWith";
            this.btnChartWith.Size = new System.Drawing.Size(110, 23);
            this.btnChartWith.TabIndex = 7;
            this.btnChartWith.Text = "The Chart With";
            this.btnChartWith.UseVisualStyleBackColor = true;
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvClient);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Server";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClient)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbIPAddress;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox rtbMsg;
        private System.Windows.Forms.DataGridView dgvClient;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnDisconn;
        private System.Windows.Forms.DataGridViewTextBoxColumn connid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn port;
        private System.Windows.Forms.Button btnChartWith;
    }
}

