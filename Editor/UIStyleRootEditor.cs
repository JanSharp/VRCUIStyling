using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JanSharp
{
    [InitializeOnLoad]
    [DefaultExecutionOrder(-1000)]
    public static class UIStyleRootUtil
    {
        private static Dictionary<System.Type, System.Type> associatedProfileTypeByApplierType = new();
        private static Dictionary<System.Type, System.Type> associatedComponentTypeByApplierType = new();
        private static Dictionary<System.Type, System.Action<ValidationContext, UIStyleApplier, UIStyleProfile, UIBehaviour>> applyStyleFuncByType = new();

        static UIStyleRootUtil()
        {
            applyStyleFuncByType.Clear();
        }

        public static void RegisterApplyStyleFunc<TApplier, TProfile, TUIComponent>(
            System.Action<ValidationContext, TApplier, TProfile, TUIComponent> applyStyle)
            where TApplier : UIStyleApplier
            where TProfile : UIStyleProfile
            where TUIComponent : UIBehaviour
        {
            associatedProfileTypeByApplierType.Add(typeof(TApplier), typeof(TProfile));
            associatedComponentTypeByApplierType.Add(typeof(TApplier), typeof(TUIComponent));
            applyStyleFuncByType.Add(typeof(TApplier), (c, a, p, t) => applyStyle(c, (TApplier)a, (TProfile)p, (TUIComponent)t));
        }

        public static bool Validate(ValidationContext context)
        {
            if (context.container == null)
                throw new System.Exception("Don't make me add another type of validation error for this, "
                    + "just don't even bother validating at this point. Please.");

            if (!UIStyleProfileContainerUtil.Validate(context))
                return false; // Cannot validate the root and its appliers with invalid profiles.

            bool result = true;

            context.appliers = context.root.GetComponentsInChildren<UIStyleApplier>(includeInactive: true);
            foreach (UIStyleApplier applier in context.appliers)
                if (!ValidateApplier(context, applier))
                    result = false;

            return result;
        }

        private static bool ValidateApplier(ValidationContext context, UIStyleApplier applier)
        {
            bool result = true;

            System.Type targetType = associatedComponentTypeByApplierType[applier.GetType()];
            if (!applier.TryGetComponent(targetType, out _))
            {
                result = false;
                context.AddValidationError(new ApplierMissingAssociatedTargetComponentError(applier, targetType));
            }

            if (UIStylingEditorUtil.IsEmptyName(applier.profileName))
            {
                context.AddValidationError(new EmptyProfileNameForApplierError(applier));
                return false;
            }

            if (!context.profilesByName.TryGetValue(applier.profileName, out UIStyleProfile profile))
            {
                context.AddValidationError(new MissingProfileReferenceError(applier));
                return false;
            }

            System.Type expectedProfileType = associatedProfileTypeByApplierType[applier.GetType()];
            if (profile.GetType() != expectedProfileType)
            {
                result = false;
                context.AddValidationError(new ApplierProfileTypeMismatchError(applier, profile, expectedProfileType));
            }

            return result;
        }

        public static void ApplyStyle(ValidationContext context, UIStyleApplier applier)
        {
            System.Type applierType = applier.GetType();
            System.Type targetType = associatedComponentTypeByApplierType[applierType];

            UIStyleProfile profile = context.profilesByName[applier.profileName];
            if (profile == null)
            {
                Debug.LogError($"[UIStyling] The associated profile by the name '{applier.profileName}' "
                    + $"for the {applierType.Name} on the object {applier.name} got destroyed "
                    + $"after validation has passed.", applier);
                return;
            }

            UIBehaviour target = (UIBehaviour)applier.GetComponent(targetType);
            if (target == null)
            {
                Debug.LogError($"[UIStyling] The associated {targetType.Name} component for the {applierType.Name} on "
                    + $"the object {applier.name} got destroyed after validation has passed.", applier);
                return;
            }

            applyStyleFuncByType[applierType](context, applier, profile, target);
        }
    }

    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleRoot))]
    public class UIStyleRootEditor : Editor
    {
        private SerializedProperty profileContainerProp;
        private static List<(UIStyleRoot root, ValidationContext context)> invalidContexts = new();
        private bool invalidContextsAreStale = true;
        private int successfulApplicationCount = 0;
        private HashSet<UIStyleRoot> targetsLut;

        public void OnEnable()
        {
            profileContainerProp = serializedObject.FindProperty(nameof(UIStyleRoot.profileContainer));
            targetsLut = targets.Cast<UIStyleRoot>().ToHashSet();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(profileContainerProp);
            serializedObject.ApplyModifiedProperties();

            if (targets.Cast<UIStyleRoot>().Any(r => r.profileContainer != null)
                && GUILayout.Button(new GUIContent("Apply UI Style", "Use the profiles from the Profile Container "
                    + "to apply styling to all UI Style components which are children of the UI Style Root.")))
            {
                ApplyStylesForAll();
            }

            DrawSuccessInfoBox();

            bool didDrawValidationErrors = false;
            foreach (var invalidContext in invalidContexts)
                if (targetsLut.Contains(invalidContext.root))
                {
                    DrawValidationErrors(invalidContext.root, invalidContext.context);
                    didDrawValidationErrors = true;
                }

            if (didDrawValidationErrors)
            {
                EditorGUILayout.Space();
                if (GUILayout.Button("Clear Validation Errors"))
                    invalidContexts.RemoveAll(c => targetsLut.Contains(c.root));
            }
        }

        private void ApplyStylesForAll()
        {
            successfulApplicationCount = 0;
            invalidContextsAreStale = false;
            invalidContexts.Clear();
            foreach (UIStyleRoot root in targets.Cast<UIStyleRoot>())
                if (root.profileContainer != null)
                    ApplyStyle(root);
        }

        private void ApplyStyle(UIStyleRoot root)
        {
            ValidationContext context = new(root);

            if (!UIStyleRootUtil.Validate(context))
            {
                invalidContexts.Add((root, context));
                bool single = context.validationErrors.Count == 1;
                Debug.Log($"[UIStyling] Begin of {context.validationErrors.Count} validation {(single ? "error" : "errors")} "
                    + $"for {root.name} (this message just helps visually split the log).", root);
                foreach (ValidationError error in context.validationErrors)
                    error.Log();
                Debug.LogError($"[UIStyling] There {(single ? "is" : "are")} {context.validationErrors.Count} "
                    + $"validation {(single ? "error" : "errors")} for {root.name}, see above.", root);
                return;
            }

            successfulApplicationCount++;
            foreach (UIStyleApplier applier in context.appliers)
                UIStyleRootUtil.ApplyStyle(context, applier);
        }

        private void DrawSuccessInfoBox()
        {
            if (successfulApplicationCount == 0)
                return;
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                if (targets.Length == successfulApplicationCount)
                    GUILayout.Label("Successfully applied UI Style!", EditorStyles.wordWrappedLabel);
                else
                    GUILayout.Label($"Successfully applied UI Style for {successfulApplicationCount}/{targets.Length}", EditorStyles.wordWrappedLabel);
        }

        private void DrawValidationErrors(UIStyleRoot root, ValidationContext context)
        {
            EditorGUILayout.Space();
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                bool single = context.validationErrors.Count == 1;
                GUILayout.Label($"{context.validationErrors.Count} Validation {(single ? "error" : "errors")}"
                    + (invalidContextsAreStale ? " (from previous attempt, press the button again if you have resolved the issues)" : ""),
                    EditorStyles.wordWrappedLabel);
                if (targets.Length > 1)
                    EditorGUILayout.ObjectField("Root In Question", root, typeof(UIStyleRoot), allowSceneObjects: true);
                foreach (var error in context.validationErrors)
                {
                    EditorGUILayout.Space();
                    error.Draw();
                }
            }
        }
    }
}
