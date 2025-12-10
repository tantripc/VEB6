using Serilog;

namespace MiddlewareTool.Logs
{
    /// <summary>
    /// File Logging
    /// </summary>
    public static class Logging
    {
        #region Properties

        /// <summary>
        /// Log engine
        /// </summary>
        private static readonly ILogger Logger = Log.ForContext(typeof(Logging));

        #endregion

        #region Methods

        /// <summary>
        /// Ghi log debug
        /// </summary>
        /// <param name="message">Nội dung log</param>
        public static void LogDebug(string message)
        {
            Logger.Debug(message);
        }

        /// <summary>
        /// Ghi log debug
        /// </summary>
        /// <param name="message">Nội dung log</param>
        /// <param name="exception">Đối tượng Exception</param>
        public static void LogDebug(string message, Exception exception)
        {
            Logger.Debug(message, exception);
        }

        /// <summary>
        /// Ghi log lỗi
        /// </summary>
        /// <param name="message">Nội dung lỗi</param>
        public static void LogError(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                // Replace pattern-breaking characters
                message = message.Replace('\n', '_').Replace('\r', '_').Replace('\t', '_');
                Logger.Error(message);
            }

        }

        /// <summary>
        /// Ghi log lỗi
        /// </summary>
        /// <param name="message">Nội dung lỗi</param>
        /// <param name="exception">Đối tượng Exception</param>
        public static void LogError(string message, Exception exception)
        {
            Logger.Error(message, exception);
        }

        /// <summary>
        /// Ghi log lỗi
        /// </summary>
        /// <param name="message">Nội dung lỗi</param>
        public static void LogFatal(string message)
        {
            Logger.Fatal(message);
        }

        /// <summary>
        /// Ghi log lỗi
        /// </summary>
        /// <param name="message">Nội dung lỗi</param>
        /// <param name="exception">Đối tượng Exception</param>
        public static void LogFatal(string message, Exception exception)
        {
            Logger.Fatal(message, exception);
        }

        /// <summary>
        /// Ghi log thông tin
        /// </summary>
        /// <param name="message">Nội dung</param>
        public static void LogInfo(string message)
        {
            Logger.Information(message);
        }

        /// <summary>
        /// Ghi log thông tin
        /// </summary>
        /// <param name="message">Nội dung</param>
        /// <param name="exception">Đối tượng Exception</param>
        public static void LogInfo(string message, Exception exception)
        {
            Logger.Information(message, exception);
        }

        /// <summary>
        /// Ghi log cảnh báo
        /// </summary>
        /// <param name="message">Nội dung cảnh báo</param>
        public static void LogWarn(string message)
        {
            Logger.Warning(message);
        }

        /// <summary>
        /// Ghi log cảnh báo
        /// </summary>
        /// <param name="message">Nội dung cảnh báo</param>
        /// <param name="exception">Đối tượng Exception</param>
        public static void LogWarn(string message, Exception exception)
        {
            Logger.Warning(message, exception);
        }

        #endregion
    }
}
