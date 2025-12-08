using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace JanSharp
{
    public static class UIStyleProfileUtil
    {
        public static UIStyleProfileContainer GetContainer(IEnumerable<UIStyleProfile> targets, out string errorMsg)
        {
            UIStyleProfileContainer[] containers = targets
                .Select(p => p.GetComponentInParent<UIStyleProfileContainer>(includeInactive: true))
                .ToArray();
            if (containers.All(c => c == null))
            {
                errorMsg = "Missing UI Style Profile Container in parents.";
                return null;
            }
            if (containers.Any(c => c == null))
            {
                errorMsg = "Some selected objects are not a child of any UI Style Profile Container.";
                return null;
            }
            if (containers.Distinct().Count() != 1)
            {
                errorMsg = "Selected objects are children of different UI Style Profile Containers.";
                return null;
            }
            errorMsg = null;
            return containers[0];
        }

        public static void DrawSelectorField(SerializedProperty prop, bool disabled, string[] names)
            => DrawSelectorField(prop, disabled, names, p => EditorGUILayout.PropertyField(prop));

        public static void DrawSelectorField(SerializedProperty prop, GUIContent label, bool disabled, string[] names)
            => DrawSelectorField(prop, disabled, names, p => EditorGUILayout.PropertyField(prop, label));

        private static void DrawSelectorField(SerializedProperty prop, bool disabled, string[] names, System.Action<SerializedProperty> drawProp)
        {
            using (new EditorGUI.DisabledScope(disabled))
            using (new GUILayout.HorizontalScope())
            {
                drawProp(prop);
                int index = EditorGUILayout.Popup(0, names, GUILayout.Width(20f));
                if (index != 0)
                    prop.stringValue = names[index];
            }
        }
    }

    public abstract class UIStyleProfileEditor : Editor
    {
        protected SerializedProperty profileNameProp;
        private HashSet<string> otherProfileNamesLut = new();
        private bool isEditingMultipleObjects;

        protected UIStyleProfileContainer container;
        protected string errorMsg;
        protected string[] colorNames = new string[] { "" };
        protected string[] spriteNames = new string[] { "" };
        private static HashSet<string> visitedNames = new();
        protected bool IsValid => errorMsg == null;

        public virtual void OnEnable()
        {
            isEditingMultipleObjects = targets.Length > 1;
            profileNameProp = serializedObject.FindProperty(nameof(UIStyleProfile.profileName));

            if (!GetContainer())
                return;
            GetOtherProfileNames();
            if (!GetColorsNames())
                return;
            if (!GetSpriteNames())
                return;
        }

        public virtual void OnDisable()
        {
            otherProfileNamesLut.Clear();
        }

        private bool GetContainer()
        {
            container = UIStyleProfileUtil.GetContainer(targets.Cast<UIStyleProfile>(), out errorMsg);
            return container != null;
        }

        private void GetOtherProfileNames()
        {
            if (isEditingMultipleObjects)
                return;
            UIStyleProfile target = (UIStyleProfile)this.target;
            foreach (UIStyleProfile profile in UIStyleProfileContainerUtil.GetActiveProfiles(container))
                if (profile != target)
                    otherProfileNamesLut.Add(profile.profileName);
        }

        private bool GetColorsNames()
        {
            UIStyleColorEntry[] colors = container.colorPallet.colors;
            if (colors == null)
            {
                errorMsg = "The UI Style Profile Container is missing its UI Style Color Pallet.";
                return false;
            }
            colorNames = new string[colors.Length + 1];
            colorNames[0] = "";
            for (int i = 0; i < colors.Length; i++)
            {
                UIStyleColorEntry color = colors[i];
                if (!visitedNames.Add(color.name))
                {
                    visitedNames.Clear();
                    errorMsg = "Color Pallet contains duplicate names.";
                    return false;
                }
                colorNames[i + 1] = color.name;
            }
            visitedNames.Clear();
            return true;
        }

        private bool GetSpriteNames()
        {
            UIStyleSpriteEntry[] sprites = container.spritePallet.sprites;
            if (sprites == null)
            {
                errorMsg = "The UI Style Profile Container is missing its UI Style Sprite Pallet.";
                return false;
            }
            spriteNames = new string[sprites.Length + 1];
            spriteNames[0] = "";
            for (int i = 0; i < sprites.Length; i++)
            {
                UIStyleSpriteEntry sprite = sprites[i];
                if (!visitedNames.Add(sprite.name))
                {
                    visitedNames.Clear();
                    errorMsg = "Sprite Pallet contains duplicate names.";
                    return false;
                }
                spriteNames[i + 1] = sprite.name;
            }
            visitedNames.Clear();
            return true;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawProfileEditor();
            serializedObject.ApplyModifiedProperties();
        }

        protected void DrawProfileEditor()
        {
            if (!IsValid)
            {
                using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                    GUILayout.Label(errorMsg, EditorStyles.wordWrappedLabel);
                EditorGUILayout.Space();
            }

            using (new EditorGUI.DisabledScope(isEditingMultipleObjects))
                EditorGUILayout.PropertyField(profileNameProp);
            if (!isEditingMultipleObjects)
            {
                if (UIStylingEditorUtil.IsEmptyName(profileNameProp.stringValue))
                    using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                        GUILayout.Label("Profile Name must not be empty.", EditorStyles.wordWrappedLabel);
                else if (UIStylingEditorUtil.HasLeadingTrailingWhitespace(profileNameProp.stringValue))
                    using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                        GUILayout.Label("Profile Name must not have leading nor trailing white space.",
                            EditorStyles.wordWrappedLabel);
                else if (!UIStyleProfileContainerUtil.IsProfileActive((UIStyleProfile)target))
                    using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                        GUILayout.Label("Profile is inactive and will not be usable.",
                            EditorStyles.wordWrappedLabel);
                else if (otherProfileNamesLut.Contains(profileNameProp.stringValue))
                    using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                        GUILayout.Label("This name is already used by another profile. "
                            + "Names must be unique.", EditorStyles.wordWrappedLabel);
            }
            EditorGUILayout.Space();
        }

        protected void DrawColorSelectorField(SerializedProperty prop)
            => UIStyleProfileUtil.DrawSelectorField(prop, !IsValid, colorNames);
        protected void DrawColorSelectorField(SerializedProperty prop, GUIContent label)
            => UIStyleProfileUtil.DrawSelectorField(prop, label, !IsValid, colorNames);

        protected void DrawSpriteSelectorField(SerializedProperty prop)
            => UIStyleProfileUtil.DrawSelectorField(prop, !IsValid, spriteNames);
        protected void DrawSpriteSelectorField(SerializedProperty prop, GUIContent label)
            => UIStyleProfileUtil.DrawSelectorField(prop, label, !IsValid, spriteNames);
    }
}
