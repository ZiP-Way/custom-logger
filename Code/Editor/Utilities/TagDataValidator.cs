using System.Collections.Generic;
using Logger.Data;

namespace Logger.Editor
{
  public static class TagDataValidator
  {
    public static bool IsTagNameValid(IReadOnlyList<TagData> tagsData)
    {
      foreach (TagData tagData in tagsData)
      {
        if (!IsTagNameValid(tagData.Tag))
          return false;
      }

      return true;
    }
    
    public static bool IsTagNameValid(string name)
    {
      if (string.IsNullOrEmpty(name) || char.IsDigit(name[0]))
        return false;

      foreach (char c in name)
      {
        if (!char.IsLetterOrDigit(c) && c != '_')
          return false;
      }

      return true;
    }

    public static bool IsDataChanged(IReadOnlyList<TagData> originalTagsData, IReadOnlyList<TagData> newTagsData)
    {
      if (originalTagsData.Count != newTagsData.Count)
        return true;
      
      for (int index = 0; index < originalTagsData.Count; index++)
      {
        TagData originalTagData = originalTagsData[index];
        TagData newTagData = newTagsData[index];
        
        if (originalTagData.Tag != newTagData.Tag || 
            originalTagData.BaseColor != newTagData.BaseColor ||
            originalTagData.IsEnabled != newTagData.IsEnabled)
          return true;
      }

      return false;
    }
    
    public static bool IsNeedToRebakeEnum(IReadOnlyList<TagData> originalTagsData, IReadOnlyList<TagData> newTagsData)
    {
      if (originalTagsData.Count != newTagsData.Count)
        return true;
      
      for (int index = 0; index < originalTagsData.Count; index++)
      {
        TagData originalTagData = originalTagsData[index];
        TagData newTagData = newTagsData[index];
        
        if (originalTagData.Tag != newTagData.Tag)
          return true;
      }

      return false;
    }
  }
}