using UnityEditor;
using UnityEngine.UI;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleSlider))]
    public class UIStyleSliderEditor : UIStyleApplierEditor<UIStyleSliderProfile>
    {
        [MenuItem("CONTEXT/" + nameof(Slider) + "/Control Using UI Style", isValidateFunction: true)]
        public static bool AddApplierValidation(MenuCommand menuCommand)
            => UIStyleApplierUtil.ContextMenuAddApplierValidation<UIStyleSlider>(menuCommand);

        [MenuItem("CONTEXT/" + nameof(Slider) + "/Control Using UI Style")]
        public static void AddApplier(MenuCommand menuCommand)
            => UIStyleApplierUtil.ContextMenuAddApplier<UIStyleSlider>(menuCommand);

        [InitializeOnLoadMethod]
        public static void OnAssemblyLoad()
        {
            UIStyleRootUtil.RegisterApplyStyleFunc<UIStyleSlider, UIStyleSliderProfile, Slider>(ApplyStyle);
        }

        private static void ApplyStyle(
            ValidationContext context,
            UIStyleSlider applier,
            UIStyleSliderProfile profile,
            Slider target)
        {
            SerializedObject so = new(target);
            UIStyleSelectableEditor.ApplySelectableStyle(so, context, applier, profile);
            if (applier.controlDirection)
                so.FindProperty("m_Direction").intValue = (int)profile.direction;
            if (applier.controlMinValue)
                so.FindProperty("m_MinValue").floatValue = profile.minValue;
            if (applier.controlMaxValue)
                so.FindProperty("m_MaxValue").floatValue = profile.maxValue;
            if (applier.controlWholeNumbers)
                so.FindProperty("m_WholeNumbers").boolValue = profile.wholeNumbers;
            if (applier.controlValue)
                so.FindProperty("m_Value").floatValue = profile.value;
            so.ApplyModifiedProperties();
        }

        /*
          m_FillRect: {fileID: 93960022}
          m_HandleRect: {fileID: 1999136943}
          m_Direction: 0
          m_MinValue: 0
          m_MaxValue: 1
          m_WholeNumbers: 0
          m_Value: 0
          m_OnValueChanged:
            m_PersistentCalls:
              m_Calls: []
        */
    }
}
