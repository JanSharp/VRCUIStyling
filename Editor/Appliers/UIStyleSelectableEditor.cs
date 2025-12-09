using UnityEditor;
using UnityEngine.UI;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleSelectable))]
    public class UIStyleSelectableEditor : UIStyleApplierEditor<UIStyleSelectableProfile>
    {
        [MenuItem("CONTEXT/" + nameof(Selectable) + "/Control Using UI Style", isValidateFunction: true)]
        public static bool AddApplierValidation(MenuCommand menuCommand)
            => UIStyleApplierUtil.ContextMenuAddApplierValidation<UIStyleSelectable>(menuCommand);

        [MenuItem("CONTEXT/" + nameof(Selectable) + "/Control Using UI Style")]
        public static void AddApplier(MenuCommand menuCommand)
            => UIStyleApplierUtil.ContextMenuAddApplier<UIStyleSelectable>(menuCommand);

        [InitializeOnLoadMethod]
        public static void OnAssemblyLoad()
        {
            UIStyleRootUtil.RegisterApplyStyleFunc<UIStyleSelectable, UIStyleSelectableProfile, Selectable>(ApplyStyle);
        }

        private static void ApplyStyle(
            ValidationContext context,
            UIStyleSelectable applier,
            UIStyleSelectableProfile profile,
            Selectable target)
        {
            SerializedObject so = new(target);
            ApplySelectableStyle(so, context, applier, profile);
            so.ApplyModifiedProperties();
        }

        public static void ApplySelectableStyle(SerializedObject so, ValidationContext context, UIStyleSelectable applier, UIStyleSelectableProfile profile)
        {
            if (applier.controlInteractable)
                so.FindProperty("m_Interactable").boolValue = profile.interactable;
            if (applier.controlTransition)
                so.FindProperty("m_Transition").intValue = (int)profile.transition;
            if (applier.controlNormalColor)
                so.FindProperty("m_Colors.m_NormalColor").colorValue = context.colorsByName[profile.normalColor];
            if (applier.controlHighlightedColor)
                so.FindProperty("m_Colors.m_HighlightedColor").colorValue = context.colorsByName[profile.highlightedColor];
            if (applier.controlPressedColor)
                so.FindProperty("m_Colors.m_PressedColor").colorValue = context.colorsByName[profile.pressedColor];
            if (applier.controlSelectedColor)
                so.FindProperty("m_Colors.m_SelectedColor").colorValue = context.colorsByName[profile.selectedColor];
            if (applier.controlDisabledColor)
                so.FindProperty("m_Colors.m_DisabledColor").colorValue = context.colorsByName[profile.disabledColor];
            if (applier.controlColorMultiplier)
                so.FindProperty("m_Colors.m_ColorMultiplier").floatValue = profile.colorMultiplier;
            if (applier.controlFadeDuration)
                so.FindProperty("m_Colors.m_FadeDuration").floatValue = profile.fadeDuration;
            if (applier.controlHighlightedSprite)
                so.FindProperty("m_SpriteState.m_HighlightedSprite").objectReferenceValue = context.spritesByName[profile.highlightedSprite];
            if (applier.controlPressedSprite)
                so.FindProperty("m_SpriteState.m_PressedSprite").objectReferenceValue = context.spritesByName[profile.pressedSprite];
            if (applier.controlSelectedSprite)
                so.FindProperty("m_SpriteState.m_SelectedSprite").objectReferenceValue = context.spritesByName[profile.selectedSprite];
            if (applier.controlDisabledSprite)
                so.FindProperty("m_SpriteState.m_DisabledSprite").objectReferenceValue = context.spritesByName[profile.disabledSprite];
            if (applier.controlNavigation)
            {
                so.FindProperty("m_Navigation.m_Mode").enumValueFlag = (int)profile.navigation.mode;
                so.FindProperty("m_Navigation.m_WrapAround").boolValue = profile.navigation.wrapAround;
            }
        }

        /*
          m_Navigation:
            m_Mode: 0
            m_WrapAround: 0
            m_SelectOnUp: {fileID: 0}
            m_SelectOnDown: {fileID: 0}
            m_SelectOnLeft: {fileID: 0}
            m_SelectOnRight: {fileID: 0}
          m_Transition: 1
          m_Colors:
            m_NormalColor: {r: 1, g: 1, b: 1, a: 1}
            m_HighlightedColor: {r: 0.9607843, g: 0.9607843, b: 0.9607843, a: 1}
            m_PressedColor: {r: 0.78431374, g: 0.78431374, b: 0.78431374, a: 1}
            m_SelectedColor: {r: 0.9607843, g: 0.9607843, b: 0.9607843, a: 1}
            m_DisabledColor: {r: 0.78431374, g: 0.78431374, b: 0.78431374, a: 0.5019608}
            m_ColorMultiplier: 1
            m_FadeDuration: 0.1
          m_SpriteState:
            m_HighlightedSprite: {fileID: 0}
            m_PressedSprite: {fileID: 0}
            m_SelectedSprite: {fileID: 0}
            m_DisabledSprite: {fileID: 0}
          m_AnimationTriggers:
            m_NormalTrigger: Normal
            m_HighlightedTrigger: Highlighted
            m_PressedTrigger: Pressed
            m_SelectedTrigger: Selected
            m_DisabledTrigger: Disabled
          m_Interactable: 1
          m_TargetGraphic: {fileID: 404176421}
        */
    }
}
