using UnityEditor;
using UnityEngine.UI;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleScrollRect))]
    public class UIStyleScrollRectEditor : UIStyleApplierEditor<UIStyleScrollRectProfile>
    {
        [InitializeOnLoadMethod]
        public static void OnAssemblyLoad()
        {
            UIStyleRootUtil.RegisterApplyStyleFunc<UIStyleScrollRect, UIStyleScrollRectProfile, ScrollRect>(ApplyStyle);
        }

        private static void ApplyStyle(
            ValidationContext context,
            UIStyleScrollRect applier,
            UIStyleScrollRectProfile profile,
            ScrollRect target)
        {
            SerializedObject so = new(target);
            if (applier.controlHorizontal)
                so.FindProperty("m_Horizontal").boolValue = profile.horizontal;
            if (applier.controlVertical)
                so.FindProperty("m_Vertical").boolValue = profile.vertical;
            if (applier.controlMovementType)
                so.FindProperty("m_MovementType").intValue = (int)profile.movementType;
            if (applier.controlElasticity)
                so.FindProperty("m_Elasticity").floatValue = profile.elasticity;
            if (applier.controlInertia)
                so.FindProperty("m_Inertia").boolValue = profile.inertia;
            if (applier.controlDecelerationRate)
                so.FindProperty("m_DecelerationRate").floatValue = profile.decelerationRate;
            if (applier.controlScrollSensitivity)
                so.FindProperty("m_ScrollSensitivity").floatValue = profile.scrollSensitivity;
            if (applier.controlHorizontalVisibility)
                so.FindProperty("m_HorizontalScrollbarVisibility").intValue = (int)profile.horizontalVisibility;
            if (applier.controlHorizontalSpacing)
                so.FindProperty("m_HorizontalScrollbarSpacing").floatValue = profile.horizontalSpacing;
            if (applier.controlVerticalVisibility)
                so.FindProperty("m_VerticalScrollbarVisibility").intValue = (int)profile.verticalVisibility;
            if (applier.controlVerticalSpacing)
                so.FindProperty("m_VerticalScrollbarSpacing").floatValue = profile.verticalSpacing;
            so.ApplyModifiedProperties();
        }

        /*
          m_Content: {fileID: 1179033135}
          m_Horizontal: 1
          m_Vertical: 1
          m_MovementType: 1
          m_Elasticity: 0.1
          m_Inertia: 1
          m_DecelerationRate: 0.135
          m_ScrollSensitivity: 1
          m_Viewport: {fileID: 446623882}
          m_HorizontalScrollbar: {fileID: 94990790}
          m_VerticalScrollbar: {fileID: 1647588110}
          m_HorizontalScrollbarVisibility: 2
          m_VerticalScrollbarVisibility: 2
          m_HorizontalScrollbarSpacing: -3
          m_VerticalScrollbarSpacing: -3
          m_OnValueChanged:
            m_PersistentCalls:
              m_Calls: []
        */
    }
}
