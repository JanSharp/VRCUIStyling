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
            horizontalProp = serializedObject.FindProperty("horizontal");
            verticalProp = serializedObject.FindProperty("vertical");
            movementTypeProp = serializedObject.FindProperty("movementType");
            elasticityProp = serializedObject.FindProperty("elasticity");
            inertiaProp = serializedObject.FindProperty("inertia");
            decelerationRateProp = serializedObject.FindProperty("decelerationRate");
            scrollSensitivityProp = serializedObject.FindProperty("scrollSensitivity");
            horizontalVisibilityProp = serializedObject.FindProperty("horizontalVisibility");
            horizontalSpacingProp = serializedObject.FindProperty("horizontalSpacing");
            verticalVisibilityProp = serializedObject.FindProperty("verticalVisibility");
            verticalSpacingProp = serializedObject.FindProperty("verticalSpacing");
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
