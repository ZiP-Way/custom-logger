using UnityEngine;

namespace Logger.Types
{
  public interface ILogger
  {
    void Message(string message);
    void Message(LogTag tag, string message);
    void Warning(string message);
    void Warning(LogTag tag, string message);
    void Error(string message);
    void Error(LogTag tag, string message);
    void ManualMessage(LogType logType, string message);
    void ManualMessage(LogType logType, LogTag tag, string message);
  }
}