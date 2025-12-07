using UnityEditor;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleCardinalLayoutGroupProfile))]
    public class UIStyleCardinalLayoutGroupProfileEditor : UIStyleProfileEditor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            base.OnInspectorGUI();
            DrawPropertiesExcluding(serializedObject, "m_Script", nameof(UIStyleProfile.profileName));
            serializedObject.ApplyModifiedProperties();
        }
    }
}
