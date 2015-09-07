using SiweiSoft.SAPIService.Helper;
using System.Data;
using System.Data.Common;

namespace SiweiSoft.SAPIService.Dao
{
    /// <summary>
    /// 数据库连接上下文
    /// </summary>
    public abstract class ConnectionContext<TConnection> where TConnection : DbConnection
    {
        /// <summary>
        /// 数据库连接实例
        /// </summary>
        public TConnection Connection
        {
            get;
            set;
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public virtual bool OpenConnection(out DAOProcessCode processCode)
        {
            bool result = true;
            processCode = DAOProcessCode.Success;
            if (Connection == null)
            {
                result = false;
                processCode = DAOProcessCode.NullConnection;
                Log.Comment(CommentType.Error, "数据库连接为null。");
            }
            else if (Connection.State != ConnectionState.Open)
            {
                try
                {
                    Connection.Open();
                }
                catch (DbException e)
                {
                    result = false;
                    processCode = DAOProcessCode.ConnectionOpenFailed;
                    Log.Comment(CommentType.Error, "数据库连接为打开失败，错误信息：" + e.Message);
                }
            }
            return result;
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public virtual bool CloseConnection(out DAOProcessCode processCode)
        {
            bool result = true;
            processCode = DAOProcessCode.Success;
            if (Connection == null)
            {
                result = false;
                processCode = DAOProcessCode.NullConnection;
                Log.Comment(CommentType.Error, "数据库连接为null。");
            }
            if (Connection.State != ConnectionState.Closed)
            {
                try
                {
                    Connection.Close();
                }
                catch (DbException e)
                {
                    result = false;
                    processCode = DAOProcessCode.ConnectionCloseFailed;
                    Log.Comment(CommentType.Error, "数据库连接为关闭失败，错误信息：" + e.Message);
                }
            }
            return result;
        }
    }

    /// <summary>
    /// 数据库执行代码
    /// </summary>
    public enum DAOProcessCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success,

        /// <summary>
        /// 数据库连接为空
        /// </summary>
        NullConnection,

        /// <summary>
        /// 数据库连接打开失败
        /// </summary>
        ConnectionOpenFailed,

        /// <summary>
        /// 数据库连接关闭失败
        /// </summary>
        ConnectionCloseFailed,

        /// <summary>
        /// 执行查询语句失败
        /// </summary>
        ExecuteNonQueryFailed,

        /// <summary>
        /// 事物执行失败
        /// </summary>
        ExecuteTransactionFailed,

        /// <summary>
        /// Reader执行失败
        /// </summary>
        ExecuteReaderFailed,

        /// <summary>
        /// Scalar执行失败
        /// </summary>
        ExecuteScalarFailed,

        /// <summary>
        /// 获取DataSet失败
        /// </summary>
        GetDataSetFailed,

        /// <summary>
        /// 参数为空或者null
        /// </summary>
        ArguementNullOrEmpty,

        /// <summary>
        /// 添加参数错误
        /// </summary>
        AddParameterFailure,
    }
}
