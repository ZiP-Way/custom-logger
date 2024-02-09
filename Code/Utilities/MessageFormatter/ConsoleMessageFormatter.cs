using Logger.Data;
using Logger.Utilities.Configurer;
using UnityEngine;

namespace Logger.Utilities.MessageFormatter
{
  public class ConsoleMessageFormatter : IMessageFormatter
  {
    private readonly ILoggerConfigurer _loggerConfigurer;

    #region Fields

    private readonly LoggerConfiguration _loggerConfiguration;

    #endregion

    public ConsoleMessageFormatter(ILoggerConfigurer loggerConfigurer) => 
      _loggerConfigurer = loggerConfigurer;

    public string FormatMessage(string message) => 
      message;

    public string FormatMessage(LogTag tag, string message)
    {
      TagData tagData = _loggerConfigurer.GetData(tag);
      string HTMLColor = ColorUtility.ToHtmlStringRGBA(tagData.BaseColor);
      
      return $"<color=#{HTMLColor}>[{tag}] {message}</color>";
    }
  }
}