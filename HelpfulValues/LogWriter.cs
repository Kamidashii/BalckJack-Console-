using System;
using HelpfulValues.Constants;
using System.IO;
using System.Text;

namespace HelpfulValues
{
    public class LogWriter
    {
        public static void WriteLog(string message)
        {
            CreateLogFolder();

            using (StreamWriter writer = new StreamWriter(GameService_Constants.LOG_PATH, true, Encoding.Default))
            {
                writer.Write(message);
            }
        }


        private static void CreateLogFolder()
        {
            if (!Directory.Exists(GameService_Constants.LOG_FOLDER))
            {
                Directory.CreateDirectory(GameService_Constants.LOG_FOLDER);
            }
        }

        private static void CreateLogFile()
        {
            if (!File.Exists(GameService_Constants.LOG_PATH))
            {
                File.Create(GameService_Constants.LOG_PATH);
            }
        }
    }
}
