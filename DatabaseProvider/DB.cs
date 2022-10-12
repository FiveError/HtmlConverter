using Microsoft.Data.Sqlite;

namespace DatabaseProvider
{
    public class DB
    {
        private static readonly Object s_lock = new Object();
        private static DB instance = null;

        private string connectionDB = "Data Source=/app/queue.db;Mode=ReadWriteCreate;";
        private SqliteConnection connection;
        private DB()
        {
            connection = new SqliteConnection(connectionDB);
        }

        public static DB Instance
        {
            get
            {
                if (instance != null)
                    return instance;
                Monitor.Enter(s_lock);
                DB temp = new DB();
                Interlocked.Exchange(ref instance, temp);
                Monitor.Exit(s_lock);
                return instance;
            }
        }

        public void RunSqlRequestQuery(string Request)
        {
            OpenConnection();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = Request;
            command.ExecuteNonQuery();

        }
        public SqliteDataReader? RunSqlRequestReader(string Request)
        {
            OpenConnection();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = Request;
            return command.ExecuteReader();

        }

        private void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

    }
}
