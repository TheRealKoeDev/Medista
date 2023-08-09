using System;
using System.IO;

namespace LazyLoops.Ultils
{
    public static class AppFolderHandler
    {
        private static string RoamingFolderPath => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string DataPath => Path.Combine(RoamingFolderPath, nameof(LazyLoops));

    }
}
