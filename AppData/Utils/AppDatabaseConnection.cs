using LinqToDB.Data;

namespace AppData.Utils
{
    public class AppDatabaseConnection : DataConnection
    {
        public AppDatabaseConnection(string providerName, string connectionString) : base(providerName, connectionString)
        {
        }
    }
}
