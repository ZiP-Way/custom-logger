﻿using Logger.Data;
using Logger.Types;
using Logger.Utilities.Configurer;
using Logger.Utilities.MessageFormatter;
using UnityEngine;
using ILogger = Logger.Types.ILogger;

namespace Logger
{
  public static class DLogger
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

    public static void Log(string message) =>
      Logger.Log(message);

    public static void Log(LogTag tag, string message) =>
      LogManual(LogType.Log, tag, message);

    public static void LogWarning(string message) =>
      Logger.LogWarning(message);

    public static void LogWarning(LogTag tag, string message) =>
      LogManual(LogType.Warning, tag, message);

    public static void LogError(string message) =>
      Logger.LogError(message);

    public static void LogError(LogTag tag, string message) =>
      LogManual(LogType.Error, tag, message);

    public static void LogManual(LogType logType, string message) =>
      Logger.LogManual(logType, message);

    public static void LogManual(LogType logType, LogTag tag, string message) =>
      Logger.LogManual(logType, tag, message);
    
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