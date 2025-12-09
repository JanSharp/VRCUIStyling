using System.Linq;
using UnityEditor;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleSelectableProfile))]
    public class UIStyleSelectableProfileEditor : UIStyleProfileEditor
    {
        [InitializeOnLoadMethod]
        public static void OnAssemblyLoad()
        {
            RegisterSelectableColorSpriteFields<UIStyleSelectableProfile>();
        }

        protected static void RegisterSelectableColorSpriteFields<T>(
            string[] extraColorFields = null,
            string[] extraSpriteFields = null)
            where T : UIStyleSelectableProfile
        {
            string[] colorFields = new string[]
            {
                nameof(UIStyleSelectableProfile.normalColor),
                nameof(UIStyleSelectableProfile.highlightedColor),
                nameof(UIStyleSelectableProfile.pressedColor),
                nameof(UIStyleSelectableProfile.selectedColor),
                nameof(UIStyleSelectableProfile.disabledColor),
            };
            string[] spriteFields = new string[]
            {
                nameof(UIStyleSelectableProfile.highlightedSprite),
                nameof(UIStyleSelectableProfile.pressedSprite),
                nameof(UIStyleSelectableProfile.selectedSprite),
                nameof(UIStyleSelectableProfile.disabledSprite),
            };
            if (extraColorFields != null)
                colorFields = colorFields.Union(extraColorFields).ToArray();
            if (extraSpriteFields != null)
                spriteFields = spriteFields.Union(extraSpriteFields).ToArray();
            UIStyleProfileContainerUtil.RegisterColorFields<T>(colorFields);
            UIStyleProfileContainerUtil.RegisterSpriteFields<T>(spriteFields);
        }

        private SerializedProperty interactableProp;
        private SerializedProperty transitionProp;
        private SerializedProperty normalColorProp;
        private SerializedProperty highlightedColorProp;
        private SerializedProperty pressedColorProp;
        private SerializedProperty selectedColorProp;
        private SerializedProperty disabledColorProp;
        private SerializedProperty colorMultiplierProp;
        private SerializedProperty fadeDurationProp;
        private SerializedProperty highlightedSpriteProp;
        private SerializedProperty pressedSpriteProp;
        private SerializedProperty selectedSpriteProp;
        private SerializedProperty disabledSpriteProp;
        private SerializedProperty navigationProp;

        public override void OnEnable()
        {
            base.OnEnable();
            interactableProp = serializedObject.FindProperty(nameof(UIStyleSelectableProfile.interactable));
            transitionProp = serializedObject.FindProperty(nameof(UIStyleSelectableProfile.transition));
            normalColorProp = serializedObject.FindProperty(nameof(UIStyleSelectableProfile.normalColor));
            highlightedColorProp = serializedObject.FindProperty(nameof(UIStyleSelectableProfile.highlightedColor));
            pressedColorProp = serializedObject.FindProperty(nameof(UIStyleSelectableProfile.pressedColor));
            selectedColorProp = serializedObject.FindProperty(nameof(UIStyleSelectableProfile.selectedColor));
            disabledColorProp = serializedObject.FindProperty(nameof(UIStyleSelectableProfile.disabledColor));
            colorMultiplierProp = serializedObject.FindProperty(nameof(UIStyleSelectableProfile.colorMultiplier));
            fadeDurationProp = serializedObject.FindProperty(nameof(UIStyleSelectableProfile.fadeDuration));
            highlightedSpriteProp = serializedObject.FindProperty(nameof(UIStyleSelectableProfile.highlightedSprite));
            pressedSpriteProp = serializedObject.FindProperty(nameof(UIStyleSelectableProfile.pressedSprite));
            selectedSpriteProp = serializedObject.FindProperty(nameof(UIStyleSelectableProfile.selectedSprite));
            disabledSpriteProp = serializedObject.FindProperty(nameof(UIStyleSelectableProfile.disabledSprite));
            navigationProp = serializedObject.FindProperty(nameof(UIStyleSelectableProfile.navigation));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawProfileEditor();
            DrawSelectableFields();
            serializedObject.ApplyModifiedProperties();
        }

        protected void DrawSelectableFields()
        {
            EditorGUILayout.PropertyField(interactableProp);
            EditorGUILayout.PropertyField(transitionProp);
            DrawColorSelectorField(normalColorProp);
            DrawColorSelectorField(highlightedColorProp);
            DrawColorSelectorField(pressedColorProp);
            DrawColorSelectorField(selectedColorProp);
            DrawColorSelectorField(disabledColorProp);
            EditorGUILayout.PropertyField(colorMultiplierProp);
            EditorGUILayout.PropertyField(fadeDurationProp);
            DrawSpriteSelectorField(highlightedSpriteProp);
            DrawSpriteSelectorField(pressedSpriteProp);
            DrawSpriteSelectorField(selectedSpriteProp);
            DrawSpriteSelectorField(disabledSpriteProp);
            EditorGUILayout.PropertyField(navigationProp);
        }
    }
}
