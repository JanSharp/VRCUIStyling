using System.Linq;
using UnityEditor;
using UnityEngine;

namespace JanSharp
{
    // [CanEditMultipleObjects]
    [CustomEditor(typeof(UIButtonStyle))]
    public class UIButtonStyleEditor : Editor
    {
        private UIColorPool colorPool;
        private string[] colorNames;

        private SerializedProperty normalColorProp;
        private SerializedProperty highlightedColorProp;
        private SerializedProperty pressedColorProp;
        private SerializedProperty selectedColorProp;
        private SerializedProperty disabledColorProp;

        public void OnEnable()
        {
            UIStyledContainer container = ((UIButtonStyle)target).GetComponentInParent<UIStyledContainer>(includeInactive: true);
            colorPool = container?.colorPool;
            colorNames = new string[] { "" }.Union(colorPool.colors.Select(e => e.name).Distinct()).ToArray();

            normalColorProp = serializedObject.FindProperty("normalColor");
            highlightedColorProp = serializedObject.FindProperty("highlightedColor");
            pressedColorProp = serializedObject.FindProperty("pressedColor");
            selectedColorProp = serializedObject.FindProperty("selectedColor");
            disabledColorProp = serializedObject.FindProperty("disabledColor");
        }

        public override void OnInspectorGUI()
        {
            if (colorPool == null)
            {
                serializedObject.Update();
                DrawPropertiesExcluding(serializedObject, "m_Script");
                serializedObject.ApplyModifiedProperties();
                return;
            }

            serializedObject.Update();

            DrawColorSelector(normalColorProp);
            DrawColorSelector(highlightedColorProp);
            DrawColorSelector(pressedColorProp);
            DrawColorSelector(selectedColorProp);
            DrawColorSelector(disabledColorProp);

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawColorSelector(SerializedProperty prop)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.PropertyField(prop);
                int index = EditorGUILayout.Popup(0, colorNames, GUILayout.Width(20f));
                if (index != 0)
                    prop.stringValue = colorNames[index];
            }
        }
    }
}
