using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace JanSharp
{
    [System.Flags]
    public enum FontStylesFlags
    {
        Normal = FontStyles.Normal,
        Bold = FontStyles.Bold,
        Italic = FontStyles.Italic,
        Underline = FontStyles.Underline,
        Strikethrough = FontStyles.Strikethrough,
    }

    public enum FontStylesExclusive
    {
        Normal = FontStyles.Normal,
        LowerCase = FontStyles.LowerCase,
        UpperCase = FontStyles.UpperCase,
        SmallCaps = FontStyles.SmallCaps,
    }

    public class UIStyleTextMeshProUGUIProfile : UIStyleProfile, VRC.SDKBase.IEditorOnly
    {
        [TextArea(5, 10)]
        public string text;
        // public bool textStyle = true; // Pretty useless and hard to control.
        public TMP_FontAsset fontAsset;
        [Tooltip("Must be a material associated with the Font Asset.")]
        public Material materialPreset;
        public FontStylesFlags fontStyleFlags;
        public FontStylesExclusive fontStyleExclusive;
        public float fontSize = 36f;
        public bool autoSize = false;
        public float autoSizeMin = 18f;
        public float autoSizeMax = 72f;
        public float autoSizeMaxCharacterAdjust = 0f;
        public float autoSizeMaxLineAdjust = 0f;
        public string vertexColor;
        public bool colorGradient = false;
        public TMP_ColorGradient colorGradientPreset;
        public ColorMode colorGradientMode = ColorMode.FourCornersGradient;
        public string colorGradientTopLeft;
        public string colorGradientTopRight;
        public string colorGradientBottomLeft;
        public string colorGradientBottomRight;
        public bool overrideColorTags = false;
        public float characterSpacing = 0f;
        public float wordSpacing = 0f;
        public float lineSpacing = 0f;
        public float paragraphSpacing = 0f;
        public HorizontalAlignmentOptions horizontalAlignment = HorizontalAlignmentOptions.Left;
        public VerticalAlignmentOptions verticalAlignment = VerticalAlignmentOptions.Top;
        [Range(0f, 1f)]
        public float wordWrappingRatios = 0.4f;
        public bool wrapping = true;
        public TextOverflowModes overflow = TextOverflowModes.Overflow;
        public int pageToDisplay = 1;
        public TextureMappingOptions horizontalMapping = TextureMappingOptions.Character;
        public TextureMappingOptions verticalMapping = TextureMappingOptions.Character;
        public float uVLineOffset = 0f;
        public float marginLeft = 0f;
        public float marginTop = 0f;
        public float marginRight = 0f;
        public float marginBottom = 0f;
        public VertexSortingOrder geometrySorting = VertexSortingOrder.Normal;
        public bool isScaleStatic = false;
        public bool richText = true;
        public bool raycastTarget = true;
        public bool maskable = true;
        public bool parseEscapeCharacters = true;
        public bool visibleDescender = true;
        public TMP_SpriteAsset spriteAsset;
        public TMP_StyleSheet styleSheetAsset;
        public bool kerning = true;
        public bool extraPadding = false;

#if UNITY_EDITOR
        private void Reset()
        {
            // It would seem TMP itself finds the asset by path this way.
            fontAsset = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(
                "Assets/TextMesh Pro/Resources/Fonts & Materials/LiberationSans SDF.asset");
            materialPreset = fontAsset?.material;
        }
#endif
    }
}
