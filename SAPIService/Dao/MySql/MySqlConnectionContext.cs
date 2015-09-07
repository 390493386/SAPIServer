using MySql.Data.MySqlClient;

namespace SiweiSoft.SAPIService.Dao.MySql
{
    public class MySqlConnectionContext : ConnectionContext<MySqlConnection>
    {
        public MySqlConnectionContext()
        {
        }
    }
}
