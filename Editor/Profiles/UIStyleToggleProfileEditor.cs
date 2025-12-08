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
            isOnProp = serializedObject.FindProperty("isOn");
            toggleTransitionProp = serializedObject.FindProperty("toggleTransition");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawProfileEditor();
            DrawSelectableFields();
            EditorGUILayout.PropertyField(isOnProp);
            EditorGUILayout.PropertyField(toggleTransitionProp);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
