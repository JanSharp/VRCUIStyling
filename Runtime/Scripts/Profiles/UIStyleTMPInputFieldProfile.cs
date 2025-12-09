using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace JanSharp
{
    public class UIStyleTMPInputFieldProfile : UIStyleSelectableProfile, VRC.SDKBase.IEditorOnly
    {
        [TextArea(5, 10)]
        public string text;
        public TMP_FontAsset fontAsset;
        public float fontSize = 14f;
        [Min(0)]
        public int characterLimit = 0;
        [Tooltip("All the properties needed for Custom are not supported.")]
        public TMP_InputField.ContentType contentType = TMP_InputField.ContentType.Standard;
        public TMP_InputField.LineType lineType = TMP_InputField.LineType.SingleLine;
        public float scrollSensitivity = 1f;
        [Range(0f, 4f)]
        public float caretBlinkRate = 0.85f;
        [Range(1, 5)]
        public int caretWidth = 1;
        public bool customCaretColor = true;
        public string caretColor;
        public string selectionColor;
        public bool onFocusSelectAll = true;
        public bool resetOnDeActivation = true;
        public bool restoreOnESCKey = true;
        public bool hideSoftKeyboard = false;
        public bool hideMobileInput = false;
        public bool readOnly = false;
        public bool richText = true;
        public bool allowRichTextEditing = false;

#if UNITY_EDITOR
        private void Reset()
        {
            // It would seem TMP itself finds the asset by path this way.
            fontAsset = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(
                "Assets/TextMesh Pro/Resources/Fonts & Materials/LiberationSans SDF.asset");
        }
#endif
    }
}
