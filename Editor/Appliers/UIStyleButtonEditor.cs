using UnityEditor;
using UnityEngine.UI;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleButton))]
    public class UIStyleButtonEditor : UIStyleApplierEditor<UIStyleButtonProfile>
    {
        [InitializeOnLoadMethod]
        public static void OnAssemblyLoad()
        {
            UIStyleRootUtil.RegisterApplyStyleFunc<UIStyleButton, UIStyleButtonProfile, Button>(ApplyStyle);
        }

        private static void ApplyStyle(
            ValidationContext context,
            UIStyleButton applier,
            UIStyleButtonProfile profile,
            Button target)
        {
            SerializedObject so = new(target);
            UIStyleSelectableEditor.ApplySelectableStyle(so, context, applier, profile);
            so.ApplyModifiedProperties();
        }

        /*
          m_OnClick:
            m_PersistentCalls:
              m_Calls: []
        */
    }
}
