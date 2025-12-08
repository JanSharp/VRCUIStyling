using UnityEditor;
using UnityEngine.UI;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleScrollRectProfile))]
    public class UIStyleScrollRectProfileEditor : UIStyleProfileEditor
    {
        private SerializedProperty horizontalProp;
        private SerializedProperty verticalProp;
        private SerializedProperty movementTypeProp;
        private SerializedProperty elasticityProp;
        private SerializedProperty inertiaProp;
        private SerializedProperty decelerationRateProp;
        private SerializedProperty scrollSensitivityProp;
        private SerializedProperty horizontalVisibilityProp;
        private SerializedProperty horizontalSpacingProp;
        private SerializedProperty verticalVisibilityProp;
        private SerializedProperty verticalSpacingProp;

        public override void OnEnable()
        {
            base.OnEnable();
            horizontalProp = serializedObject.FindProperty(nameof(UIStyleScrollRectProfile.horizontal));
            verticalProp = serializedObject.FindProperty(nameof(UIStyleScrollRectProfile.vertical));
            movementTypeProp = serializedObject.FindProperty(nameof(UIStyleScrollRectProfile.movementType));
            elasticityProp = serializedObject.FindProperty(nameof(UIStyleScrollRectProfile.elasticity));
            inertiaProp = serializedObject.FindProperty(nameof(UIStyleScrollRectProfile.inertia));
            decelerationRateProp = serializedObject.FindProperty(nameof(UIStyleScrollRectProfile.decelerationRate));
            scrollSensitivityProp = serializedObject.FindProperty(nameof(UIStyleScrollRectProfile.scrollSensitivity));
            horizontalVisibilityProp = serializedObject.FindProperty(nameof(UIStyleScrollRectProfile.horizontalVisibility));
            horizontalSpacingProp = serializedObject.FindProperty(nameof(UIStyleScrollRectProfile.horizontalSpacing));
            verticalVisibilityProp = serializedObject.FindProperty(nameof(UIStyleScrollRectProfile.verticalVisibility));
            verticalSpacingProp = serializedObject.FindProperty(nameof(UIStyleScrollRectProfile.verticalSpacing));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawProfileEditor();
            EditorGUILayout.PropertyField(horizontalProp);
            EditorGUILayout.PropertyField(verticalProp);
            EditorGUILayout.PropertyField(movementTypeProp);
            if (((ScrollRect.MovementType)movementTypeProp.intValue) == ScrollRect.MovementType.Elastic)
                using (new EditorGUI.IndentLevelScope())
                    EditorGUILayout.PropertyField(elasticityProp);
            EditorGUILayout.PropertyField(inertiaProp);
            if (inertiaProp.boolValue)
                using (new EditorGUI.IndentLevelScope())
                    EditorGUILayout.PropertyField(decelerationRateProp);
            EditorGUILayout.PropertyField(scrollSensitivityProp);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(horizontalVisibilityProp);
            if (((ScrollRect.ScrollbarVisibility)horizontalVisibilityProp.intValue) == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport)
                using (new EditorGUI.IndentLevelScope())
                    EditorGUILayout.PropertyField(horizontalSpacingProp);
            EditorGUILayout.PropertyField(verticalVisibilityProp);
            if (((ScrollRect.ScrollbarVisibility)verticalVisibilityProp.intValue) == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport)
                using (new EditorGUI.IndentLevelScope())
                    EditorGUILayout.PropertyField(verticalSpacingProp);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
