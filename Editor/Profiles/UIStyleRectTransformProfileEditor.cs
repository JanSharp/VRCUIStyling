using UnityEditor;
using UnityEngine;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleRectTransformProfile))]
    public class UIStyleRectTransformProfileEditor : UIStyleProfileEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                GUILayout.Label("Uses the Rect Transform component as the profile definition.", EditorStyles.wordWrappedLabel);
        }
    }
}
