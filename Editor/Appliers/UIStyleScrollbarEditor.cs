using UnityEditor;
using UnityEngine.UI;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleScrollbar))]
    public class UIStyleScrollbarEditor : UIStyleApplierEditor<UIStyleScrollbarProfile>
    {
        [InitializeOnLoadMethod]
        public static void OnAssemblyLoad()
        {
            UIStyleRootUtil.RegisterApplyStyleFunc<UIStyleScrollbar, UIStyleScrollbarProfile, Scrollbar>(ApplyStyle);
        }

        private static void ApplyStyle(
            ValidationContext context,
            UIStyleScrollbar applier,
            UIStyleScrollbarProfile profile,
            Scrollbar target)
        {
            SerializedObject so = new(target);
            UIStyleSelectableEditor.ApplySelectableStyle(so, context, applier, profile);
            if (applier.controlDirection)
                so.FindProperty("m_Direction").intValue = (int)profile.direction;
            if (applier.controlValue)
                so.FindProperty("m_Value").floatValue = profile.value;
            if (applier.controlSize)
                so.FindProperty("m_Size").floatValue = profile.size;
            if (applier.controlNumberOfSteps)
                so.FindProperty("m_NumberOfSteps").intValue = profile.numberOfSteps;
            so.ApplyModifiedProperties();
        }

        /*
        m_HandleRect: {fileID: 2027199006}
        m_Direction: 0
        m_Value: 1
        m_Size: 0.99999994
        m_NumberOfSteps: 0
        m_OnValueChanged:
            m_PersistentCalls:
            m_Calls: []
        */
    }
}
