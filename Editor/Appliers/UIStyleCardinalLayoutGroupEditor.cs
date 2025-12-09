using UnityEditor;
using UnityEngine.UI;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleCardinalLayoutGroup))]
    public class UIStyleCardinalLayoutGroupEditor : UIStyleApplierEditor<UIStyleCardinalLayoutGroupProfile>
    {
        [MenuItem("CONTEXT/" + nameof(HorizontalOrVerticalLayoutGroup) + "/Control Using UI Style", isValidateFunction: true)]
        public static bool AddApplierValidation(MenuCommand menuCommand)
            => UIStyleApplierUtil.ContextMenuAddApplierValidation<UIStyleCardinalLayoutGroup>(menuCommand);

        [MenuItem("CONTEXT/" + nameof(HorizontalOrVerticalLayoutGroup) + "/Control Using UI Style")]
        public static void AddApplier(MenuCommand menuCommand)
            => UIStyleApplierUtil.ContextMenuAddApplier<UIStyleCardinalLayoutGroup>(menuCommand);

        [InitializeOnLoadMethod]
        public static void OnAssemblyLoad()
        {
            UIStyleRootUtil.RegisterApplyStyleFunc<UIStyleCardinalLayoutGroup, UIStyleCardinalLayoutGroupProfile, HorizontalOrVerticalLayoutGroup>(ApplyStyle);
        }

        private static void ApplyStyle(
            ValidationContext context,
            UIStyleCardinalLayoutGroup applier,
            UIStyleCardinalLayoutGroupProfile profile,
            HorizontalOrVerticalLayoutGroup target)
        {
            SerializedObject so = new(target);
            if (applier.controlPaddingLeft)
                so.FindProperty("m_Padding.m_Left").intValue = profile.paddingLeft;
            if (applier.controlPaddingRight)
                so.FindProperty("m_Padding.m_Right").intValue = profile.paddingRight;
            if (applier.controlPaddingTop)
                so.FindProperty("m_Padding.m_Top").intValue = profile.paddingTop;
            if (applier.controlPaddingBottom)
                so.FindProperty("m_Padding.m_Bottom").intValue = profile.paddingBottom;
            if (applier.controlSpacing)
                so.FindProperty("m_Spacing").floatValue = profile.spacing;
            if (applier.controlChildAlignment)
                so.FindProperty("m_ChildAlignment").intValue = (int)profile.childAlignment;
            if (applier.controlReverseArrangement)
                so.FindProperty("m_ReverseArrangement").boolValue = profile.reverseArrangement;
            if (applier.controlControlChildWidth)
                so.FindProperty("m_ChildControlWidth").boolValue = profile.controlChildWidth;
            if (applier.controlControlChildHeight)
                so.FindProperty("m_ChildControlHeight").boolValue = profile.controlChildHeight;
            if (applier.controlUseChildScaleWidth)
                so.FindProperty("m_ChildScaleWidth").boolValue = profile.useChildScaleWidth;
            if (applier.controlUseChildScaleHeight)
                so.FindProperty("m_ChildScaleHeight").boolValue = profile.useChildScaleHeight;
            if (applier.controlChildForceExpandWidth)
                so.FindProperty("m_ChildForceExpandWidth").boolValue = profile.childForceExpandWidth;
            if (applier.controlChildForceExpandHeight)
                so.FindProperty("m_ChildForceExpandHeight").boolValue = profile.childForceExpandHeight;
            so.ApplyModifiedProperties();
        }

        /*
          m_Padding:
            m_Left: 2
            m_Right: 2
            m_Top: 2
            m_Bottom: 2
          m_ChildAlignment: 0
          m_Spacing: 0
          m_ChildForceExpandWidth: 0
          m_ChildForceExpandHeight: 0
          m_ChildControlWidth: 1
          m_ChildControlHeight: 1
          m_ChildScaleWidth: 0
          m_ChildScaleHeight: 0
          m_ReverseArrangement: 0
        */
    }
}
