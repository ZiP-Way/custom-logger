using System;
using System.Collections.Generic;
using Logger.Data;
using Logger.Editor.Utilities;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Logger.Editor
{
  public class TagsConfigurationTable
  {
    private const string ErrorText = "Error: Invalid tag name. Ensure a non-empty name, use only letters, numbers or `_` symbol.";
    
    #region Properties

    private IReadOnlyList<TagData> TagsData => (IReadOnlyList<TagData>)_tags.list;

    #endregion
    
    #region Fields

    private readonly LoggerConfiguration _loggerConfiguration;
    private readonly TagsSaver _tagsSaver;
    
    private readonly string[] _columnNames = new string[] { "", "Tag Name", "Base Color" };
    private readonly float[] _columnWidthsInPercentages = new float[] {2, 83, 15 };
    private readonly float[] _maxColumnWidths = new float[] { 15, float.MaxValue, float.MaxValue };
    
    private readonly float _columnPadding = 5f;
    private readonly float _paddingBetweenLines = 5f;
    
    private ReorderableList _tags;

    #endregion
    
    public TagsConfigurationTable(LoggerConfiguration loggerConfiguration)
    {
      _loggerConfiguration = loggerConfiguration;
      _tagsSaver = new TagsSaver(loggerConfiguration);
    }

    public void OnEnable()
    {
      _tags = InitializeList();
      SubscribeToCallbacks();
    }

    public void OnDisable() => 
      UnsubscribeFromCallbacks();

    public void Draw(Rect container)
    {
      _tags.DoLayoutList();
      
      IReadOnlyList<TagData> newTagsData = TagsData;
      bool isDataChanged = TagDataValidator.IsDataChanged(_loggerConfiguration.TagsData, newTagsData);
      bool isTagNameValid = TagDataValidator.IsTagNameValid(newTagsData);

      container.y = _tags.GetHeight() + _paddingBetweenLines;

      if (!isTagNameValid)
      {
        DrawErrorBox(container);
        container.y += EditorGUIUtility.singleLineHeight * 2 + _paddingBetweenLines; 
      }

      DrawApplyAndRevertButtons(container, isDataChanged, isTagNameValid);
    }

    private ReorderableList InitializeList() => 
      new(new List<TagData>(_loggerConfiguration.TagsData), typeof(TagData));

    private void SubscribeToCallbacks()
    {
      _tags.drawHeaderCallback += DrawListHeader;
      _tags.drawElementCallback += DrawElement;

      _tags.elementHeightCallback += SetElementHeight;

      _tags.onAddCallback += AddElement;
      _tags.onRemoveCallback += RemoveElement;
    }

    private void UnsubscribeFromCallbacks()
    {
      _tags.drawHeaderCallback -= DrawListHeader;
      _tags.drawElementCallback -= DrawElement;

      _tags.elementHeightCallback -= SetElementHeight;

      _tags.onAddCallback -= AddElement;
      _tags.onRemoveCallback -= RemoveElement;
    }

    private void DrawApplyAndRevertButtons(Rect container, bool isDataChanged, bool isTagNameValid)
    {
      Vector2 position = new Vector2(container.width, container.y);
      Vector2 size = new Vector2(120, EditorGUIUtility.singleLineHeight);

      GUI.enabled = isDataChanged && isTagNameValid;
      DrawButton(new Rect(position.x - 250, position.y, size.x, size.y), "Apply", ApplyChanges);
      GUI.enabled = isDataChanged;
      DrawButton(new Rect(position.x - 130, position.y, size.x, size.y), "Revert", RevertChanges);
      GUI.enabled = true;
    }

    private void ApplyChanges()
    {
      _tagsSaver.ApplyChanges(TagsData);
      GUI.FocusControl(null);
    }

    private void RevertChanges()
    {
      _tags.list.Clear();
      
      foreach (TagData tag in _loggerConfiguration.TagsData)
        _tags.list.Add(tag);
      
      GUI.FocusControl(null);
    }

    private void DrawListHeader(Rect rect)
    {
      rect.x += 15;
      rect.width -= 15;

      EditorGUI.LabelField(GetColumnRect(0, rect), _columnNames[0]);
      EditorGUI.LabelField(GetColumnRect(1, rect), _columnNames[1]);
      EditorGUI.LabelField(GetColumnRect(2, rect), _columnNames[2]);
    }

    private void DrawElement(Rect rect, int index, bool isactive, bool isfocused)
    {
      TagData tagData = (TagData)_tags.list[index];
      
      tagData.IsEnabled = EditorGUI.Toggle(GetColumnRect(0, rect), tagData.IsEnabled);
      tagData.BaseColor = EditorGUI.ColorField(GetColumnRect(2, rect), tagData.BaseColor);
      tagData.Tag = EditorGUI.TextField(GetColumnRect(1, rect),  tagData.Tag);

      _tags.list[index] = tagData;
    }

    private float SetElementHeight(int index) => 
      EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

    private void AddElement(ReorderableList list) => 
      _tags.list.Add(new TagData("NewTag", true, Color.white));

    private void RemoveElement(ReorderableList list) => 
      _tags.list.RemoveAt(list.index);

    private Rect GetColumnRect(int columnIndex, Rect parentRect)
    {
      Vector2 size = GetColumnSize(columnIndex, parentRect.size);
      Vector2 position = CalculateColumnPosition(columnIndex, parentRect);

      return new Rect(position, size);
    }

    private Vector2 CalculateColumnPosition(int columnIndex, Rect parentRect)
    {
      Vector2 position = parentRect.position;

      for (int i = 0; i < columnIndex; i++)
      {
        Vector2 previousColumnSize = GetColumnSize(i, parentRect.size);
        position.x += previousColumnSize.x + _columnPadding;
      }
      
      return position;
    }

    private Vector2 GetColumnSize(int columnIndex, Vector2 parentSize)
    {
      float width = parentSize.x / 100 * _columnWidthsInPercentages[columnIndex] - _columnPadding;

      if (width > _maxColumnWidths[columnIndex]) 
        width = _maxColumnWidths[columnIndex];

      return new Vector2(width, EditorGUIUtility.singleLineHeight);
    }
    
    private void DrawButton(Rect rect, string name, Action onClick)
    {
      if (GUI.Button(rect, name))
        onClick();
    }
    
    private void DrawErrorBox(Rect container)
    {
      float padding = container.width / 100 * 5;
      
      float x = container.x + padding;
      float y = container.y;

      float width = container.width - padding * 2;
      float height = EditorGUIUtility.singleLineHeight * 2;
      
      EditorGUI.HelpBox(new Rect(x, y, width, height), ErrorText, MessageType.Error);
    }
  }
}