using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeClientes.Util
{
    public class Util
    {
        public static string GetDbFilePath()
        {
            return Path.Combine(GetFolderAppProgramData(), "GestaoDeClientesDB.db");
        }

        public static string GetFolderAppProgramData()
        {
            string appDataPath = "C:\\ProgramData";

            string AppDataPathRCollector = Path.Combine(appDataPath, "GestaoDeClientes");

            if (Directory.Exists(AppDataPathRCollector))
            {
                return AppDataPathRCollector;
            }
            else
            {
                Directory.CreateDirectory(AppDataPathRCollector);
                return AppDataPathRCollector;
            }
        }
        public static string GetDbStringConnection()
        {
            return String.Concat("Data source= ", GetDbFilePath());
        }

        public static string GetDbFilePathFromEnvironmentVariable()
        {
            return Environment.GetEnvironmentVariable("GestaoDeClientesDbPath");
        }
        public static string GetDbStringConnectionFromEnvironmentVariable()
        {
            return String.Concat("Data source= ", GetDbFilePathFromEnvironmentVariable());
        }
    }
}
