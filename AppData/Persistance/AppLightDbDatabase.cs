// Test Header

using LiteDB;

namespace AppData.Persistance
{
    public class AppLightDbDatabase : LiteDatabase, IDisposable
    {
        public AppLightDbDatabase(string? dbFile = null) : base(HandleDbFilePath(dbFile))
        {
        }

        private static string? HandleDbFilePath(string? dbFilePath)
        {
            string usedDbFile = dbFilePath ?? Path.Combine(Environment.CurrentDirectory, "Data", "App.db");
            if (dbFilePath == null)
            {
                string dbFileDirectory = Path.GetDirectoryName(usedDbFile) ?? throw new Exception("Failed to generate database directory");
                Directory.CreateDirectory(dbFileDirectory);
            }

            return usedDbFile;
        }

        ~AppLightDbDatabase()
        {
            Dispose();
        }
    }
}
