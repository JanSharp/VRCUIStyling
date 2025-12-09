using TMPro;
using UnityEditor;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleTMPInputField))]
    public class UIStyleTMPInputFieldEditor : UIStyleApplierEditor<UIStyleTMPInputFieldProfile>
    {
        [MenuItem("CONTEXT/" + nameof(TMP_InputField) + "/Control Using UI Style", isValidateFunction: true)]
        public static bool AddApplierValidation(MenuCommand menuCommand)
            => UIStyleApplierUtil.ContextMenuAddApplierValidation<UIStyleTMPInputField>(menuCommand);

        [MenuItem("CONTEXT/" + nameof(TMP_InputField) + "/Control Using UI Style")]
        public static void AddApplier(MenuCommand menuCommand)
            => UIStyleApplierUtil.ContextMenuAddApplier<UIStyleTMPInputField>(menuCommand);

        [InitializeOnLoadMethod]
        public static void OnAssemblyLoad()
        {
            UIStyleRootUtil.RegisterApplyStyleFunc<UIStyleTMPInputField, UIStyleTMPInputFieldProfile, TMP_InputField>(ApplyStyle);
        }

        private static void ApplyStyle(
            ValidationContext context,
            UIStyleTMPInputField applier,
            UIStyleTMPInputFieldProfile profile,
            TMP_InputField target)
        {
            SerializedObject so = new(target);
            UIStyleSelectableEditor.ApplySelectableStyle(so, context, applier, profile);
            if (applier.controlText)
                so.FindProperty("m_Text").stringValue = profile.text;
            if (applier.controlFontAsset)
                so.FindProperty("m_GlobalFontAsset").objectReferenceValue = profile.fontAsset;
            if (applier.controlFontSize)
                so.FindProperty("m_GlobalPointSize").floatValue = profile.fontSize;
            if (applier.controlCharacterLimit)
                so.FindProperty("m_CharacterLimit").intValue = profile.characterLimit;
            if (applier.controlContentType)
                so.FindProperty("m_ContentType").intValue = (int)profile.contentType;
            if (applier.controlLineType)
                so.FindProperty("m_LineType").intValue = (int)profile.lineType;
            if (applier.controlScrollSensitivity)
                so.FindProperty("m_ScrollSensitivity").floatValue = profile.scrollSensitivity;
            if (applier.controlCaretBlinkRate)
                so.FindProperty("m_CaretBlinkRate").floatValue = profile.caretBlinkRate;
            if (applier.controlCaretWidth)
                so.FindProperty("m_CaretWidth").intValue = profile.caretWidth;
            if (applier.controlCustomCaretColor)
                so.FindProperty("m_CustomCaretColor").boolValue = profile.customCaretColor;
            if (applier.controlCaretColor)
                so.FindProperty("m_CaretColor").colorValue = context.colorsByName[profile.caretColor];
            if (applier.controlSelectionColor)
                so.FindProperty("m_SelectionColor").colorValue = context.colorsByName[profile.selectionColor];
            if (applier.controlOnFocusSelectAll)
                so.FindProperty("m_OnFocusSelectAll").boolValue = profile.onFocusSelectAll;
            if (applier.controlResetOnDeActivation)
                so.FindProperty("m_ResetOnDeActivation").boolValue = profile.resetOnDeActivation;
            if (applier.controlRestoreOnESCKey)
                so.FindProperty("m_RestoreOriginalTextOnEscape").boolValue = profile.restoreOnESCKey;
            if (applier.controlHideSoftKeyboard)
                so.FindProperty("m_HideSoftKeyboard").boolValue = profile.hideSoftKeyboard;
            if (applier.controlHideMobileInput)
                so.FindProperty("m_HideMobileInput").boolValue = profile.hideMobileInput;
            if (applier.controlReadOnly)
                so.FindProperty("m_ReadOnly").boolValue = profile.readOnly;
            if (applier.controlRichText)
                so.FindProperty("m_RichText").boolValue = profile.richText;
            if (applier.controlAllowRichTextEditing)
                so.FindProperty("m_isRichTextEditingAllowed").boolValue = profile.allowRichTextEditing;
            so.ApplyModifiedProperties();
        }

        /*
          m_TextViewport: {fileID: 572972766}
          m_TextComponent: {fileID: 1254669467}
          m_Placeholder: {fileID: 758186941}
          m_VerticalScrollbar: {fileID: 132581243}
          m_VerticalScrollbarEventHandler: {fileID: 0}
          m_LayoutGroup: {fileID: 0}
          m_ScrollSensitivity: 1
          m_ContentType: 0
          m_InputType: 0
          m_AsteriskChar: 42
          m_KeyboardType: 0
          m_LineType: 0
          m_HideMobileInput: 0
          m_HideSoftKeyboard: 0
          m_CharacterValidation: 0
          m_RegexValue:
          m_GlobalPointSize: 14
          m_CharacterLimit: 0
          m_OnEndEdit:
            m_PersistentCalls:
              m_Calls: []
          m_OnSubmit:
            m_PersistentCalls:
              m_Calls: []
          m_OnSelect:
            m_PersistentCalls:
              m_Calls: []
          m_OnDeselect:
            m_PersistentCalls:
              m_Calls: []
          m_OnTextSelection:
            m_PersistentCalls:
              m_Calls: []
          m_OnEndTextSelection:
            m_PersistentCalls:
              m_Calls: []
          m_OnValueChanged:
            m_PersistentCalls:
              m_Calls: []
          m_OnTouchScreenKeyboardStatusChanged:
            m_PersistentCalls:
              m_Calls: []
          m_CaretColor: {r: 0.19607843, g: 0.19607843, b: 0.19607843, a: 1}
          m_CustomCaretColor: 1
          m_SelectionColor: {r: 0.65882355, g: 0.80784315, b: 1, a: 0.7529412}
          m_Text:
          m_CaretBlinkRate: 0.85
          m_CaretWidth: 1
          m_ReadOnly: 0
          m_RichText: 1
          m_GlobalFontAsset: {fileID: 11400000, guid: 8f586378b4e144a9851e7b34d9b748ee, type: 2}
          m_OnFocusSelectAll: 1
          m_ResetOnDeActivation: 1
          m_RestoreOriginalTextOnEscape: 1
          m_isRichTextEditingAllowed: 0
          m_LineLimit: 0
          m_InputValidator: {fileID: 0}
        */
    }
}
