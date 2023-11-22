using Loggin.Enum;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Loggin
{
    public class Log : ILog
    {
        public string PathForLogs { get; set; }

        public async Task AddTrace(string logMessage) => await Add(logMessage, LogLevel.Trace);

        public async Task AddDebug(string logMessage) => await Add(logMessage, LogLevel.Debug);

        public async Task AddInfo(string logMessage) => await Add(logMessage, LogLevel.Information);

        public async Task AddWarning(string logMessage) => await Add(logMessage, LogLevel.Warning);

        public async Task AddError(string logMessage) => await Add(logMessage, LogLevel.Error);

        public async Task AddCritical(string logMessage) => await Add(logMessage, LogLevel.Critical);

        public async Task AddNone(string logMessage) => await Add(logMessage, LogLevel.None);

        #region Helper

        private async Task Add(string logMessage, LogLevel logLevel = 0)
        {

            string path = GetPathForLevel(logLevel);
            CreateDirectory(path);
            StreamWriter sw = new StreamWriter(path + "/" + GetNameFile(logLevel), true);
            await sw.WriteAsync(DateTime.Now + " - " + logMessage + Environment.NewLine);
            sw.Close();
            
        }

        private string GetNameFile(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    return "trace_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + ".txt";
                case LogLevel.Debug:
                    return "debug_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + ".txt";
                case LogLevel.Information:
                    return "info_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + ".txt";
                case LogLevel.Warning:
                    return "warng_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + ".txt";
                case LogLevel.Error:
                    return "err_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + ".txt";
                case LogLevel.Critical:
                    return "crit_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + ".txt";
                case LogLevel.None:
                    return "none_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + ".txt";
            }
            return string.Empty;
        }

        private string GetPathForLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    return $@"{PathForLogs}\Trace\";
                case LogLevel.Debug:
                    return $@"{PathForLogs}\Debug\";
                case LogLevel.Information:
                    return $@"{PathForLogs}\Info\";
                case LogLevel.Warning:
                    return $@"{PathForLogs}\Warning\";
                case LogLevel.Error:
                    return $@"{PathForLogs}\Error\";
                case LogLevel.Critical:
                    return $@"{PathForLogs}\Critical\";
                case LogLevel.None:
                    return $@"{PathForLogs}\None\";
            }
            return string.Empty;
        }
        // ToDo: Hay que reemplazarlo por el que esta en el nuget LDT pero antes hay que agregarle el try catch al Nuget
        private void CreateDirectory(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}

