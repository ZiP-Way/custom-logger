using Logger.Utilities.Configurer;
using Logger.Utilities.MessageFormatter;
using UnityEngine;

namespace Logger.Types
{
  public class ConsoleLogger : ILogger
  {
    private UnityEngine.ILogger InternalLogger => Debug.unityLogger;

    #region Fields

    private readonly IMessageFormatter _messageFormatter;
    private readonly ILoggerConfigurer _loggerConfigurer;

    #endregion

    public ConsoleLogger(IMessageFormatter messageFormatter, ILoggerConfigurer loggerConfigurer)
    {
      _messageFormatter = messageFormatter;
      _loggerConfigurer = loggerConfigurer;
    }

    public void Log(string message) =>
      LogManual(LogType.Log, message);

    public void Log(LogTag tag, string message) =>
      LogManual(LogType.Log, tag, message);

    public void LogWarning(string message) =>
      LogManual(LogType.Warning, message);

    public void LogWarning(LogTag tag, string message) =>
      LogManual(LogType.Warning, tag, message);

    public void LogError(string message) =>
      LogManual(LogType.Error, message);

    public void LogError(LogTag tag, string message) =>
      LogManual(LogType.Error, tag, message);

    public void LogManual(LogType logType, string message) =>
      InternalLogger.Log(logType, message);

    public void LogManual(LogType logType, LogTag tag, string message)
    {
      if (!_loggerConfigurer.IsTagEnabled(tag))
        return;
      
      InternalLogger.Log(logType, _messageFormatter.FormatMessage(tag, message));
    }
  }
}