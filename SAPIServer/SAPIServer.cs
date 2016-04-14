using SiweiSoft.SAPIService.Core;
using SiweiSoft.SAPIService.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;

namespace SiweiSoft.SAPIServer
{
    public partial class SAPIServer : Form
    {
        private Dictionary<string, string> configuration;
        private SapiService serviceInstance;
        private Thread serviceThread;

        public SAPIServer()
        {
            InitializeComponent();
            RefreshSystemConfiguration();

            //打印日志事件
            Log.LogEvent += LogComment;
        }

        private void SvcStartStop_Click(object sender, EventArgs e)
        {
            if (serviceInstance != null && serviceInstance.Status == Status.Running)
                serviceInstance.Stop();
            else
            {
                this.RefreshSystemConfiguration();
                string serverIP = configuration.ContainsKey("ServerIP") ? configuration["ServerIP"] : null;
                int port = 0;
                if (configuration.ContainsKey("ServerPort"))
                    Int32.TryParse(configuration["ServerPort"], out port);
                string serviceRoot = configuration.ContainsKey("ServiceRoot") ? configuration["ServiceRoot"] : null;
                string fileSavedPath = configuration.ContainsKey("FileSavedPath") ? configuration["FileSavedPath"] : null;
                string cookieName = configuration.ContainsKey("CookieName") ? configuration["CookieName"] : null;
                int cookieExpiredTime = 3600;
                if (configuration.ContainsKey("CookieExpiredTime"))
                    Int32.TryParse(configuration["CookieExpiredTime"], out cookieExpiredTime);
                string controllersAssembly = configuration.ContainsKey("ControllersAssembly") ? configuration["ControllersAssembly"] : null;

                ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings["TQ"];
                string conStr = setting != null && setting.ConnectionString != String.Empty ? setting.ConnectionString : null;

                //请求需要认证并且没有经过认证时的处理代码
                string notAuthorized = configuration.ContainsKey("NotAuthorized") ? configuration["NotAuthorized"] : null;

                //用户自定义配置
                Dictionary<string, object> serConfig = new Dictionary<string, object>();
                //支持跨多个域
                List<string> originHosts = new List<string>();
                foreach (KeyValuePair<string, string> config in configuration)
                {
                    string key = config.Key.ToUpper();
                    if (key.StartsWith("UDF-"))
                        serConfig.Add(config.Key.Substring(4), config.Value);
                    else if (key.StartsWith("ORIGINHOST"))
                        originHosts.Add(config.Value);
                }

                //创建服务实例
                serviceInstance = new SapiService(serverIP, port, rootPath: serviceRoot,
                    originHosts: originHosts, fileServerPath: fileSavedPath, cookieName: cookieName,
                    cookieExpires: cookieExpiredTime, controllersAssembly: controllersAssembly,
                    connectionString: conStr, notAuthorized: notAuthorized, serverConfig: serConfig);
                //初始化服务
                serviceInstance.Initialize();
                if (serviceInstance.Status == Status.NotInitialized)
                {
                    Log.Comment(CommentType.Error, "服务启动失败。");
                }
                else
                {
                    //开辟新的线程运行服务
                    serviceThread = new Thread(serviceInstance.Process<UserSession>);
                    serviceThread.Start();

                    Log.Comment(CommentType.Info, string.Format("服务正在运行。"));
                }
            }
        }

        //主线程中在窗口打印日志信息
        private void LogCommentM(CommentType commentType, string comment)
        {
            string mark = null;
            ItemType itemType = ItemType.Error;
            if (commentType == CommentType.Info)
            {
                mark = "消息";
                itemType = ItemType.Info;
            }
            else if (commentType == CommentType.Warn)
            {
                mark = "警告";
                itemType = ItemType.Warn;
            }
            else if (commentType == CommentType.Error)
            {
                mark = "错误";
                itemType = ItemType.Error;
            }

            string message = String.Format("{0} [{1}] {2}", DateTime.Now.ToString(), mark, comment);
            SListBoxItem item = new SListBoxItem(message, itemType);

            //添加滚动效果，在添加记录前，先计算滚动条是否在底部，从而决定添加后是否自动滚动
            bool scoll = false;
            if (logsBox.TopIndex == logsBox.Items.Count - (int)(logsBox.Height / logsBox.ItemHeight))
                scoll = true;
            //添加记录
            logsBox.Items.Add(item);
            //滚动到底部
            if (scoll)
                logsBox.TopIndex = logsBox.Items.Count - (int)(logsBox.Height / logsBox.ItemHeight);
        }

        /// <summary>
        /// 子线程中调用的打印日志的方法
        /// </summary>
        /// <param name="type">信息类型</param>
        /// <param name="message">信息</param>
        private void LogComment(CommentType type, string message)
        {
            this.Invoke(new Action<CommentType, string>(this.LogCommentM), type, message);
        }

        private void RefreshSystemConfiguration()
        {
            configuration = new Dictionary<string, string>();
            ConfigurationManager.RefreshSection("appSettings");
            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                configuration.Add(key, ConfigurationManager.AppSettings[key]);
            }

