using Logger.Data;

namespace Logger.Utilities.Configurer
{
  public interface ILoggerConfigurer
  {
    void BakeTags();
    void EnableTag(LogTag tag);
    void DisableTag(LogTag tag);
    bool IsTagEnabled(LogTag tag);
    TagData GetData(LogTag tag);
  }
}