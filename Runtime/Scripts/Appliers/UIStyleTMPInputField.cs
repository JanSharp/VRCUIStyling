using UnityEngine;

namespace JanSharp
{
    [DisallowMultipleComponent]
    public class UIStyleTMPInputField : UIStyleSelectable, VRC.SDKBase.IEditorOnly
    {
        public bool controlText = false;
        public bool controlFontAsset = true;
        public bool controlFontSize = true;
        public bool controlCharacterLimit = true;
        public bool controlContentType = true;
        public bool controlLineType = true;
        public bool controlScrollSensitivity = true;
        public bool controlCaretBlinkRate = true;
        public bool controlCaretWidth = true;
        public bool controlCustomCaretColor = true;
        public bool controlCaretColor = true;
        public bool controlSelectionColor = true;
        public bool controlOnFocusSelectAll = true;
        public bool controlResetOnDeActivation = true;
        public bool controlRestoreOnESCKey = true;
        public bool controlHideSoftKeyboard = true;
        public bool controlHideMobileInput = true;
        public bool controlReadOnly = true;
        public bool controlRichText = true;
        public bool controlAllowRichTextEditing = true;
    }
}
