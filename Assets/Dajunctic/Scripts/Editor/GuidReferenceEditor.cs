using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Dajunctic
{
    [CustomPropertyDrawer(typeof(GuidReferenceAttribute))]
    public class GuidReferenceEditor: PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.LabelField(position, label.text, "Use string field");
                return;
            }

            var attr = (GuidReferenceAttribute)attribute;
            var targetType = attr.TargetType;

            EditorGUI.BeginProperty(position, label, property);

            var assets = GetConfigAssets(targetType);

            var totalWidth = position.width;
            var pingBtnWidth = attr.ShowPingButton ? 44f: 0f;
            var space = attr.ShowPingButton ? 4f: 0f;
            var dropdownWidth = totalWidth - pingBtnWidth - space;

            var labelRect = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, position.height);
            var pingRect = new Rect(position.x + dropdownWidth + space, position.y, pingBtnWidth, position.height);
            var dropdownRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y, dropdownWidth - EditorGUIUtility.labelWidth, position.height);

            EditorGUI.LabelField(labelRect, label);

            var currentValue = property.stringValue;
            var displayTitle = string.IsNullOrEmpty(currentValue) ? "None" : currentValue;

            if (EditorGUI.DropdownButton(dropdownRect, new GUIContent(displayTitle), FocusType.Keyboard))
            {
                var dropdown = new GuidReferenceDropdown(new AdvancedDropdownState(), assets, (selectItem) =>
                {
                    property.stringValue = selectItem.Id;
                    property.serializedObject.ApplyModifiedProperties();
                });

                dropdown.Show(dropdownRect);
            }
            

            if (attr.ShowPingButton)
            {
                var currentItem = assets.FirstOrDefault(x => x.Id == currentValue);
                EditorGUI.BeginDisabledGroup(currentItem == null);
                    
                if (GUI.Button(pingRect, new GUIContent("Ping"), EditorStyles.miniButton))
                {
                    
                    if (currentItem.Asset != null)
                    {
                        EditorGUIUtility.PingObject(currentItem.Asset);
                    }
                    
                }

                EditorGUI.EndDisabledGroup();
                
            }

            EditorGUI.EndProperty();
            
        }

        public class GuidReferenceDropdown : AdvancedDropdown
        {
            private readonly List<ConfigItem> _items;
            private readonly Action<ConfigItem> _onSelect;

            public GuidReferenceDropdown(AdvancedDropdownState state, List<ConfigItem> items, Action<ConfigItem> onSelect) : base(state)
            {
                _items = items;
                _onSelect = onSelect;
                minimumSize = new Vector2(250, 400);
            }

            protected override AdvancedDropdownItem BuildRoot()
            {
                var root = new AdvancedDropdownItem("Reference");

                var noneItem = new GuidReferenceItem("None", new ConfigItem("", null, ""));
                root.AddChild(noneItem);

                foreach (var item in _items)
                {
                    var dropdownItem = new GuidReferenceItem($"{item.Id}", item);
                    root.AddChild(dropdownItem);
                }

                return root;
            }

            protected override void ItemSelected(AdvancedDropdownItem item)
            {
                base.ItemSelected(item);

                if (item is GuidReferenceItem refItem)
                {
                    _onSelect?.Invoke(refItem.ConfigItem);
                }
            }
        }

        public class GuidReferenceItem: AdvancedDropdownItem
        {
            public ConfigItem ConfigItem {get; }
            public GuidReferenceItem(string name, ConfigItem configItem) : base(name)
            {
                ConfigItem = configItem;
            }   
        }



        List<ConfigItem> GetConfigAssets(Type targetType)
        {
            var list = new List<ConfigItem>();
            var filter = "t:" + targetType.Name;
            var guids = AssetDatabase.FindAssets(filter);

            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var obj = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

                if (obj != null && targetType.IsAssignableFrom(obj.GetType()))
                {
                    var id = obj.name;
                    if (obj is BaseConfig baseConfig && !string.IsNullOrEmpty(baseConfig.Id))
                    {
                        id = baseConfig.Id;
                    }

                    list.Add(new ConfigItem(id, obj, path));
                }
            }

            return list;
        }

        public class ConfigItem
        {
            public string Id;
            public UnityEngine.Object Asset;
            public string Path;

            public ConfigItem(string id, UnityEngine.Object asset, string path)
            {
                Id = id;
                Asset = asset;
                Path = path;
            }
        }
        
    }
}