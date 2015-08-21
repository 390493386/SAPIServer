using SiweiSoft.SAPIService.Helper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading;

namespace SiweiSoft.SAPIService.Core
{
    /// <summary>
    /// 服务状态
    /// </summary>
    public enum Status
    {
        NotInitialized,
        Ready,
        Running,
        Stopped
    }

    public class SapiService
    {
        #region private fields

        //待绑定IP
        private string _ipAddress;

        //监听端口
        private int _port;

        //跨源主机
        private string _originHost;

        //是否跨源?
        private bool _withCrossOrigin;

        //文件存放路径
        private string _fileServerPath;

        /// <summary>
        /// Cookie name
        /// </summary>
        private string _cookieName;

        //Cookie名字
        private bool _withCookie;

        //Cookie过期时间（秒）
        private int _cookieExpires;

        //外部Controller程序集
        private string _controllersAssembly;

        //Http监听器
        private HttpListener listener;

        #endregion private fields

        #region internal static fields

        /// <summary>
        /// 服务根目录
        /// </summary>
        internal static string RootPath;

        /// <summary>
        /// 服务器配置
        /// </summary>
        internal static Dictionary<string, object> ServerConfigs;

        /// <summary>
        /// Controllers信息
        /// </summary>
        internal static Dictionary<string, ControllerReflectionInfo> ControllersInfos;

        /// <summary>
        /// Sessions列表
        /// </summary>
        internal static Dictionary<string, Session> SessionsDictionary;

        #endregion internal static fields

        #region private const variable, for default values

        //系统默认cookie过期时间，单位：秒
        private const int defaultCookieExpires = 3600;

        #endregion private const variable, for default values

        #region public properties

        /// <summary>
        /// 服务状态
        /// </summary>
        public Status Status { get; private set; }

        #endregion public properties

        /// <summary>
        /// 无参构造方法
        /// </summary>
        public SapiService()
        {
            _ipAddress = "localhost";
            _port = 8885;
            _withCrossOrigin = false;
            _withCookie = false;

            Status = Status.NotInitialized;
        }

        /// <summary>
        /// 带参构造方法
        /// </summary>
        /// <param name="ipAddress">本地IP地址</param>
        /// <param name="port">可用端口</param>
        /// <param name="RootPath">服务根目录</param>
        /// <param name="originHost">跨源主机地址，不设定表示不需跨源</param>
        /// <param name="fileServerPath">文件存放路径，上传文件请求用到</param>
        /// <param name="cookieName">Cookie名字，不设定表示不需Cookie支持</param>
        /// <param name="cookieExpires">Cookie过期时间，单位：秒</param>
        /// <param name="controllersAssembly">需加载的controllers所在的程序集</param>
        /// <param name="serverConfig">服务器配置</param>
        public SapiService(string ipAddress, int port,
            string rootPath = null, string originHost = null,
            string fileServerPath = null, string cookieName = null,
            int? cookieExpires = null, string controllersAssembly = null,
            Dictionary<string, object> serverConfig = null)
        {
            _ipAddress = ipAddress;
            _port = port;

            if (rootPath != null)
            {
                rootPath = rootPath.Trim(' ');
                RootPath = String.IsNullOrEmpty(rootPath) ? null : rootPath;
            }
            if (originHost != null)
            {
                originHost = originHost.Trim(' ');
                if (!String.IsNullOrEmpty(originHost))
                {
                    _originHost = originHost;
                    _withCrossOrigin = true;
                }
            }
            if (fileServerPath != null)
            {
                fileServerPath = fileServerPath.Trim(' ');
                _fileServerPath = String.IsNullOrEmpty(fileServerPath) ? null : fileServerPath;
            }
            if (cookieName != null)
            {
                cookieName = cookieName.Trim(' ');
                if (!String.IsNullOrEmpty(cookieName))
                {
                    _cookieName = cookieName;
                    _withCookie = true;
                }
            }
            _cookieExpires = cookieExpires ?? defaultCookieExpires;
            _controllersAssembly = controllersAssembly;
            ServerConfigs = serverConfig;

            Status = Status.NotInitialized;
        }

