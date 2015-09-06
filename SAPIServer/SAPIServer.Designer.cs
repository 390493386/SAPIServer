namespace SiweiSoft.SAPIServer
{
    partial class SAPIServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SAPIServer));
            this.tab1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ClearLog = new System.Windows.Forms.Button();
            this.AutoTask = new System.Windows.Forms.Button();
            this.SvcStartStop = new System.Windows.Forms.Button();
            this.logsBox = new SiweiSoft.SAPIServer.SListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.t_cookieExpires = new System.Windows.Forms.TextBox();
            this.t_cookieName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SaveConfig = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.t_controllersAssembly = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.t_localIPAddress = new System.Windows.Forms.TextBox();
            this.t_fileSvPath = new System.Windows.Forms.TextBox();
            this.t_originHost = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.t_serviceName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.t_rootPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.t_localPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.c_selfStartService = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.c_selfStartServer = new System.Windows.Forms.CheckBox();
            this.tab1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.Controls.Add(this.tabPage1);
            this.tab1.Controls.Add(this.tabPage2);
            this.tab1.Location = new System.Drawing.Point(12, 12);
            this.tab1.Name = "tab1";
            this.tab1.SelectedIndex = 0;
            this.tab1.Size = new System.Drawing.Size(641, 449);
            this.tab1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ClearLog);
            this.tabPage1.Controls.Add(this.AutoTask);
            this.tabPage1.Controls.Add(this.SvcStartStop);
            this.tabPage1.Controls.Add(this.logsBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(633, 423);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SAPI服务";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ClearLog
            // 
            this.ClearLog.Location = new System.Drawing.Point(550, 389);
            this.ClearLog.Name = "ClearLog";
            this.ClearLog.Size = new System.Drawing.Size(77, 23);
            this.ClearLog.TabIndex = 3;
            this.ClearLog.Text = "清除日志";
            this.ClearLog.UseVisualStyleBackColor = true;
            this.ClearLog.Click += new System.EventHandler(this.ClearLog_Click);
            // 
            // AutoTask
            // 
            this.AutoTask.Location = new System.Drawing.Point(172, 389);
            this.AutoTask.Name = "AutoTask";
            this.AutoTask.Size = new System.Drawing.Size(160, 23);
            this.AutoTask.TabIndex = 2;
            this.AutoTask.Text = "自动任务：开始/停止";
            this.AutoTask.UseVisualStyleBackColor = true;
            // 
            // SvcStartStop
            // 
            this.SvcStartStop.Location = new System.Drawing.Point(6, 389);
            this.SvcStartStop.Name = "SvcStartStop";
            this.SvcStartStop.Size = new System.Drawing.Size(160, 23);
            this.SvcStartStop.TabIndex = 1;
            this.SvcStartStop.Text = "SAPI服务：开始/停止";
            this.SvcStartStop.UseVisualStyleBackColor = true;
            this.SvcStartStop.Click += new System.EventHandler(this.SvcStartStop_Click);
            // 
            // logsBox
            // 
            this.logsBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.logsBox.FormattingEnabled = true;
            this.logsBox.ItemHeight = 12;
            this.logsBox.Location = new System.Drawing.Point(6, 7);
            this.logsBox.Name = "logsBox";
            this.logsBox.Size = new System.Drawing.Size(621, 376);
            this.logsBox.TabIndex = 0;
            this.logsBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.logsBox_MouseDoubleClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.SaveConfig);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(633, 423);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "系统配置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.t_cookieExpires);
            this.groupBox3.Controls.Add(this.t_cookieName);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(7, 301);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(620, 82);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // t_cookieExpires
            // 
            this.t_cookieExpires.Location = new System.Drawing.Point(122, 50);
            this.t_cookieExpires.Name = "t_cookieExpires";
            this.t_cookieExpires.Size = new System.Drawing.Size(221, 21);
            this.t_cookieExpires.TabIndex = 15;
            // 
            // t_cookieName
            // 
            this.t_cookieName.Location = new System.Drawing.Point(122, 21);
            this.t_cookieName.Name = "t_cookieName";
            this.t_cookieName.Size = new System.Drawing.Size(221, 21);
            this.t_cookieName.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "过期时间：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "cookie名称：";
            // 
            // SaveConfig
            // 
            this.SaveConfig.Location = new System.Drawing.Point(6, 394);
            this.SaveConfig.Name = "SaveConfig";
            this.SaveConfig.Size = new System.Drawing.Size(104, 23);
            this.SaveConfig.TabIndex = 2;
            this.SaveConfig.Text = "保存";
            this.SaveConfig.UseVisualStyleBackColor = true;
            this.SaveConfig.Click += new System.EventHandler(this.SaveConfig_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.t_controllersAssembly);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.t_localIPAddress);
            this.groupBox2.Controls.Add(this.t_fileSvPath);
            this.groupBox2.Controls.Add(this.t_originHost);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.t_serviceName);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.t_rootPath);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.t_localPort);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(7, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(620, 220);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // t_controllersAssembly
            // 
            this.t_controllersAssembly.Location = new System.Drawing.Point(122, 185);
            this.t_controllersAssembly.Name = "t_controllersAssembly";
            this.t_controllersAssembly.Size = new System.Drawing.Size(221, 21);
            this.t_controllersAssembly.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(36, 189);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "控制器：";
            // 
            // t_localIPAddress
            // 
            this.t_localIPAddress.Location = new System.Drawing.Point(122, 20);
            this.t_localIPAddress.Name = "t_localIPAddress";
            this.t_localIPAddress.Size = new System.Drawing.Size(151, 21);
            this.t_localIPAddress.TabIndex = 14;
            // 
            // t_fileSvPath
            // 
            this.t_fileSvPath.Location = new System.Drawing.Point(122, 152);
            this.t_fileSvPath.Name = "t_fileSvPath";
            this.t_fileSvPath.Size = new System.Drawing.Size(221, 21);
            this.t_fileSvPath.TabIndex = 13;
            // 
            // t_originHost
            // 
            this.t_originHost.Location = new System.Drawing.Point(122, 119);
            this.t_originHost.Name = "t_originHost";
            this.t_originHost.Size = new System.Drawing.Size(221, 21);
            this.t_originHost.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 156);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "文件路径：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "源主机：";
            // 
            // t_serviceName
            // 
            this.t_serviceName.Location = new System.Drawing.Point(122, 86);
            this.t_serviceName.Name = "t_serviceName";
            this.t_serviceName.Size = new System.Drawing.Size(221, 21);
            this.t_serviceName.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "服务名称：";
            // 
            // t_rootPath
            // 
            this.t_rootPath.Location = new System.Drawing.Point(122, 53);
            this.t_rootPath.Name = "t_rootPath";
            this.t_rootPath.Size = new System.Drawing.Size(221, 21);
            this.t_rootPath.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "根目录：";
            // 
            // t_localPort
            // 
            this.t_localPort.Location = new System.Drawing.Point(295, 20);
            this.t_localPort.Name = "t_localPort";
            this.t_localPort.Size = new System.Drawing.Size(48, 21);
            this.t_localPort.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(279, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = ":";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "主机配置：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.c_selfStartService);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.c_selfStartServer);
            this.groupBox1.Location = new System.Drawing.Point(7, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(620, 79);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // c_selfStartService
            // 
            this.c_selfStartService.AutoSize = true;
            this.c_selfStartService.Location = new System.Drawing.Point(122, 45);
            this.c_selfStartService.Name = "c_selfStartService";
            this.c_selfStartService.Size = new System.Drawing.Size(84, 16);
            this.c_selfStartService.TabIndex = 2;
            this.c_selfStartService.Text = "服务自启动";
            this.c_selfStartService.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "系统配置：";
            // 
            // c_selfStartServer
            // 
            this.c_selfStartServer.AutoSize = true;
            this.c_selfStartServer.Location = new System.Drawing.Point(122, 23);
            this.c_selfStartServer.Name = "c_selfStartServer";
            this.c_selfStartServer.Size = new System.Drawing.Size(72, 16);
            this.c_selfStartServer.TabIndex = 0;
            this.c_selfStartServer.Text = "开机运行";
            this.c_selfStartServer.UseVisualStyleBackColor = true;
            // 
            // SAPIServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 470);
            this.Controls.Add(this.tab1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SAPIServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SAPIServer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SAPIServer_FormClosing);
            this.Load += new System.EventHandler(this.SAPIServer_Load);
            this.tab1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tab1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button SvcStartStop;
        private SListBox logsBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button SaveConfig;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox c_selfStartService;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox c_selfStartServer;
        private System.Windows.Forms.TextBox t_localPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox t_rootPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox t_serviceName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox t_cookieExpires;
        private System.Windows.Forms.TextBox t_cookieName;
        private System.Windows.Forms.TextBox t_fileSvPath;
        private System.Windows.Forms.TextBox t_originHost;
        private System.Windows.Forms.TextBox t_localIPAddress;
        private System.Windows.Forms.TextBox t_controllersAssembly;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button ClearLog;
        private System.Windows.Forms.Button AutoTask;
    }
}

