using UnityEditor;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleToggleProfile))]
    public class UIStyleToggleProfileEditor : UIStyleSelectableProfileEditor
    {
        [InitializeOnLoadMethod]
        public static void OnToggleAssemblyLoad()
        {
            RegisterSelectableColorSpriteFields<UIStyleToggleProfile>();
        }

        private SerializedProperty isOnProp;
        private SerializedProperty toggleTransitionProp;

        public override void OnEnable()
        {
            base.OnEnable();
            isOnProp = serializedObject.FindProperty(nameof(UIStyleToggleProfile.isOn));
            toggleTransitionProp = serializedObject.FindProperty(nameof(UIStyleToggleProfile.toggleTransition));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawProfileEditor();
            DrawSelectableFields();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(isOnProp);
            EditorGUILayout.PropertyField(toggleTransitionProp);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
