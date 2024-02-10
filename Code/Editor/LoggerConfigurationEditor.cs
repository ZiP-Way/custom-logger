using Logger.Data;
using UnityEditor;
using UnityEngine;

namespace Logger.Editor
{
  [CustomEditor(typeof(LoggerConfiguration))]
  public class LoggerConfigurationEditor : UnityEditor.Editor
  {
    // public override void OnInspectorGUI()
    // {
    //   GUILayout.Label("It is recommended that this asset be edited only via the Logger Configuration window.",
    //     EditorStyles.wordWrappedLabel);
    //   
    //   EditorGUILayout.Separator();
    //   
    //   if (GUILayout.Button("Open Logger Configuration window")) 
    //     LoggerWindow.ShowWindow();
    // }
  }
}