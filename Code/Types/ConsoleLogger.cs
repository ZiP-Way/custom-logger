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

    public void Log(string message, Object context = null) =>
      LogManual(LogType.Log, message, context);

    public void Log(LogTag tag, string message, Object context = null) =>
      LogManual(LogType.Log, tag, message, context);

    public void LogWarning(string message, Object context = null) =>
      LogManual(LogType.Warning, message, context);

    public void LogWarning(LogTag tag, string message, Object context = null) =>
      LogManual(LogType.Warning, tag, message, context);

    public void LogError(string message, Object context = null) =>
      LogManual(LogType.Error, message, context);

    public void LogError(LogTag tag, string message, Object context = null) =>
      LogManual(LogType.Error, tag, message, context);

    public void LogManual(LogType logType, object message, Object context = null) => 
      InternalLogger.Log(logType, message, context);

    public void LogManual(LogType logType, LogTag tag, object message , Object context = null)
    {
      if (!_loggerConfigurer.IsTagEnabled(tag))
        return;

      message = _messageFormatter.FormatMessage(tag, message as string);
      InternalLogger.Log(logType, message, context);
    }
  }
}