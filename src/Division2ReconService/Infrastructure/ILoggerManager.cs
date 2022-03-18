namespace Division2ReconService.Infrastructure
{
    /// <summary>
    ///  Interface Logger Manager
    /// </summary>
    public interface ILoggerManager
    {
        void LogDebug(string message);
        void LogError(string message);
        void LogInfo(string message);
        void LogWarn(string message);

    }
}
