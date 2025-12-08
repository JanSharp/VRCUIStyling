using UnityEditor;
using UnityEngine.UI;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleToggle))]
    public class UIStyleToggleEditor : UIStyleApplierEditor<UIStyleToggleProfile>
    {
        [InitializeOnLoadMethod]
        public static void OnAssemblyLoad()
        {
            UIStyleRootUtil.RegisterApplyStyleFunc<UIStyleToggle, UIStyleToggleProfile, Toggle>(ApplyStyle);
        }

        private static void ApplyStyle(
            ValidationContext context,
            UIStyleToggle applier,
            UIStyleToggleProfile profile,
            Toggle target)
        {
            SerializedObject so = new(target);
            UIStyleSelectableEditor.ApplySelectableStyle(so, context, applier, profile);
            if (applier.controlIsOn)
                so.FindProperty("m_IsOn").boolValue = profile.isOn;
            if (applier.controlToggleTransition)
                so.FindProperty("toggleTransition").enumValueIndex = (int)profile.toggleTransition;
            so.ApplyModifiedProperties();
        }

        /*
          toggleTransition: 1
          graphic: {fileID: 370168402}
          m_Group: {fileID: 0}
          onValueChanged:
            m_PersistentCalls:
              m_Calls: []
          m_IsOn: 1
        */
    }
}