        /// <summary>
        /// 初始化服务
        /// </summary>
        public void Initialize()
        {
            if (String.IsNullOrEmpty(_ipAddress) || _port < 1)
            {
                Log.Comment(CommentType.Error, "服务器配置错误，IP地址或者端口不正确。");
                return;
            }
            Log.Comment(CommentType.Info, "正在初始化服务。。。");
            listener = new HttpListener();
            listener.Prefixes.Add(string.Format("http://{0}:{1}/" +
                (String.IsNullOrEmpty(RootPath) ? null : (RootPath + "/")), _ipAddress, _port.ToString()));
            Status = Status.Ready;
            Log.Comment(CommentType.Info, "服务已经绑定到IP：{0}和端口：{1}。", _ipAddress, _port.ToString());

            try
            {
                listener.Start();
                Status = Status.Running;

                Log.Comment(CommentType.Info, "初始化Sessions列表。。。");
                SessionsDictionary = new Dictionary<string, Session>();

                Log.Comment(CommentType.Info, "初始化Controllers信息。。。");
                Assembly assembly = String.IsNullOrEmpty(_controllersAssembly) ? Assembly.GetCallingAssembly() : Assembly.LoadFrom(_controllersAssembly);
                if (assembly != null)
                {
                    ControllersInfos = new Dictionary<string, ControllerReflectionInfo>();
                    Type[] types = assembly.GetTypes();
                    foreach (Type type in types)
                    {
                        if (type.Name.Length > 10 && type.Name.EndsWith("Controller"))
                        {
                            string key = type.Name.Replace("Controller", null).ToUpper();
                            if (ControllersInfos.ContainsKey(key))
                                Log.Comment(CommentType.Warn, "存在同名的Controller：{0}，可能引起冲突！", key);
                            else
                                ControllersInfos.Add(key, new ControllerReflectionInfo(type));
                        }
                    }
                }
                Log.Comment(CommentType.Info, "是否允许跨源访问：" + (_withCrossOrigin ? "是。" : "否。"));
                Log.Comment(CommentType.Info, "是否支持cookie：" + (_withCookie ? "是。" : "否。"));
                Log.Comment(CommentType.Info, "服务已正常启动，等待连接请求。。。");
            }
            catch (HttpListenerException ex)
            {
                Status = Status.NotInitialized;
                Log.Comment(CommentType.Error, "服务运行错误：" + ex.Message);
            }
        }

        /// <summary>
        /// 运行服务
        /// </summary>
        public void Process<TSession>() where TSession : Session, new()
        {
            if (Status == Status.Running)
            {
                while (Status == Status.Running)
                {
                    try
                    {
                        HttpListenerContext context = listener.GetContext();
                        if (context != null)
                            ThreadPool.QueueUserWorkItem(new WaitCallback(ConcreteProcess<TSession>), context);
                    }
                    catch (HttpListenerException)
                    {
                        Log.Comment(CommentType.Warn, "服务线程已终止。");
                    }
                }
            }
            else
            {
                Log.Comment(CommentType.Error, "服务没有初始化或者初始化出错。");
            }
        }

        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="context"></param>
        private void ConcreteProcess<TSession>(object context) where TSession : Session, new()
        {
            HttpListenerContext requestContext = (HttpListenerContext)context;

            if (requestContext.Request.RawUrl == "/favicon.ico")//Request for the icon
            {
                //TODO: handle the request for the icon
                requestContext.Response.OutputStream.Close();
            }
            else
            {
                TSession session = null;
                if (_withCookie)
                {
                    //Set cros options
                    if (_withCrossOrigin)
                        requestContext.Response.Headers.Add("Access-Control-Allow-Credentials: true");

                    //Get the cookie from the request
                    Cookie cookie = requestContext.Request.Cookies[_cookieName];
                    if (cookie == null)
                    {
                        session = GenerateNewSession<TSession>(requestContext);
                    }
                    else
                    {
                        string cookieString = cookie.Value;
                        if (SessionsDictionary.ContainsKey(cookieString))
                            session = (TSession)SessionsDictionary[cookieString];
                        else
                            session = this.GenerateNewSession<TSession>(requestContext, expires: cookie.Expires);
                    }
                }
                if (_withCrossOrigin)
                    requestContext.Response.Headers.Add("Access-Control-Allow-Origin: " + _originHost);

                SapiRequest request = new SapiRequest(requestContext, session);
                request.Response();
            }
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public void Stop()
        {
            Status = Status.Stopped;
            listener.Stop();
            Log.Comment(CommentType.Warn, "Service stoped.");
        }

        //生成新的session
        private TSession GenerateNewSession<TSession>(HttpListenerContext context, DateTime? expires = null)
            where TSession : Session, new()
        {
            string cookieString = Guid.NewGuid().ToString();
            Cookie cookie = new Cookie(_cookieName, cookieString, "/")
            {
                Expires = expires ?? DateTime.Now.AddSeconds(_cookieExpires)
            };
            context.Response.SetCookie(cookie);
            TSession session = new TSession()
            {
                IsAuthorized = false
            };
            session.ResetExpireDate(_cookieExpires);
            SessionsDictionary.Add(cookieString, session);
            return session;
        }
    }
}
