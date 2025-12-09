using TMPro;
using UnityEditor;
using UnityEngine;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleTMPInputFieldProfile))]
    public class UIStyleTMPInputFieldProfileEditor : UIStyleSelectableProfileEditor
    {
        [InitializeOnLoadMethod]
        public static void OnTMPInputFieldAssemblyLoad()
        {
            RegisterSelectableColorSpriteFields<UIStyleTMPInputFieldProfile>(
                extraColorFields: new string[]
                {
                    nameof(UIStyleTMPInputFieldProfile.caretColor),
                    nameof(UIStyleTMPInputFieldProfile.selectionColor),
                });
        }

        private SerializedProperty textProp;
        private SerializedProperty fontAssetProp;
        private SerializedProperty fontSizeProp;
        private SerializedProperty characterLimitProp;
        private SerializedProperty contentTypeProp;
        private SerializedProperty lineTypeProp;
        private SerializedProperty scrollSensitivityProp;
        private SerializedProperty caretBlinkRateProp;
        private SerializedProperty caretWidthProp;
        private SerializedProperty customCaretColorProp;
        private SerializedProperty caretColorProp;
        private SerializedProperty selectionColorProp;
        private SerializedProperty onFocusSelectAllProp;
        private SerializedProperty resetOnDeActivationProp;
        private SerializedProperty restoreOnESCKeyProp;
        private SerializedProperty hideSoftKeyboardProp;
        private SerializedProperty hideMobileInputProp;
        private SerializedProperty readOnlyProp;
        private SerializedProperty richTextProp;
        private SerializedProperty allowRichTextEditingProp;

        public override void OnEnable()
        {
            base.OnEnable();
            textProp = serializedObject.FindProperty(nameof(UIStyleTMPInputFieldProfile.text));
            fontAssetProp = serializedObject.FindProperty(nameof(UIStyleTMPInputFieldProfile.fontAsset));
            fontSizeProp = serializedObject.FindProperty(nameof(UIStyleTMPInputFieldProfile.fontSize));
            characterLimitProp = serializedObject.FindProperty(nameof(UIStyleTMPInputFieldProfile.characterLimit));
            contentTypeProp = serializedObject.FindProperty(nameof(UIStyleTMPInputFieldProfile.contentType));
            lineTypeProp = serializedObject.FindProperty(nameof(UIStyleTMPInputFieldProfile.lineType));
            scrollSensitivityProp = serializedObject.FindProperty(nameof(UIStyleTMPInputFieldProfile.scrollSensitivity));
            caretBlinkRateProp = serializedObject.FindProperty(nameof(UIStyleTMPInputFieldProfile.caretBlinkRate));
            caretWidthProp = serializedObject.FindProperty(nameof(UIStyleTMPInputFieldProfile.caretWidth));
            customCaretColorProp = serializedObject.FindProperty(nameof(UIStyleTMPInputFieldProfile.customCaretColor));
            caretColorProp = serializedObject.FindProperty(nameof(UIStyleTMPInputFieldProfile.caretColor));
            selectionColorProp = serializedObject.FindProperty(nameof(UIStyleTMPInputFieldProfile.selectionColor));
            onFocusSelectAllProp = serializedObject.FindProperty(nameof(UIStyleTMPInputFieldProfile.onFocusSelectAll));
            resetOnDeActivationProp = serializedObject.FindProperty(nameof(UIStyleTMPInputFieldProfile.resetOnDeActivation));
            restoreOnESCKeyProp = serializedObject.FindProperty(nameof(UIStyleTMPInputFieldProfile.restoreOnESCKey));
            hideSoftKeyboardProp = serializedObject.FindProperty(nameof(UIStyleTMPInputFieldProfile.hideSoftKeyboard));
            hideMobileInputProp = serializedObject.FindProperty(nameof(UIStyleTMPInputFieldProfile.hideMobileInput));
            readOnlyProp = serializedObject.FindProperty(nameof(UIStyleTMPInputFieldProfile.readOnly));
            richTextProp = serializedObject.FindProperty(nameof(UIStyleTMPInputFieldProfile.richText));
            allowRichTextEditingProp = serializedObject.FindProperty(nameof(UIStyleTMPInputFieldProfile.allowRichTextEditing));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawProfileEditor();
            DrawSelectableFields();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(textProp);
            EditorGUILayout.PropertyField(fontAssetProp);
            EditorGUILayout.PropertyField(fontSizeProp);
            EditorGUILayout.PropertyField(characterLimitProp);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(contentTypeProp);
            TMP_InputField.ContentType contentType = (TMP_InputField.ContentType)contentTypeProp.intValue;
            if (contentType == TMP_InputField.ContentType.Standard
                || contentType == TMP_InputField.ContentType.Autocorrected
                || contentType == TMP_InputField.ContentType.Custom)
            {
                using (new EditorGUI.IndentLevelScope())
                    EditorGUILayout.PropertyField(lineTypeProp);
            }
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(scrollSensitivityProp);
            EditorGUILayout.PropertyField(caretBlinkRateProp);
            EditorGUILayout.PropertyField(caretWidthProp);
            EditorGUILayout.PropertyField(customCaretColorProp);
            DrawColorSelectorField(caretColorProp);
            DrawColorSelectorField(selectionColorProp);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(onFocusSelectAllProp);
            EditorGUILayout.PropertyField(resetOnDeActivationProp);
            EditorGUILayout.PropertyField(restoreOnESCKeyProp);
            EditorGUILayout.PropertyField(hideSoftKeyboardProp);
            EditorGUILayout.PropertyField(hideMobileInputProp);
            EditorGUILayout.PropertyField(readOnlyProp);
            EditorGUILayout.PropertyField(richTextProp);
            EditorGUILayout.PropertyField(allowRichTextEditingProp);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
