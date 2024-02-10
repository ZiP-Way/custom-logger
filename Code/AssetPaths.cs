using UnityEditor;

namespace Logger
{
  public static class AssetPaths
  {
    public static readonly string LoggerConfigurationPath = "LoggerConfiguration";
    
    public static string GetAssetPath(string assetName, string fileType, string filter)
    {
      string[] guids = AssetDatabase.FindAssets(assetName + filter);
      
      foreach (string guid in guids)
      {
        string assetPath = AssetDatabase.GUIDToAssetPath(guid);
        
        if (assetPath.EndsWith(assetName + fileType))
          return assetPath;
      }

      return null;
    }
  }
}