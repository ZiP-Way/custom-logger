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

    public void Message(string message) =>
      ManualMessage(LogType.Log, message);

    public void Message(LogTag tag, string message) =>
      ManualMessage(LogType.Log, tag, message);

    public void Warning(string message) =>
      ManualMessage(LogType.Warning, message);

    public void Warning(LogTag tag, string message) =>
      ManualMessage(LogType.Warning, tag, message);

    public void Error(string message) =>
      ManualMessage(LogType.Error, message);

    public void Error(LogTag tag, string message) =>
      ManualMessage(LogType.Error, tag, message);

    public void ManualMessage(LogType logType, string message) =>
      InternalLogger.Log(logType, message);

    public void ManualMessage(LogType logType, LogTag tag, string message)
    {
      if (!_loggerConfigurer.IsTagEnabled(tag))
        return;
      
      InternalLogger.Log(logType, _messageFormatter.FormatMessage(tag, message));
    }
  }
}