using TMPro;
using UnityEditor;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleTextMeshProUGUI))]
    public class UIStyleTextMeshProUGUIEditor : UIStyleApplierEditor<UIStyleTextMeshProUGUIProfile>
    {
        [InitializeOnLoadMethod]
        public static void OnAssemblyLoad()
        {
            UIStyleRootUtil.RegisterApplyStyleFunc<UIStyleTextMeshProUGUI, UIStyleTextMeshProUGUIProfile, TextMeshProUGUI>(ApplyStyle);
        }

        private static void ApplyStyle(
            ValidationContext context,
            UIStyleTextMeshProUGUI applier,
            UIStyleTextMeshProUGUIProfile profile,
            TextMeshProUGUI target)
        {
            SerializedObject so = new(target);
            if (applier.controlText)
                so.FindProperty("m_text").stringValue = profile.text;
            if (applier.controlFontAsset)
                so.FindProperty("m_fontAsset").objectReferenceValue = profile.fontAsset;
            if (applier.controlMaterialPreset)
                so.FindProperty("m_sharedMaterial").objectReferenceValue = profile.materialPreset;
            if (applier.controlFontStyle)
                so.FindProperty("m_fontStyle").intValue = (int)profile.fontStyleFlags | (int)profile.fontStyleExclusive;
            if (applier.controlFontSize)
                so.FindProperty("m_fontSize").floatValue = profile.fontSize;
            if (applier.controlAutoSize)
                so.FindProperty("m_enableAutoSizing").boolValue = profile.autoSize;
            if (applier.controlAutoSizeMin)
                so.FindProperty("m_fontSizeMin").floatValue = profile.autoSizeMin;
            if (applier.controlAutoSizeMax)
                so.FindProperty("m_fontSizeMax").floatValue = profile.autoSizeMax;
            if (applier.controlAutoSizeMaxCharacterAdjust)
                so.FindProperty("m_charWidthMaxAdj").floatValue = profile.autoSizeMaxCharacterAdjust;
            if (applier.controlAutoSizeMaxLineAdjust)
                so.FindProperty("m_lineSpacingMax").floatValue = profile.autoSizeMaxLineAdjust;
            if (applier.controlVertexColor)
                so.FindProperty("m_fontColor").colorValue = context.colorsByName[profile.vertexColor];
            if (applier.controlColorGradient)
            {
                so.FindProperty("m_enableVertexGradient").boolValue = profile.colorGradient;
                so.FindProperty("m_fontColorGradientPreset").objectReferenceValue = profile.colorGradientPreset;
                so.FindProperty("m_colorMode").intValue = (int)profile.colorGradientMode;
                so.FindProperty("m_fontColorGradient.topLeft").colorValue = context.colorsByName[profile.colorGradientTopLeft];
                so.FindProperty("m_fontColorGradient.topRight").colorValue = context.colorsByName[profile.colorGradientTopRight];
                so.FindProperty("m_fontColorGradient.bottomLeft").colorValue = context.colorsByName[profile.colorGradientBottomLeft];
                so.FindProperty("m_fontColorGradient.bottomRight").colorValue = context.colorsByName[profile.colorGradientBottomRight];
            }
            if (applier.controlOverrideColorTags)
                so.FindProperty("m_overrideHtmlColors").boolValue = profile.overrideColorTags;
            if (applier.controlCharacterSpacing)
                so.FindProperty("m_characterSpacing").floatValue = profile.characterSpacing;
            if (applier.controlWordSpacing)
                so.FindProperty("m_wordSpacing").floatValue = profile.wordSpacing;
            if (applier.controlLineSpacing)
                so.FindProperty("m_lineSpacing").floatValue = profile.lineSpacing;
            if (applier.controlParagraphSpacing)
                so.FindProperty("m_paragraphSpacing").floatValue = profile.paragraphSpacing;
            if (applier.controlHorizontalAlignment)
                so.FindProperty("m_HorizontalAlignment").intValue = (int)profile.horizontalAlignment;
            if (applier.controlVerticalAlignment)
                so.FindProperty("m_VerticalAlignment").intValue = (int)profile.verticalAlignment;
            if (applier.controlWordWrappingRatios)
                so.FindProperty("m_wordWrappingRatios").floatValue = (int)profile.wordWrappingRatios;
            if (applier.controlWrapping)
                so.FindProperty("m_enableWordWrapping").boolValue = profile.wrapping;
            if (applier.controlOverflow)
                so.FindProperty("m_overflowMode").intValue = (int)profile.overflow;
            if (applier.controlPageToDisplay)
                so.FindProperty("m_pageToDisplay").intValue = profile.pageToDisplay;
            if (applier.controlHorizontalMapping)
                so.FindProperty("m_horizontalMapping").intValue = (int)profile.horizontalMapping;
            if (applier.controlVerticalMapping)
                so.FindProperty("m_verticalMapping").intValue = (int)profile.verticalMapping;
            if (applier.controlUVLineOffset)
                so.FindProperty("m_uvLineOffset").floatValue = profile.uVLineOffset;
            if (applier.controlMarginLeft)
                so.FindProperty("m_margin.x").floatValue = profile.marginLeft;
            if (applier.controlMarginTop)
                so.FindProperty("m_margin.y").floatValue = profile.marginTop;
            if (applier.controlMarginRight)
                so.FindProperty("m_margin.z").floatValue = profile.marginRight;
            if (applier.controlMarginBottom)
                so.FindProperty("m_margin.w").floatValue = profile.marginBottom;
            if (applier.controlGeometrySorting)
                so.FindProperty("m_geometrySortingOrder").intValue = (int)profile.geometrySorting;
            if (applier.controlIsScaleStatic)
                so.FindProperty("m_IsTextObjectScaleStatic").boolValue = profile.isScaleStatic;
            if (applier.controlRichText)
                so.FindProperty("m_isRichText").boolValue = profile.richText;
            if (applier.controlRaycastTarget)
                so.FindProperty("m_RaycastTarget").boolValue = profile.raycastTarget;
            if (applier.controlMaskable)
                so.FindProperty("m_Maskable").boolValue = profile.maskable;
            if (applier.controlParseEscapeCharacters)
                so.FindProperty("m_parseCtrlCharacters").boolValue = profile.parseEscapeCharacters;
            if (applier.controlVisibleDescender)
                so.FindProperty("m_useMaxVisibleDescender").boolValue = profile.visibleDescender;
            if (applier.controlSpriteAsset)
                so.FindProperty("m_spriteAsset").objectReferenceValue = profile.spriteAsset;
            if (applier.controlStyleSheetAsset)
                so.FindProperty("m_StyleSheet").objectReferenceValue = profile.styleSheetAsset;
            if (applier.controlKerning)
                so.FindProperty("m_enableKerning").boolValue = profile.kerning;
            if (applier.controlExtraPadding)
                so.FindProperty("m_enableExtraPadding").boolValue = profile.extraPadding;
            so.ApplyModifiedProperties();
        }

        /*
        MonoBehaviour:
          m_Material: {fileID: 0}
          m_Color: {r: 1, g: 1, b: 1, a: 1}
          m_RaycastTarget: 1
          m_RaycastPadding: {x: 0, y: 0, z: 0, w: 0}
          m_Maskable: 1
          m_OnCullStateChanged:
            m_PersistentCalls:
              m_Calls: []
          m_text:
          m_isRightToLeft: 0
          m_fontAsset: {fileID: 11400000, guid: 8f586378b4e144a9851e7b34d9b748ee, type: 2}
          m_sharedMaterial: {fileID: 2180264, guid: 8f586378b4e144a9851e7b34d9b748ee, type: 2}
          m_fontSharedMaterials: []
          m_fontMaterial: {fileID: 0}
          m_fontMaterials: []
          m_fontColor32:
            serializedVersion: 2
            rgba: 4294967295
          m_fontColor: {r: 1, g: 1, b: 1, a: 1}
          m_enableVertexGradient: 0
          m_colorMode: 3
          m_fontColorGradient:
            topLeft: {r: 1, g: 1, b: 1, a: 1}
            topRight: {r: 1, g: 1, b: 1, a: 1}
            bottomLeft: {r: 1, g: 1, b: 1, a: 1}
            bottomRight: {r: 1, g: 1, b: 1, a: 1}
          m_fontColorGradientPreset: {fileID: 0}
          m_spriteAsset: {fileID: 0}
          m_tintAllSprites: 0
          m_StyleSheet: {fileID: 0}
          m_TextStyleHashCode: 0
          m_overrideHtmlColors: 0
          m_faceColor:
            serializedVersion: 2
            rgba: 4294967295
          m_fontSize: 36
          m_fontSizeBase: 36
          m_fontWeight: 400
          m_enableAutoSizing: 0
          m_fontSizeMin: 18
          m_fontSizeMax: 72
          m_fontStyle: 0
          m_HorizontalAlignment: 1
          m_VerticalAlignment: 256
          m_textAlignment: 65535
          m_characterSpacing: 0
          m_wordSpacing: 0
          m_lineSpacing: 0
          m_lineSpacingMax: 0
          m_paragraphSpacing: 0
          m_charWidthMaxAdj: 0
          m_enableWordWrapping: 1
          m_wordWrappingRatios: 0.4
          m_overflowMode: 0
          m_linkedTextComponent: {fileID: 0}
          parentLinkedComponent: {fileID: 0}
          m_enableKerning: 1
          m_enableExtraPadding: 0
          checkPaddingRequired: 0
          m_isRichText: 1
          m_parseCtrlCharacters: 1
          m_isOrthographic: 0
          m_isCullingEnabled: 0
          m_horizontalMapping: 0
          m_verticalMapping: 0
          m_uvLineOffset: 0
          m_geometrySortingOrder: 0
          m_IsTextObjectScaleStatic: 0
          m_VertexBufferAutoSizeReduction: 0
          m_useMaxVisibleDescender: 1
          m_pageToDisplay: 1
          m_margin: {x: 0, y: 0, z: 0, w: 0}
          m_isUsingLegacyAnimationComponent: 0
          m_isVolumetricText: 0
          m_hasFontAssetChanged: 0
          m_baseMaterial: {fileID: 0}
          m_maskOffset: {x: 0, y: 0, z: 0, w: 0}
        */
    }
}
