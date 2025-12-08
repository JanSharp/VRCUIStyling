using TMPro;
using UnityEditor;
using UnityEngine;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleTextMeshProUGUIProfile))]
    public class UIStyleTextMeshProUGUIProfileEditor : UIStyleProfileEditor
    {
        [InitializeOnLoadMethod]
        public static void OnAssemblyLoad()
        {
            UIStyleProfileContainerUtil.RegisterColorFields<UIStyleTextMeshProUGUIProfile>(new string[]
            {
                nameof(UIStyleTextMeshProUGUIProfile.vertexColor),
                nameof(UIStyleTextMeshProUGUIProfile.colorGradientTopLeft),
                nameof(UIStyleTextMeshProUGUIProfile.colorGradientTopRight),
                nameof(UIStyleTextMeshProUGUIProfile.colorGradientBottomLeft),
                nameof(UIStyleTextMeshProUGUIProfile.colorGradientBottomRight),
            });
        }

        private static GUIContent alignmentLabel = new GUIContent("Alignment", "Horizontal and vertical alignment of the text within its container.");
        private static GUIContent wrapMixLabel = new GUIContent("Wrap Mix (W <-> C)", "How much to favor words versus characters when distributing the text.");

        private SerializedProperty textProp;
        private SerializedProperty fontAssetProp;
        private SerializedProperty materialPresetProp;
        private SerializedProperty fontStyleFlagsProp;
        private SerializedProperty fontStyleExclusiveProp;
        private SerializedProperty fontSizeProp;
        private SerializedProperty autoSizeProp;
        private SerializedProperty autoSizeMinProp;
        private SerializedProperty autoSizeMaxProp;
        private SerializedProperty autoSizeMaxCharacterAdjustProp;
        private SerializedProperty autoSizeMaxLineAdjustProp;
        private SerializedProperty vertexColorProp;
        private SerializedProperty colorGradientProp;
        private SerializedProperty colorGradientPresetProp;
        private SerializedProperty colorGradientModeProp;
        private SerializedProperty colorGradientTopLeftProp;
        private SerializedProperty colorGradientTopRightProp;
        private SerializedProperty colorGradientBottomLeftProp;
        private SerializedProperty colorGradientBottomRightProp;
        private SerializedProperty overrideColorTagsProp;
        private SerializedProperty characterSpacingProp;
        private SerializedProperty wordSpacingProp;
        private SerializedProperty lineSpacingProp;
        private SerializedProperty paragraphSpacingProp;
        private SerializedProperty horizontalAlignmentProp;
        private SerializedProperty verticalAlignmentProp;
        private SerializedProperty wordWrappingRatiosProp;
        private SerializedProperty wrappingProp;
        private SerializedProperty overflowProp;
        private SerializedProperty pageToDisplayProp;
        private SerializedProperty horizontalMappingProp;
        private SerializedProperty verticalMappingProp;
        private SerializedProperty uVLineOffsetProp;
        private SerializedProperty marginLeftProp;
        private SerializedProperty marginTopProp;
        private SerializedProperty marginRightProp;
        private SerializedProperty marginBottomProp;
        private SerializedProperty geometrySortingProp;
        private SerializedProperty isScaleStaticProp;
        private SerializedProperty richTextProp;
        private SerializedProperty raycastTargetProp;
        private SerializedProperty maskableProp;
        private SerializedProperty parseEscapeCharactersProp;
        private SerializedProperty visibleDescenderProp;
        private SerializedProperty spriteAssetProp;
        private SerializedProperty styleSheetAssetProp;
        private SerializedProperty kerningProp;
        private SerializedProperty extraPaddingProp;

        public override void OnEnable()
        {
            base.OnEnable();
            textProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.text));
            fontAssetProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.fontAsset));
            materialPresetProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.materialPreset));
            fontStyleFlagsProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.fontStyleFlags));
            fontStyleExclusiveProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.fontStyleExclusive));
            fontSizeProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.fontSize));
            autoSizeProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.autoSize));
            autoSizeMinProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.autoSizeMin));
            autoSizeMaxProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.autoSizeMax));
            autoSizeMaxCharacterAdjustProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.autoSizeMaxCharacterAdjust));
            autoSizeMaxLineAdjustProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.autoSizeMaxLineAdjust));
            vertexColorProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.vertexColor));
            colorGradientProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.colorGradient));
            colorGradientPresetProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.colorGradientPreset));
            colorGradientModeProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.colorGradientMode));
            colorGradientTopLeftProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.colorGradientTopLeft));
            colorGradientTopRightProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.colorGradientTopRight));
            colorGradientBottomLeftProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.colorGradientBottomLeft));
            colorGradientBottomRightProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.colorGradientBottomRight));
            overrideColorTagsProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.overrideColorTags));
            characterSpacingProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.characterSpacing));
            wordSpacingProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.wordSpacing));
            lineSpacingProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.lineSpacing));
            paragraphSpacingProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.paragraphSpacing));
            horizontalAlignmentProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.horizontalAlignment));
            verticalAlignmentProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.verticalAlignment));
            wordWrappingRatiosProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.wordWrappingRatios));
            wrappingProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.wrapping));
            overflowProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.overflow));
            pageToDisplayProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.pageToDisplay));
            horizontalMappingProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.horizontalMapping));
            verticalMappingProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.verticalMapping));
            uVLineOffsetProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.uVLineOffset));
            marginLeftProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.marginLeft));
            marginTopProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.marginTop));
            marginRightProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.marginRight));
            marginBottomProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.marginBottom));
            geometrySortingProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.geometrySorting));
            isScaleStaticProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.isScaleStatic));
            richTextProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.richText));
            raycastTargetProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.raycastTarget));
            maskableProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.maskable));
            parseEscapeCharactersProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.parseEscapeCharacters));
            visibleDescenderProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.visibleDescender));
            spriteAssetProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.spriteAsset));
            styleSheetAssetProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.styleSheetAsset));
            kerningProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.kerning));
            extraPaddingProp = serializedObject.FindProperty(nameof(UIStyleTextMeshProUGUIProfile.extraPadding));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawProfileEditor();
            EditorGUILayout.PropertyField(textProp);
            EditorGUILayout.PropertyField(fontAssetProp);
            EditorGUILayout.PropertyField(materialPresetProp);
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.PropertyField(fontStyleFlagsProp);
                EditorGUILayout.PropertyField(fontStyleExclusiveProp, GUIContent.none, GUILayout.MaxWidth(90f));
            }
            EditorGUILayout.Space();
            using (new EditorGUI.DisabledGroupScope(autoSizeProp.boolValue))
                EditorGUILayout.PropertyField(fontSizeProp);
            EditorGUILayout.PropertyField(autoSizeProp);
            if (autoSizeProp.boolValue)
            {
                EditorGUILayout.PropertyField(autoSizeMinProp);
                EditorGUILayout.PropertyField(autoSizeMaxProp);
                EditorGUILayout.PropertyField(autoSizeMaxCharacterAdjustProp);
                EditorGUILayout.PropertyField(autoSizeMaxLineAdjustProp);
            }
            EditorGUILayout.Space();
            DrawColorSelectorField(vertexColorProp);
            EditorGUILayout.PropertyField(colorGradientProp);
            if (colorGradientProp.boolValue)
                DrawGradientColors();
            EditorGUILayout.PropertyField(overrideColorTagsProp);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(characterSpacingProp);
            EditorGUILayout.PropertyField(wordSpacingProp);
            EditorGUILayout.PropertyField(lineSpacingProp);
            EditorGUILayout.PropertyField(paragraphSpacingProp);
            EditorGUILayout.Space();
            DrawAlignment();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(wrappingProp);
            EditorGUILayout.PropertyField(overflowProp);
            EditorGUILayout.PropertyField(pageToDisplayProp);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(horizontalMappingProp);
            EditorGUILayout.PropertyField(verticalMappingProp);
            if (((TextureMappingOptions)horizontalMappingProp.enumValueIndex) != TextureMappingOptions.Character)
                EditorGUILayout.PropertyField(uVLineOffsetProp);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(marginLeftProp);
            EditorGUILayout.PropertyField(marginTopProp);
            EditorGUILayout.PropertyField(marginRightProp);
            EditorGUILayout.PropertyField(marginBottomProp);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(geometrySortingProp);
            EditorGUILayout.PropertyField(isScaleStaticProp);
            EditorGUILayout.PropertyField(richTextProp);
            EditorGUILayout.PropertyField(raycastTargetProp);
            EditorGUILayout.PropertyField(maskableProp);
            EditorGUILayout.PropertyField(parseEscapeCharactersProp);
            EditorGUILayout.PropertyField(visibleDescenderProp);
            EditorGUILayout.PropertyField(spriteAssetProp);
            EditorGUILayout.PropertyField(styleSheetAssetProp);
            EditorGUILayout.PropertyField(kerningProp);
            EditorGUILayout.PropertyField(extraPaddingProp);
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawGradientColors()
        {
            EditorGUILayout.PropertyField(colorGradientPresetProp);
            bool forceCopyColors;
            using (var scope = new EditorGUI.ChangeCheckScope())
            {
                EditorGUILayout.PropertyField(colorGradientModeProp);
                forceCopyColors = scope.changed;
            }
            switch (((ColorMode)colorGradientModeProp.intValue))
            {
                default:
                case ColorMode.Single:
                    DrawGradientColorsSingle(forceCopyColors);
                    break;
                case ColorMode.HorizontalGradient:
                    DrawGradientColorsHorizontal(forceCopyColors);
                    break;
                case ColorMode.VerticalGradient:
                    DrawGradientColorsVertical(forceCopyColors);
                    break;
                case ColorMode.FourCornersGradient:
                    DrawGradientColorsFourCorners();
                    break;
            }
        }

        private void DrawGradientColorsSingle(bool forceCopyColors)
        {
            using var scope = new EditorGUI.ChangeCheckScope();
            DrawColorSelectorField(colorGradientTopLeftProp, new GUIContent("Color Gradient Color"));
            if (!scope.changed && !forceCopyColors)
                return;
            colorGradientTopRightProp.stringValue = colorGradientTopLeftProp.stringValue;
            colorGradientBottomLeftProp.stringValue = colorGradientTopLeftProp.stringValue;
            colorGradientBottomRightProp.stringValue = colorGradientTopLeftProp.stringValue;
        }

        private void DrawGradientColorsHorizontal(bool forceCopyColors)
        {
            using var scope = new EditorGUI.ChangeCheckScope();
            DrawColorSelectorField(colorGradientTopLeftProp, new GUIContent("Color Gradient Left"));
            DrawColorSelectorField(colorGradientTopRightProp, new GUIContent("Color Gradient Right"));
            if (!scope.changed && !forceCopyColors)
                return;
            colorGradientBottomLeftProp.stringValue = colorGradientTopLeftProp.stringValue;
            colorGradientBottomRightProp.stringValue = colorGradientTopRightProp.stringValue;
        }

        private void DrawGradientColorsVertical(bool forceCopyColors)
        {
            using var scope = new EditorGUI.ChangeCheckScope();
            DrawColorSelectorField(colorGradientTopLeftProp, new GUIContent("Color Gradient Top"));
            DrawColorSelectorField(colorGradientBottomLeftProp, new GUIContent("Color Gradient Bottom"));
            if (!scope.changed && !forceCopyColors)
                return;
            colorGradientTopRightProp.stringValue = colorGradientTopLeftProp.stringValue;
            colorGradientBottomRightProp.stringValue = colorGradientBottomLeftProp.stringValue;
        }

        private void DrawGradientColorsFourCorners()
        {
            DrawColorSelectorField(colorGradientTopLeftProp);
            DrawColorSelectorField(colorGradientTopRightProp);
            DrawColorSelectorField(colorGradientBottomLeftProp);
            DrawColorSelectorField(colorGradientBottomRightProp);
        }

        private void DrawAlignment()
        {
            Rect rect = EditorGUILayout.GetControlRect(hasLabel: true, EditorGUIUtility.currentViewWidth > 504f ? 20f : 40f + 3f);
            using (new EditorGUI.PropertyScope(rect, alignmentLabel, horizontalAlignmentProp))
            using (new EditorGUI.PropertyScope(rect, alignmentLabel, verticalAlignmentProp))
            {
                EditorGUI.PrefixLabel(rect, alignmentLabel);
                rect.x += EditorGUIUtility.labelWidth;
                EditorGUI.PropertyField(rect, horizontalAlignmentProp, GUIContent.none);
                EditorGUI.PropertyField(rect, verticalAlignmentProp, GUIContent.none);
            }

            if (((HorizontalAlignmentOptions)horizontalAlignmentProp.enumValueFlag
                & (HorizontalAlignmentOptions.Justified | HorizontalAlignmentOptions.Flush)) != 0)
            {
                EditorGUILayout.PropertyField(wordWrappingRatiosProp, wrapMixLabel);
            }
        }
    }
}
