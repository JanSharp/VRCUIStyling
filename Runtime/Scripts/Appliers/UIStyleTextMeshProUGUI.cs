using UnityEngine;

namespace JanSharp
{
    [DisallowMultipleComponent]
    public class UIStyleTextMeshProUGUI : UIStyleApplier, VRC.SDKBase.IEditorOnly
    {
        public bool controlText = false;
        // public bool controlTextStyle = true; // Pretty useless and hard to control.
        public bool controlFontAsset = true;
        public bool controlMaterialPreset = true;
        public bool controlFontStyle = true;
        public bool controlFontSize = true;
        public bool controlAutoSize = true;
        public bool controlAutoSizeMin = true;
        public bool controlAutoSizeMax = true;
        public bool controlAutoSizeMaxCharacterAdjust = true;
        public bool controlAutoSizeMaxLineAdjust = true;
        public bool controlVertexColor = true;
        public bool controlColorGradient = true;
        public bool controlOverrideColorTags = true;
        public bool controlCharacterSpacing = true;
        public bool controlWordSpacing = true;
        public bool controlLineSpacing = true;
        public bool controlParagraphSpacing = true;
        public bool controlHorizontalAlignment = true;
        public bool controlVerticalAlignment = true;
        public bool controlWordWrappingRatios = true;
        public bool controlWrapping = true;
        public bool controlOverflow = true;
        public bool controlPageToDisplay = true;
        public bool controlHorizontalMapping = true;
        public bool controlVerticalMapping = true;
        public bool controlUVLineOffset = true;
        public bool controlMarginLeft = true;
        public bool controlMarginTop = true;
        public bool controlMarginRight = true;
        public bool controlMarginBottom = true;
        public bool controlGeometrySorting = true;
        public bool controlIsScaleStatic = true;
        public bool controlRichText = true;
        public bool controlRaycastTarget = true;
        public bool controlMaskable = true;
        public bool controlParseEscapeCharacters = true;
        public bool controlVisibleDescender = true;
        public bool controlSpriteAsset = true;
        public bool controlStyleSheetAsset = true;
        public bool controlKerning = true;
        public bool controlExtraPadding = true;
    }
}
