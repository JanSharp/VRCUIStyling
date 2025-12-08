using UnityEditor;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleScrollbarProfile))]
    public class UIStyleScrollbarProfileEditor : UIStyleSelectableProfileEditor
    {
        [InitializeOnLoadMethod]
        public static void OnScrollbarAssemblyLoad()
        {
            RegisterSelectableColorSpriteFields<UIStyleScrollbarProfile>();
        }

        private SerializedProperty directionProp;
        private SerializedProperty valueProp;
        private SerializedProperty sizeProp;
        private SerializedProperty numberOfStepsProp;

        public override void OnEnable()
        {
            base.OnEnable();
            directionProp = serializedObject.FindProperty(nameof(UIStyleScrollbarProfile.direction));
            valueProp = serializedObject.FindProperty(nameof(UIStyleScrollbarProfile.value));
            sizeProp = serializedObject.FindProperty(nameof(UIStyleScrollbarProfile.size));
            numberOfStepsProp = serializedObject.FindProperty(nameof(UIStyleScrollbarProfile.numberOfSteps));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawProfileEditor();
            DrawSelectableFields();
            EditorGUILayout.PropertyField(directionProp);
            EditorGUILayout.PropertyField(valueProp);
            EditorGUILayout.PropertyField(sizeProp);
            EditorGUILayout.PropertyField(numberOfStepsProp);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
