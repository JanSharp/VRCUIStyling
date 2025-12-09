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

        public static bool ContextMenuAddApplierValidation<T>(MenuCommand menuCommand)
            where T : UIStyleApplier
        {
            Component target = (Component)menuCommand.context;
            return !target.TryGetComponent<T>(out _);
        }

        public static void ContextMenuAddApplier<T>(MenuCommand menuCommand)
            where T : UIStyleApplier
        {
            Component target = (Component)menuCommand.context;
            T applier = target.gameObject.AddComponent<T>();
            Undo.RegisterCreatedObjectUndo(applier, $"Add {typeof(T).Name}");

            Component[] components = target.gameObject.GetComponents<Component>();
            int targetIndex = System.Array.IndexOf(components, target);
            int applierIndex = System.Array.IndexOf(components, applier);
            if (targetIndex < 0 || applierIndex < 0)
                throw new System.Exception("[UIStyling] Impossible.");

            for (int i = 0; i < applierIndex - targetIndex - 1; i++)
                UnityEditorInternal.ComponentUtility.MoveComponentUp(applier);
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
        private bool controlsFoldedOut = false;
        private int successfulApplicationCount = 0;
        private List<UIStyleRoot> rootsWithValidationErrors;

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
            if (GUILayout.Button("Apply Style"))
                ApplyStyleForAll();
            DrawSuccessInfoBox();
            EditorGUILayout.Space();
            if (controlsFoldedOut = EditorGUILayout.Foldout(controlsFoldedOut, "Controls", toggleOnLabelClick: true))
                DrawControls();
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawControls()
        {
            SerializedProperty iter = serializedObject.GetIterator();
            if (iter.NextVisible(true))
                do
                    if (iter.name != "m_Script" && iter.name != nameof(UIStyleApplier.profileName))
                        DrawControl(iter);
                while (iter.NextVisible(false));
        }

        private void DrawControl(SerializedProperty prop)
        {
            Rect rect = EditorGUILayout.GetControlRect(hasLabel: true);
            using (new EditorGUI.PropertyScope(rect, label: null, prop))
                prop.boolValue = EditorGUI.ToggleLeft(rect, new GUIContent(prop.displayName), prop.boolValue);
        }

        private void ApplyStyleForAll()
        {
            successfulApplicationCount = UIStyleRootEditor.ApplyStylesForAll(
                targets.Cast<UIStyleApplier>(),
                out rootsWithValidationErrors);
        }

        private void DrawSuccessInfoBox()
        {
            if (rootsWithValidationErrors == null)
                return;
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                if (targets.Length == successfulApplicationCount)
                    GUILayout.Label("Successfully applied UI Style!", EditorStyles.wordWrappedLabel);
                else
                {
                    if (successfulApplicationCount == 0)
                        GUILayout.Label($"Failed to apply UI Style", EditorStyles.wordWrappedLabel);
                    else
                        GUILayout.Label($"Successfully applied UI Style for "
                            + $"{successfulApplicationCount}/{targets.Length}", EditorStyles.wordWrappedLabel);

                    if (rootsWithValidationErrors.Count == 0)
                        GUILayout.Label($"See Console log for errors", EditorStyles.wordWrappedLabel);
                    else
                    {
                        GUILayout.Label($"See Console log or UI Style {(rootsWithValidationErrors.Count == 1 ? "Root" : "Roots")} "
                            + "for validation errors:", EditorStyles.wordWrappedLabel);
                        foreach (UIStyleRoot root in rootsWithValidationErrors)
                            EditorGUILayout.ObjectField(root, typeof(UIStyleRoot), allowSceneObjects: true);

                    }

                }
        }
    }
}
