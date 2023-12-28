using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeClientes.Domain
{
    public static class Logger
    {
        private static readonly string logFilePath = Path.Combine(Path.Combine(Path.GetTempPath(), "GestaoDeClientes"), "log.txt");

        static Logger()
        {
            string logDirectory = Path.GetDirectoryName(logFilePath);

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
        }

        public static void LogException(Exception ex)
        {
            string logMessage = $"[EXCEPTION] {DateTime.Now:yyyy-MM-dd HH:mm:ss}: {ex.GetType().FullName}\nMessage: {ex.Message}\nStack Trace:\n{ex.StackTrace}\n\n";
            WriteLog(logMessage);
        }

        public static void LogMessage(string message)
        {
            string logMessage = $"[INFO] {DateTime.Now:yyyy-MM-dd HH:mm:ss}: {message}\n\n";
            WriteLog(logMessage);
        }

        private static void WriteLog(string logMessage)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(logFilePath))
                {
                    writer.Write(logMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao escrever no arquivo de log: {ex.Message}");
            }
        }
    }
}
