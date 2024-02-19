using System;
using System.Collections.Generic;
using System.Linq;
using Logger.Data;

namespace Logger.Utilities.Configurer
{
  public class LoggerConfigurer : ILoggerConfigurer
  {
    #region Fields

    private readonly LoggerConfiguration _loggerConfiguration;

    private HashSet<int> _enabledTagIDs;
    
    #endregion
    
    public LoggerConfigurer(LoggerConfiguration loggerConfiguration) => 
      _loggerConfiguration = loggerConfiguration;

    public void BakeTags()
    {
      HashSet<int> enabledTagsIDs = new HashSet<int>(_loggerConfiguration.TagsData.Count);
      
      foreach (TagData tagData in _loggerConfiguration.TagsData)
      {
        if(tagData.IsEnabled)
          enabledTagsIDs.Add((int)Enum.Parse<LogTag>(tagData.Tag));
      }

      _enabledTagIDs = enabledTagsIDs;
    }

    public void EnableTag(LogTag tag)
    {
      if (IsTagEnabled(tag))
      {
        DLogger.LogWarning($"Tag `{tag}` already enabled");
        return;
      }

      _enabledTagIDs.Add((int)tag);
    }

    public void DisableTag(LogTag tag)
    {
      if (!IsTagEnabled(tag))
      {
        DLogger.LogWarning($"Tag `{tag}` already disabled");
        return;
      }

      _enabledTagIDs.Remove((int)tag);
    }

    public bool IsTagEnabled(LogTag tag) => 
      _enabledTagIDs.Contains((int)tag);

    public TagData GetData(LogTag tag) => 
      _loggerConfiguration.TagsData.First(data => data.Tag == tag.ToString());
  }
}