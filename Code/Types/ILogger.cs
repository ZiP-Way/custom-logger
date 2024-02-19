using UnityEngine;

namespace Logger.Types
{
  public interface ILogger
  {
    void Log(string message);
    void Log(LogTag tag, string message);
    void LogWarning(string message);
    void LogWarning(LogTag tag, string message);
    void LogError(string message);
    void LogError(LogTag tag, string message);
    void LogManual(LogType logType, string message);
    void LogManual(LogType logType, LogTag tag, string message);
  }
}