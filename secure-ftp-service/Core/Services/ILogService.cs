namespace secure_ftp_service.Core.Services
{
    /// <summary>
    /// It define the various logs. 
    /// </summary>
    /// <returns>interface</returns>
    public interface ILogService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void LogDebug(string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void LogInfo(string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void LogError(string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void LogCritical(string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void LogWarning(string message);
    }
}