            //在界面上显示config配置
            c_selfStartService.Checked = configuration.ContainsKey("SvcSelfStart") && configuration["SvcSelfStart"] == "true";
            t_localIPAddress.Text = configuration.ContainsKey("ServerIP") ? configuration["ServerIP"] : null;
            t_localPort.Text = configuration.ContainsKey("ServerPort") ? configuration["ServerPort"] : null;
            t_rootPath.Text = configuration.ContainsKey("ServiceRoot") ? configuration["ServiceRoot"] : null;
            t_serviceName.Text = configuration.ContainsKey("ServiceName") ? configuration["ServiceName"] : null;
            t_originHost.Text = configuration.ContainsKey("OriginHost") ? configuration["OriginHost"] : null;
            t_fileSvPath.Text = configuration.ContainsKey("FileSavedPath") ? configuration["FileSavedPath"] : null;
            t_controllersAssembly.Text = configuration.ContainsKey("ControllersAssembly") ? configuration["ControllersAssembly"] : null;
            t_cookieName.Text = configuration.ContainsKey("CookieName") ? configuration["CookieName"] : null;
            t_cookieExpires.Text = configuration.ContainsKey("CookieExpiredTime") ? configuration["CookieExpiredTime"] : null;
        }

        private void SaveConfig_Click(object sender, EventArgs e)
        {
            string svcSelfStart = c_selfStartService.Checked ? "true" : "false";
            string serverIP = t_localIPAddress.Text;
            string serverPort = t_localPort.Text;
            string root = t_rootPath.Text;
            string svcName = t_serviceName.Text;
            string ohost = t_originHost.Text;
            string filePath = t_fileSvPath.Text;
            string contAssem = t_controllersAssembly.Text;
            string cName = t_cookieName.Text;
            string cExpires = t_cookieExpires.Text;

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["SvcSelfStart"].Value = svcSelfStart;
            config.AppSettings.Settings["ServerIP"].Value = serverIP;
            config.AppSettings.Settings["ServerPort"].Value = serverPort;
            config.AppSettings.Settings["ServiceRoot"].Value = root;
            config.AppSettings.Settings["ServiceName"].Value = svcName;
            config.AppSettings.Settings["OriginHost"].Value = ohost;
            config.AppSettings.Settings["FileSavedPath"].Value = filePath;
            config.AppSettings.Settings["ControllersAssembly"].Value = contAssem;
            config.AppSettings.Settings["CookieName"].Value = cName;
            config.AppSettings.Settings["CookieExpiredTime"].Value = cExpires;
            config.Save(ConfigurationSaveMode.Modified);

            //禁用控件
            c_selfStartServer.Enabled = false;
            c_selfStartService.Enabled = false;
            t_localIPAddress.Enabled = false;
            t_localPort.Enabled = false;
            t_rootPath.Enabled = false;
            t_serviceName.Enabled = false;
            t_originHost.Enabled = false;
            t_fileSvPath.Enabled = false;
            t_controllersAssembly.Enabled = false;
            t_cookieName.Enabled = false;
            t_cookieExpires.Enabled = false;
            SaveConfig.Enabled = false;

            EditConfig.Enabled = true;

            Log.Comment(CommentType.Warn, "服务配置信息已更改，重启服务生效！");
            MessageBox.Show("保存成功，重启服务生效！");
        }

        private void ClearLog_Click(object sender, EventArgs e)
        {
            logsBox.Items.Clear();
        }

        //窗体加载时响应事件
        private void SAPIServer_Load(object sender, EventArgs e)
        {
            if (configuration.ContainsKey("SvcSelfStart") && configuration["SvcSelfStart"] == "true")
                SvcStartStop.PerformClick();
        }

        private void SAPIServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serviceInstance != null && serviceInstance.Status == Status.Running)
            {
                if (MessageBox.Show("关闭窗口前将会停止服务，确定关闭窗口？", "确定", MessageBoxButtons.YesNo) == DialogResult.No ||
                    !serviceInstance.Stop())
                    e.Cancel = true;
            }
        }

        private void logsBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int selectedIndex = logsBox.IndexFromPoint(e.Location);
            if (selectedIndex != ListBox.NoMatches)
            {
                LBItemDetail logDetais = new LBItemDetail(((SListBoxItem)logsBox.Items[selectedIndex]).Text);
                //logDetais.ldRichTextBox
                logDetais.ShowDialog();
            }
        }

        private void EditConfig_Click(object sender, EventArgs e)
        {
            c_selfStartServer.Enabled = true;
            c_selfStartService.Enabled = true;
            t_localIPAddress.Enabled = true;
            t_localPort.Enabled = true;
            t_rootPath.Enabled = true;
            t_serviceName.Enabled = true;
            t_originHost.Enabled = true;
            t_fileSvPath.Enabled = true;
            t_controllersAssembly.Enabled = true;
            t_cookieName.Enabled = true;
            t_cookieExpires.Enabled = true;
            SaveConfig.Enabled = true;

            EditConfig.Enabled = false;
        }
    }
}
