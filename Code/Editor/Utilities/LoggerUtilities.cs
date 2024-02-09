using UnityEngine;

namespace Logger.Editor.Utilities
{
  public static class LoggerUtilities
  {
    #region Fields

    private static Texture2D _headerTexture;
    private static Color32 _headerColor = new Color32(40, 40, 40, 255);

    private static Texture2D _bodyTexture;
    private static Color32 _bodyColor;

    #endregion

    public static Texture2D GetHeaderTexture()
    {
      if (_headerTexture == null)
        _headerTexture = CreateTexture(1, 1, _headerColor);

      return _headerTexture;
    }

    public static Texture2D GetBodyTexture()
    {
      if (_bodyTexture == null)
        _bodyTexture = CreateTexture(1, 1, _bodyColor);

      return _bodyTexture;
    }
    
    private static Texture2D CreateTexture(int width, int height, Color color)
    {
      Texture2D texture2D = new Texture2D(width, height);
      texture2D.SetPixel(0, 0, color);
      texture2D.Apply();

      return texture2D;
    }
  }
}