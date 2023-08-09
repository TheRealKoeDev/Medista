using LinqToDB;

namespace AppData.Utils
{
    public class AppDatabaseConnection : LinqToDB.Data.DataConnection
    {
        public AppDatabaseConnection(string providerName, string connectionString) : base(providerName, connectionString)
        {
        }
    }
}
