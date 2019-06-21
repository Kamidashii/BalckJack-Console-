using System;
using Common.Constants;
using System.IO;
using System.Text;

namespace Common
{
    public class LogWriter
    {
        public static void WriteLog(string message)
        {
            CreateLogFolder();

            using (StreamWriter writer = new StreamWriter(GameService_Constants.LogPath, true, Encoding.Default))
            {
                writer.Write(message);
            }
        }


        private static void CreateLogFolder()
        {
            if (!Directory.Exists(GameService_Constants.LogFolder))
            {
                Directory.CreateDirectory(GameService_Constants.LogFolder);
            }
        }

        private static void CreateLogFile()
        {
            if (!File.Exists(GameService_Constants.LogPath))
            {
                File.Create(GameService_Constants.LogPath);
            }
        }
    }
}
