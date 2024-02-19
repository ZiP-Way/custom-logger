using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Logger.Data;
using UnityEditor;

namespace Logger.Editor.Utilities
{
  public class TagsSaver
  {
    #region Fields

    private readonly LoggerConfiguration _loggerConfiguration;

    #endregion

    public TagsSaver(LoggerConfiguration loggerConfiguration) => 
      _loggerConfiguration = loggerConfiguration;

    public void ApplyChanges(IReadOnlyList<TagData> tagsData)
    {
      if (TagDataValidator.IsNeedToRebakeEnum(_loggerConfiguration.TagsData, tagsData)) 
        BakeTagsEnum(tagsData);

      _loggerConfiguration.UpdateAndSaveData(tagsData);
    }
    
    private void BakeTagsEnum(IReadOnlyList<TagData> tagsData)
    {
      string assetPath = AssetPaths.GetAssetPath(nameof(LogTag), ".cs", " t:Script");

      if (String.IsNullOrWhiteSpace(assetPath))
      {
        DLogger.LogError($"Can`t bake tags, path to {nameof(LogTag)} is null or empty");
        return;
      }
      
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("namespace Logger");
      stringBuilder.AppendLine("{");
      stringBuilder.AppendLine("\tpublic enum LogTag");
      stringBuilder.AppendLine("\t{");

      for (int tagIndex = 0; tagIndex < tagsData.Count; tagIndex++)
      {
        TagData data = tagsData[tagIndex];
        stringBuilder.Append($"\t\t{data.Tag}");

        if (tagIndex != tagsData.Count - 1)
          stringBuilder.AppendLine(", ");
      }

      stringBuilder.AppendLine("\n\t}");
      stringBuilder.Append("}");

      File.WriteAllText(assetPath, stringBuilder.ToString());
      AssetDatabase.ImportAsset(assetPath);
    }
  }
}