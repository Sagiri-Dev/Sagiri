using System;
using System.IO;
using System.Text;

using Sagiri.Exceptions;

namespace Sagiri.Util.Common
{
    /// <summary>
    /// Logger
    /// </summary>
    public class Logger : IDisposable
    {
        #region Field Variable

        private static readonly Logger _Instance = new();

        #endregion

        #region Enums

        public enum LogLevel
        {
            Debug = 1,
            Info = 2,
            Warn = 3,
            Error = 4,
            Fatal = 5,
            None = 9
        }

        public enum LogWriteModeType
        {
            Append,
            Overwrite
        }

        public enum LogFormatFileNameType
        {
            YYYYMMDD,
            YYYYMMDDHHMMSS,
            YYYYMMDDHHMMSSFFF,
            None
        }

        #endregion Enums

        #region Properties

        public static Logger GetInstance => _Instance;

        public string LogFileDirectory { get; set; }

        public string LogFileName { get; set; }

        public LogWriteModeType LogWriteMode { get; set; }

        public LogFormatFileNameType LogFormatFileName { get; set; }

        public LogLevel LogOutputLevel { get; set; }

        public string Encode { get; set; }

        #endregion Properties

        #region Constructor

        private Logger()
        {
            LogFileDirectory = AppDomain.CurrentDomain.BaseDirectory + "log";
            LogFileName = "Sagiri-Log";
            LogWriteMode = LogWriteModeType.Append;
            LogFormatFileName = LogFormatFileNameType.YYYYMMDD;
            LogOutputLevel = LogLevel.Info;
            Encode = "utf-8";
            this.WriteLog("############ Logger Constructor called. ###############", LogLevel.Debug);
        }

        #endregion Constructor

        #region Private Methods

        private bool _CanWriteLog(LogLevel logLevel) => LogOutputLevel switch
        {
            LogLevel.Debug => true,
            LogLevel.Info => logLevel >= LogLevel.Info,
            LogLevel.Warn => logLevel >= LogLevel.Warn,
            LogLevel.Error => logLevel >= LogLevel.Error,
            LogLevel.Fatal => logLevel == LogLevel.Fatal,
            _ => false
        };

        private string _CreateLogString(string logMessage, LogLevel logLevel)
        {
            var logTemplate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff")
                + "\t"
                + (" 【" + logLevel.ToString() + "】").PadRight(7, ' ')
                + "\t"
                + "{0}"
                + "\r\n";

            return string.Format(logTemplate, logMessage);
        }

        private string _CreateLogFile(string logFileName) => LogFormatFileName switch
        {
            LogFormatFileNameType.YYYYMMDD => $"{logFileName}_{DateTime.Now:yyyyMMdd}.log",
            LogFormatFileNameType.YYYYMMDDHHMMSS => $"{logFileName}_{DateTime.Now:yyyyMMddHHmmss}.log",
            LogFormatFileNameType.YYYYMMDDHHMMSSFFF => $"{logFileName}_{DateTime.Now:yyyyMMddHHmmssfff}.log",
            _ => throw new SagiriException($"Not expected type value"),
        };

        #endregion Private Methods

        #region Public Methods

        public bool WriteLog(string logMessage, LogLevel logLevel)
        {
            try
            {
                if (!Directory.Exists(LogFileDirectory))
                    Directory.CreateDirectory(LogFileDirectory);

                if (_CanWriteLog(logLevel))
                {
                    using StreamWriter stream = new(
                        Path.Combine(LogFileDirectory, _CreateLogFile(LogFileName)),
                        Convert.ToBoolean(LogWriteMode == LogWriteModeType.Append),
                        Encoding.GetEncoding(Encode)
                    );

                    var logString = _CreateLogString(logMessage, logLevel);
                    stream.Write(logString);
                }

                return true;
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("############ Logger WriteLog Failed... ###############");
                return false;
            }
        }

        public void SetLogLevel(string logLevelStr) => LogOutputLevel = logLevelStr.ToLower() switch
        {
            "debug" => LogLevel.Debug,
            "info" => LogLevel.Info,
            "warn" => LogLevel.Warn,
            "error" => LogLevel.Error,
            "fatal" => LogLevel.Fatal,
            "none" => LogLevel.None,
            _ => LogLevel.Info
        };

        public void Dispose() { }

        #endregion Public Methods
    }
}
