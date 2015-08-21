using System;

namespace SiweiSoft.SAPIService.Helper
{
    /// <summary>
    /// Comment类型
    /// </summary>
    public enum CommentType
    {
        Info,
        Warn,
        Error
    }

    public class Log
    {
        //Log委托
        public delegate void LogHandler(CommentType type, string comment);

        //Log事件
        public static event LogHandler LogEvent;

        /// <summary>
        /// 显示Comment
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="arg"></param>
        public static void Comment(CommentType type, string message, params object[] arg)
        {
            if (LogEvent != null)
            {
                if (arg.Length == 0)
                    LogEvent(type, message);
                else
                    LogEvent(type, String.Format(message, arg));
            }
        }
    }
}
