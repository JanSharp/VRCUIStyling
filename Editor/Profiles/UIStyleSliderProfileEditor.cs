using UnityEditor;
using UnityEngine;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleSliderProfile))]
    public class UIStyleSliderProfileEditor : UIStyleSelectableProfileEditor
    {
        [InitializeOnLoadMethod]
        public static void OnSliderAssemblyLoad()
        {
            RegisterSelectableColorSpriteFields<UIStyleSliderProfile>();
        }

        private SerializedProperty directionProp;
        private SerializedProperty minValueProp;
        private SerializedProperty maxValueProp;
        private SerializedProperty wholeNumbersProp;
        private SerializedProperty valueProp;

        public override void OnEnable()
        {
            base.OnEnable();
            directionProp = serializedObject.FindProperty(nameof(UIStyleSliderProfile.direction));
            minValueProp = serializedObject.FindProperty(nameof(UIStyleSliderProfile.minValue));
            maxValueProp = serializedObject.FindProperty(nameof(UIStyleSliderProfile.maxValue));
            wholeNumbersProp = serializedObject.FindProperty(nameof(UIStyleSliderProfile.wholeNumbers));
            valueProp = serializedObject.FindProperty(nameof(UIStyleSliderProfile.value));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawProfileEditor();
            DrawSelectableFields();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(directionProp);
            DrawMinValueField();
            DrawMaxValueField();
            DrawWholeNumbersField();
            DrawValueSlider();
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawMinValueField()
        {
            using var scope = new EditorGUI.ChangeCheckScope();
            float prevValue = minValueProp.floatValue;
            DrawMinMaxField(minValueProp);
            if (scope.changed && minValueProp.floatValue >= maxValueProp.floatValue)
                minValueProp.floatValue = prevValue;
        }

        private void DrawMaxValueField()
        {
            using var scope = new EditorGUI.ChangeCheckScope();
            float prevValue = maxValueProp.floatValue;
            DrawMinMaxField(maxValueProp);
            if (scope.changed && maxValueProp.floatValue <= minValueProp.floatValue)
                maxValueProp.floatValue = prevValue;
        }

        private void DrawMinMaxField(SerializedProperty prop)
        {
            Rect rect = EditorGUILayout.GetControlRect(hasLabel: true);
            using (new EditorGUI.PropertyScope(rect, label: null, prop))
            {
                EditorGUI.PrefixLabel(rect, new GUIContent(prop.displayName));
                rect.x += EditorGUIUtility.labelWidth;
                rect.width -= EditorGUIUtility.labelWidth;
                if (wholeNumbersProp.boolValue)
                    prop.floatValue = EditorGUI.DelayedIntField(rect, (int)prop.floatValue);
                else
                    prop.floatValue = EditorGUI.DelayedFloatField(rect, prop.floatValue);
            }
        }

        private void DrawWholeNumbersField()
        {
            using var scope = new EditorGUI.ChangeCheckScope();
            EditorGUILayout.PropertyField(wholeNumbersProp);
            if (!scope.changed && !wholeNumbersProp.boolValue)
                return;
            // It changed from float to int.
            int min = (int)minValueProp.floatValue;
            int max = (int)maxValueProp.floatValue;
            if (max <= min)
                max = min + 1;
            minValueProp.floatValue = min;
            maxValueProp.floatValue = max;
        }

        private void DrawValueSlider()
        {
            Rect rect = EditorGUILayout.GetControlRect(hasLabel: true);
            using (new EditorGUI.PropertyScope(rect, label: null, valueProp))
            {
                EditorGUI.PrefixLabel(rect, new GUIContent(valueProp.displayName));
                rect.x += EditorGUIUtility.labelWidth;
                rect.width -= EditorGUIUtility.labelWidth;
                if (wholeNumbersProp.boolValue)
                    valueProp.floatValue = EditorGUI.IntSlider(
                        rect,
                        (int)valueProp.floatValue,
                        (int)minValueProp.floatValue,
                        (int)maxValueProp.floatValue);
                else
                    valueProp.floatValue = EditorGUI.Slider(
                        rect,
                        valueProp.floatValue,
                        minValueProp.floatValue,
                        maxValueProp.floatValue);
            }
        }
    }
}
