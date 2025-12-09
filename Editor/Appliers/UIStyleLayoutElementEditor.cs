using UnityEditor;
using UnityEngine.UI;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleLayoutElement))]
    public class UIStyleLayoutElementEditor : UIStyleApplierEditor<UIStyleLayoutElementProfile>
    {
        [MenuItem("CONTEXT/" + nameof(LayoutElement) + "/Control Using UI Style", isValidateFunction: true)]
        public static bool AddApplierValidation(MenuCommand menuCommand)
            => UIStyleApplierUtil.ContextMenuAddApplierValidation<UIStyleLayoutElement>(menuCommand);

        [MenuItem("CONTEXT/" + nameof(LayoutElement) + "/Control Using UI Style")]
        public static void AddApplier(MenuCommand menuCommand)
            => UIStyleApplierUtil.ContextMenuAddApplier<UIStyleLayoutElement>(menuCommand);

        [InitializeOnLoadMethod]
        public static void OnAssemblyLoad()
        {
            UIStyleRootUtil.RegisterApplyStyleFunc<UIStyleLayoutElement, UIStyleLayoutElementProfile, LayoutElement>(ApplyStyle);
        }

        private static void ApplyStyle(
            ValidationContext context,
            UIStyleLayoutElement applier,
            UIStyleLayoutElementProfile profile,
            LayoutElement target)
        {
            SerializedObject so = new(target);
            if (applier.controlIgnoreLayout)
                so.FindProperty("m_IgnoreLayout").boolValue = profile.ignoreLayout;
            if (applier.controlMinWidth)
                so.FindProperty("m_MinWidth").floatValue = profile.minWidth;
            if (applier.controlMinHeight)
                so.FindProperty("m_MinHeight").floatValue = profile.minHeight;
            if (applier.controlPreferredWidth)
                so.FindProperty("m_PreferredWidth").floatValue = profile.preferredWidth;
            if (applier.controlPreferredHeight)
                so.FindProperty("m_PreferredHeight").floatValue = profile.preferredHeight;
            if (applier.controlFlexibleWidth)
                so.FindProperty("m_FlexibleWidth").floatValue = profile.flexibleWidth;
            if (applier.controlFlexibleHeight)
                so.FindProperty("m_FlexibleHeight").floatValue = profile.flexibleHeight;
            if (applier.controlLayoutPriority)
                so.FindProperty("m_LayoutPriority").intValue = profile.layoutPriority;
            so.ApplyModifiedProperties();
        }

        /*
          m_IgnoreLayout: 0
          m_MinWidth: -1
          m_MinHeight: -1
          m_PreferredWidth: 200
          m_PreferredHeight: 50
          m_FlexibleWidth: -1
          m_FlexibleHeight: -1
          m_LayoutPriority: 1
        */
    }
}
