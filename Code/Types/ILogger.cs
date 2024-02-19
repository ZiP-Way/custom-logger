using UnityEngine;

namespace Logger.Types
{
  public interface ILogger
  {
    void Log(string message, Object context = null);
    void Log(LogTag tag, string message, Object context = null);
    void LogWarning(string message, Object context = null);
    void LogWarning(LogTag tag, string message, Object context = null);
    void LogError(string message, Object context = null);
    void LogError(LogTag tag, string message, Object context = null);
    void LogManual(LogType logType, object message, Object context = null);
    void LogManual(LogType logType, LogTag tag, object message, Object context = null);
  }
}