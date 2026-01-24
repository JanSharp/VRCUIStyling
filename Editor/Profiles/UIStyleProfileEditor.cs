using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace JanSharp
{
    public static class UIStyleProfileUtil
    {
        public static bool TryGetContainer(IEnumerable<UIStyleProfile> targets, out UIStyleProfileContainer container, out string errorMsg)
        {
            container = null;
            UIStyleProfileContainer[] containers = targets
                .Select(p => p.GetComponentInParent<UIStyleProfileContainer>(includeInactive: true))
                .ToArray();
            if (containers.All(c => c == null))
            {
                errorMsg = "Missing UI Style Profile Container in parents.";
                return false;
            }
            if (containers.Any(c => c == null))
            {
                errorMsg = "Some selected objects are not a child of any UI Style Profile Container.";
                return false;
            }
            if (containers.Distinct().Count() != 1)
            {
                errorMsg = "Selected objects are children of different UI Style Profile Containers.";
                return false;
            }
            container = containers[0];
            errorMsg = null;
            return true;
        }

        public static bool TryGetContainerFromRoots(IEnumerable<Component> targets, out UIStyleProfileContainer container, out string errorMsg)
        {
            container = null;
            UIStyleProfileContainer[] containers = targets
                .Select(p => p.GetComponentInParent<UIStyleRoot>(includeInactive: true)?.profileContainer)
                .ToArray();
            if (containers.All(c => c == null))
            {
                errorMsg = "Missing UI Style Root in parents.";
                return false;
            }
            if (containers.Any(c => c == null))
            {
                errorMsg = "Some selected objects are not a child of any UI Style Root.";
                return false;
            }
            if (containers.Distinct().Count() != 1)
            {
                errorMsg = "Selected objects are children of different UI Style Roots referencing different "
                    + "UI Style Profile Containers.";
                return false;
            }
            container = containers[0];
            errorMsg = null;
            return true;
        }

        public static bool TryGetColorsNames(UIStyleProfileContainer container, out string[] colorNames, out string errorMsg)
        {
            UIStyleColorEntry[] colors = container.colorPallet.colors;
            if (colors == null)
            {
                errorMsg = "The UI Style Profile Container is missing its UI Style Color Pallet.";
                colorNames = new string[] { "" };
                return false;
            }
            colorNames = new string[colors.Length + 1];
            colorNames[0] = "";
            HashSet<string> visitedNames = new();
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
            errorMsg = null;
            return true;
        }

        public static bool TryGetSpriteNames(UIStyleProfileContainer container, out string[] spriteNames, out string errorMsg)
        {
            UIStyleSpriteEntry[] sprites = container.spritePallet.sprites;
            if (sprites == null)
            {
                errorMsg = "The UI Style Profile Container is missing its UI Style Sprite Pallet.";
                spriteNames = new string[] { "" };
                return false;
            }
            spriteNames = new string[sprites.Length + 1];
            spriteNames[0] = "";
            HashSet<string> visitedNames = new();
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
            errorMsg = null;
            return true;
        }

        public static void DrawSelectorFieldLayout(SerializedProperty prop, bool disabled, string[] names)
            => DrawSelectorFieldLayout(prop, disabled, names, p => EditorGUILayout.PropertyField(p));

        public static void DrawSelectorFieldLayout(SerializedProperty prop, GUIContent label, bool disabled, string[] names)
            => DrawSelectorFieldLayout(prop, disabled, names, p => EditorGUILayout.PropertyField(p, label));

        private static void DrawSelectorFieldLayout(SerializedProperty prop, bool disabled, string[] names, System.Action<SerializedProperty> drawProp)
        {
            using (new GUILayout.HorizontalScope())
            {
                drawProp(prop);
                using (new EditorGUI.DisabledScope(disabled))
                {
                    int index = EditorGUILayout.Popup(0, names, GUILayout.Width(20f));
                    if (index != 0)
                        prop.stringValue = names[index];
                }
            }
        }

        public static void DrawSelectorField(Rect rect, SerializedProperty prop, string errorMsg, string[] names)
            => DrawSelectorField(rect, prop, errorMsg, names, (r, p) => EditorGUI.PropertyField(r, p));

        public static void DrawSelectorField(Rect rect, SerializedProperty prop, GUIContent label, string errorMsg, string[] names)
            => DrawSelectorField(rect, prop, errorMsg, names, (r, p) => EditorGUI.PropertyField(r, p, label));

        private static void DrawSelectorField(Rect rect, SerializedProperty prop, string errorMsg, string[] names, System.Action<Rect, SerializedProperty> drawProp)
        {
            float width = rect.width;
            rect.width -= 2f + 20f + 2f + 50f;
            drawProp(rect, prop);
            rect.width = 20f;
            rect.x += width - rect.width - 2f - 50f;
            EditorGUI.BeginDisabledGroup(errorMsg != null);
            int index = EditorGUI.Popup(rect, 0, names);
            if (index != 0)
                prop.stringValue = names[index];
            rect.width = 50f;
            rect.x += 2f + 20f;
            if (GUI.Button(rect, new GUIContent("Apply", errorMsg)))
                UIStyleRootEditor.ApplyStylesForAll(prop.serializedObject.targetObjects.Cast<Component>(), out _);
            EditorGUI.EndDisabledGroup();
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
        protected bool IsValid => errorMsg == null;

        public virtual void OnEnable()
        {
            isEditingMultipleObjects = targets.Length > 1;
            profileNameProp = serializedObject.FindProperty(nameof(UIStyleProfile.profileName));

            if (!UIStyleProfileUtil.TryGetContainer(targets.Cast<UIStyleProfile>(), out container, out errorMsg))
                return;
            GetOtherProfileNames();
            if (!UIStyleProfileUtil.TryGetColorsNames(container, out colorNames, out errorMsg))
                return;
            if (!UIStyleProfileUtil.TryGetSpriteNames(container, out spriteNames, out errorMsg))
                return;
        }

        public virtual void OnDisable()
        {
            otherProfileNamesLut.Clear();
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
            => UIStyleProfileUtil.DrawSelectorFieldLayout(prop, !IsValid, colorNames);
        protected void DrawColorSelectorField(SerializedProperty prop, GUIContent label)
            => UIStyleProfileUtil.DrawSelectorFieldLayout(prop, label, !IsValid, colorNames);

        protected void DrawSpriteSelectorField(SerializedProperty prop)
            => UIStyleProfileUtil.DrawSelectorFieldLayout(prop, !IsValid, spriteNames);
        protected void DrawSpriteSelectorField(SerializedProperty prop, GUIContent label)
            => UIStyleProfileUtil.DrawSelectorFieldLayout(prop, label, !IsValid, spriteNames);
    }
}
