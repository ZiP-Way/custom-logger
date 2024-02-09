using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Logger.Data
{
  [Serializable]
  public struct TagData
  {
    public string Tag;
    public bool IsEnabled;
    public Color BaseColor;

    public TagData(string name, bool isEnabled, Color color)
    {
      Tag = name;
      IsEnabled = isEnabled;
      BaseColor = color;
    }
  }

  public class LoggerConfiguration : ScriptableObject
  {
    [SerializeField]
    private List<TagData> _tagsData;

    #region Properties

    public IReadOnlyList<TagData> TagsData => _tagsData;

    #endregion

    public void UpdateAndSaveData(IReadOnlyList<TagData> tagsData)
    {
      _tagsData = new List<TagData>(tagsData);
      
      EditorUtility.SetDirty(this);

      AssetDatabase.SaveAssets();
      AssetDatabase.Refresh();
    }
  }
}