using System.Linq;
using UnityEditor;
using UnityEngine;

namespace JanSharp
{
    [CustomPropertyDrawer(typeof(UIStyleColorAttribute))]
    public class UIStyleColorDrawer : PropertyDrawer
    {
        private bool isInitialized = false;
        private string errorMsg = null;
        private UIStyleProfileContainer container = null;
        private string[] colorNames = new string[] { "" };

        private void Initialize(SerializedProperty property)
        {
            if (isInitialized)
                return;
            isInitialized = true;

            if (!UIStyleProfileUtil.TryGetContainerFromRoots(property.serializedObject.targetObjects.Cast<Component>(), out container, out errorMsg))
                return;
            if (!UIStyleProfileUtil.TryGetColorsNames(container, out colorNames, out errorMsg))
                return;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Initialize(property);
            UIStyleProfileUtil.DrawSelectorField(
                position,
                property,
                container != null ? null : (errorMsg ?? "UI Style Profile Container got destroyed."),
                colorNames);
        }
    }

    [CustomPropertyDrawer(typeof(UIStyleSpriteAttribute))]
    public class UIStyleSpriteDrawer : PropertyDrawer
    {
        private bool isInitialized = false;
        private string errorMsg = null;
        private UIStyleProfileContainer container = null;
        private string[] spriteNames = new string[] { "" };

        private void Initialize(SerializedProperty property)
        {
            if (isInitialized)
                return;
            isInitialized = true;

            if (!UIStyleProfileUtil.TryGetContainerFromRoots(property.serializedObject.targetObjects.Cast<Component>(), out container, out errorMsg))
                return;
            if (!UIStyleProfileUtil.TryGetSpriteNames(container, out spriteNames, out errorMsg))
                return;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Initialize(property);
            UIStyleProfileUtil.DrawSelectorField(
                position,
                property,
                container != null ? null : (errorMsg ?? "UI Style Profile Container got destroyed."),
                spriteNames);
        }
    }
}
