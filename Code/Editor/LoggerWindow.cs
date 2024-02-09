using Logger.Data;
using Logger.Editor.Utilities;
using UnityEditor;
using UnityEngine;

namespace Logger.Editor
{
  public class LoggerWindow : EditorWindow
  {
    #region Fields

    private Rect _headerSection;
    private Rect _bodySection;

    private LoggerConfiguration _loggerConfiguration;
    private TagsConfigurationTable _tagsConfigurationTable;
    
    private GUIStyles _guiStyles;

    #endregion
    
    [MenuItem("Window/Logger Configurations")]
    public static void ShowWindow() => 
      GetWindow<LoggerWindow>("Logger");

    private void OnEnable()
    {
      _loggerConfiguration = Resources.Load<LoggerConfiguration>(AssetPaths.LoggerConfigurationPath);
      _guiStyles = new GUIStyles();
      
      _tagsConfigurationTable = new TagsConfigurationTable(_loggerConfiguration);
      _tagsConfigurationTable.OnEnable();
    }
    
    public void OnDisable()
    {
      _tagsConfigurationTable.OnDisable();
    }

    public void OnGUI()
    {
      DrawLayouts();
      DrawHeader();
      DrawBody();
    }

    private void DrawLayouts()
    {
      float dpiScale = EditorGUIUtility.pixelsPerPoint;

      _headerSection.x = 0;
      _headerSection.y = 0;
      _headerSection.width = Screen.width / dpiScale;
      _headerSection.height = 50;

      GUI.DrawTexture(_headerSection, LoggerUtilities.GetHeaderTexture());

      _bodySection.x = 0;
      _bodySection.y = 50;
      _bodySection.width = Screen.width / dpiScale;
      _bodySection.height = Screen.height;

      GUI.DrawTexture(_bodySection, LoggerUtilities.GetBodyTexture());
    }

    private void DrawHeader()
    {
      GUILayout.BeginArea(_headerSection);

      GUILayout.FlexibleSpace();
      GUILayout.Label("Logger Configurations", _guiStyles.HeaderStyle);
      GUILayout.FlexibleSpace();

      GUILayout.EndArea();
    }

    private void DrawBody()
    {
      GUILayout.BeginArea(_bodySection);
      _tagsConfigurationTable.Draw(_bodySection);
      GUILayout.EndArea();
    }
  }
}