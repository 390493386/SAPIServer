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
                string originHost = configuration.ContainsKey("OriginHost") ? configuration["OriginHost"] : null;
                string fileSavedPath = configuration.ContainsKey("FileSavedPath") ? configuration["FileSavedPath"] : null;
                string cookieName = configuration.ContainsKey("CookieName") ? configuration["CookieName"] : null;
                int cookieExpiredTime = 3600;
                if (configuration.ContainsKey("CookieExpiredTime"))
                    Int32.TryParse(configuration["CookieExpiredTime"], out cookieExpiredTime);
                string controllersAssembly = configuration.ContainsKey("ControllersAssembly") ? configuration["ControllersAssembly"] : null;

                //创建服务实例
                serviceInstance = new SapiService(serverIP, port, rootPath: serviceRoot, originHost: originHost,
                    fileServerPath: fileSavedPath, cookieName: cookieName, cookieExpires: cookieExpiredTime,
                    controllersAssembly: controllersAssembly, serverConfig: null);
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
        }

        private void SaveConfig_Click(object sender, EventArgs e)
        {
            string svcSelfStart = c_selfStartService.Checked ? "1" : "0";
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
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void ClearLog_Click(object sender, EventArgs e)
        {
            logsBox.Items.Clear();
        }
    }
}
