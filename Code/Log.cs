using Logger.Data;
using Logger.Types;
using Logger.Utilities.Configurer;
using Logger.Utilities.MessageFormatter;
using UnityEngine;
using ILogger = Logger.Types.ILogger;

namespace Logger
{
  public static class Log
  {
    #region Properties

    public static ILogger Logger
    {
      get
      {
        if (_logger == null)
          InitializeLogger();

        return _logger;
      }
    }

    public static ILoggerConfigurer LoggerConfigurer
    {
      get
      {
        if (_loggerConfigurer == null)
          InitializeLogger();

        return _loggerConfigurer;
      }
    }

    #endregion

    #region Fields

    private static ILogger _logger;
    private static ILoggerConfigurer _loggerConfigurer;

    #endregion
    
    public static void EnableTag(LogTag tag) =>
      LoggerConfigurer.EnableTag(tag);

    public static void DisableTag(LogTag tag) =>
      LoggerConfigurer.DisableTag(tag);

    public static bool IsTagEnabled(LogTag tag) =>
      LoggerConfigurer.IsTagEnabled(tag);

    public static void Message(string message) =>
      Logger.Message(message);

    public static void Message(LogTag tag, string message) =>
      ManualMessage(LogType.Log, tag, message);

    public static void Warning(string message) =>
      Logger.Warning(message);

    public static void Warning(LogTag tag, string message) =>
      ManualMessage(LogType.Warning, tag, message);

    public static void Error(string message) =>
      Logger.Error(message);

    public static void Error(LogTag tag, string message) =>
      ManualMessage(LogType.Error, tag, message);

    public static void ManualMessage(LogType logType, string message) =>
      Logger.ManualMessage(logType, message);

    public static void ManualMessage(LogType logType, LogTag tag, string message) =>
      Logger.ManualMessage(logType, tag, message);
    
    private static void InitializeLogger()
    {
      LoggerConfiguration loggerConfiguration = Resources.Load<LoggerConfiguration>(AssetPaths.LoggerConfigurationPath);
      ILoggerConfigurer loggerConfigurer = new LoggerConfigurer(loggerConfiguration);
      IMessageFormatter messageFormatter = new ConsoleMessageFormatter(loggerConfigurer);
      ILogger logger = new ConsoleLogger(messageFormatter, loggerConfigurer);

      loggerConfigurer.BakeTags();
        
      _loggerConfigurer = loggerConfigurer;
      _logger = logger;
    }
  }
}