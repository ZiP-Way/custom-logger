namespace Logger.Utilities.MessageFormatter
{
  public interface IMessageFormatter
  {
    string FormatMessage(string message);
    string FormatMessage(LogTag tag, string message);
  }
}