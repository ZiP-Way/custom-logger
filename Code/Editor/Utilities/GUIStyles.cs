using UnityEngine;

namespace Logger.Editor.Utilities
{
  public class GUIStyles
  {
    public GUIStyle HeaderStyle
    {
      get
      {
        if (_headerStyle == null)
          _headerStyle = GenerateHeaderStyle();

        return _headerStyle;
      }
    }

    #region Fields

    private GUIStyle _headerStyle;

    #endregion

    private GUIStyle GenerateHeaderStyle()
    {
      GUIStyle guiStyle = new GUIStyle();

      guiStyle.normal.textColor = Color.white;
      guiStyle.fontSize = 20;
      guiStyle.fontStyle = FontStyle.Bold;
      guiStyle.alignment = TextAnchor.MiddleCenter;

      return guiStyle;
    }
  }
}