using UnityEditor;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleLayoutElementProfile))]
    public class UIStyleLayoutElementProfileEditor : UIStyleProfileEditor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawProfileEditor();
            DrawPropertiesExcluding(serializedObject, "m_Script", nameof(UIStyleProfile.profileName));
            serializedObject.ApplyModifiedProperties();
        }
    }
}
