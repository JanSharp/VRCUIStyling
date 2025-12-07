using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace JanSharp
{
    public static class UIStyleApplierUtil
    {
        public static UIStyleRoot GetRoot(IEnumerable<UIStyleApplier> targets, out string errorMsg)
        {
            UIStyleRoot[] roots = targets
                .Select(p => p.GetComponentInParent<UIStyleRoot>(includeInactive: true))
                .ToArray();
            if (roots.All(c => c == null))
            {
                errorMsg = "Missing UI Style Root in parents.";
                return null;
            }
            if (roots.Any(c => c == null))
            {
                errorMsg = "Some selected objects are not a child of any UI Style Root.";
                return null;
            }
            if (roots.Distinct().Count() != 1)
            {
                errorMsg = "Selected objects are children of different UI Style Roots.";
                return null;
            }
            errorMsg = null;
            return roots[0];
        }
    }

    public abstract class UIStyleApplierEditor<T> : Editor
        where T : UIStyleProfile
    {
        private UIStyleRoot root;
        private UIStyleProfileContainer container;
        private string errorMsg;
        private SerializedProperty profileNameProp;
        private string[] profileNames = new string[] { "" };
        private static HashSet<string> visitedNames = new();
        protected bool IsValid => errorMsg == null;

        public void OnEnable()
        {
            profileNameProp = serializedObject.FindProperty(nameof(UIStyleApplier.profileName));

            if (!GetRoot())
                return;
            if (!GetContainer())
                return;
            if (!GetProfileNames())
                return;
        }

        private bool GetRoot()
        {
            root = UIStyleApplierUtil.GetRoot(targets.Cast<UIStyleApplier>(), out errorMsg);
            return root != null;
        }

        private bool GetContainer()
        {
            container = root.profileContainer;
            if (container == null)
            {
                errorMsg = "The UI Style Root is missing the reference to a UI Style Profile Container.";
                return false;
            }
            return true;
        }

        private bool GetProfileNames()
        {
            List<string> profileNamesList = new() { "" };
            foreach (UIStyleProfile profile in container.GetComponentsInChildren<T>(includeInactive: true))
            {
                if (!UIStyleProfileContainerUtil.IsProfileActive(profile))
                    continue;
                if (UIStylingEditorUtil.HasEmptyProfileName(profile) || UIStylingEditorUtil.HasLeadingTrailingWhitespace(profile))
                {
                    errorMsg = "Some UI Style Profiles have invalid Profile Names.";
                    return false;
                }
                profileNamesList.Add(profile.profileName);
            }
            profileNames = profileNamesList.ToArray();
            return true;
        }

        public override void OnInspectorGUI()
        {
            if (!IsValid)
            {
                using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                    GUILayout.Label(errorMsg, EditorStyles.wordWrappedLabel);
                EditorGUILayout.Space();
            }
            serializedObject.Update();
            UIStyleProfileUtil.DrawSelectorField(profileNameProp, !IsValid, profileNames);
            EditorGUILayout.Space();
            DrawPropertiesExcluding(serializedObject, "m_Script", nameof(UIStyleApplier.profileName));
            serializedObject.ApplyModifiedProperties();
        }
    }
}
