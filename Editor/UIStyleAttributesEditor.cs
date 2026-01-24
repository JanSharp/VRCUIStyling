using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UdonSharp;
using UnityEditor;
using UnityEngine;

namespace JanSharp
{
    public class UIStyleCustomScriptCache
    {
        public System.Type ubType;
        public List<FieldPair> colorFieldPairs = new();
        public List<FieldPair> spriteFieldPairs = new();
        public Component[] instancesToApplyTo;

        public class FieldPair
        {
            public string nameFieldName;
            public string associatedFieldName;
            public string nameFieldValue;

            public FieldPair(string nameFieldName, string associatedFieldName)
            {
                this.nameFieldName = nameFieldName;
                this.associatedFieldName = associatedFieldName;
            }
        }

        public UIStyleCustomScriptCache(System.Type ubType)
        {
            this.ubType = ubType;
        }
    }

    [InitializeOnLoad]
    public static class UIStyleCustomColorAndSpriteRefs
    {
        /// <summary>
        /// <para>Contains only entires where <see cref="UIStyleCustomScriptCache.colorFieldPairs"/> or
        /// <see cref="UIStyleCustomScriptCache.spriteFieldPairs"/> contain at least one value.</para>
        /// </summary>
        private static List<UIStyleCustomScriptCache> ubTypeCache = new();
        private static Dictionary<System.Type, UIStyleCustomScriptCache> ubTypeCacheByType = new();
        private static List<System.Type> invalidUbTypes = new();
        private const BindingFlags PrivateAndPublicFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        static UIStyleCustomColorAndSpriteRefs()
        {
            ubTypeCache.Clear();
            ubTypeCacheByType.Clear();
            invalidUbTypes.Clear();
            foreach (System.Type ubType in OnAssemblyLoadUtil.AllUdonSharpBehaviourTypes)
                TryGenerateTypeCache(ubType);
        }

        private static void TryGenerateTypeCache(System.Type ubType, bool validateOnly = false)
        {
            UIStyleCustomScriptCache cached = validateOnly ? null : new(ubType);

            bool isValid = true;

            foreach (FieldInfo field in EditorUtil.GetFieldsIncludingBase(ubType, PrivateAndPublicFlags, stopAtType: typeof(UdonSharpBehaviour)))
            {
                isValid &= CheckForAttribute<UIStyleColorAttribute, Color>(ubType, field, cached.colorFieldPairs, a => a.ColorFieldName, validateOnly);
                isValid &= CheckForAttribute<UIStyleSpriteAttribute, Sprite>(ubType, field, cached.spriteFieldPairs, a => a.SpriteFieldName, validateOnly);
            }

            if (validateOnly)
                return;

            if (!isValid)
            {
                invalidUbTypes.Add(ubType);
                return;
            }

            if (cached.colorFieldPairs.Count != 0 || cached.spriteFieldPairs.Count != 0)
            {
                ubTypeCache.Add(cached);
                ubTypeCacheByType.Add(ubType, cached);
            }
        }

        private static bool CheckForAttribute<TAttribute, TAssociated>(
            System.Type ubType,
            FieldInfo field,
            List<UIStyleCustomScriptCache.FieldPair> cachedFieldPairs,
            System.Func<TAttribute, string> getAssociatedFieldName,
            bool validateOnly)
            where TAttribute : System.Attribute
        {
            TAttribute attr = field.GetCustomAttribute<TAttribute>(inherit: true);
            if (attr == null)
                return true;

            bool isValid = true;

            if (field.FieldType != typeof(string))
            {
                Debug.LogError($"[UIStyling] The {ubType.Name}.{field.Name} field has the {typeof(TAttribute).Name} "
                    + $"however its type is {field.FieldType.Name}. It must be a string.");
                isValid = false;
            }
            if (!EditorUtil.IsSerializedField(field))
            {
                Debug.LogError($"[UIStyling] The {ubType.Name}.{field.Name} field has the {typeof(TAttribute).Name} "
                    + $"however it is not a serialized field. It must either be public or have the {nameof(SerializeField)} attribute.");
                isValid = false;
            }

            string associatedFieldName = getAssociatedFieldName(attr) ?? "";
            FieldInfo defField = EditorUtil.GetFieldIncludingBase(ubType, associatedFieldName, PrivateAndPublicFlags);
            if (defField == null)
            {
                Debug.LogError($"[UIStyling] The {ubType.Name}.{field.Name} field has the {typeof(TAttribute).Name} "
                    + $"pointing to a {typeof(TAssociated).Name} field by the name '{associatedFieldName}' however no such field exists.");
                isValid = false;
            }
            if (defField != null && defField.FieldType != typeof(Color))
            {
                Debug.LogError($"[UIStyling] The {ubType.Name}.{field.Name} field has the {typeof(TAttribute).Name} "
                    + $"pointing to the field by the name '{associatedFieldName}' which has the type {defField.FieldType.Name}, "
                    + $"however it must be a {typeof(TAssociated).Name}.");
                isValid = false;
            }
            if (defField != null && !EditorUtil.IsSerializedField(defField))
            {
                Debug.LogError($"[UIStyling] The {ubType.Name}.{field.Name} field has the {typeof(TAttribute).Name} "
                    + $"pointing to the field by the name '{associatedFieldName}' which is not a serialized field. "
                    + $"It must either be public or have the {nameof(SerializeField)} attribute.");
                isValid = false;
            }

            if (isValid && !validateOnly)
                cachedFieldPairs.Add(new(field.Name, associatedFieldName));
            return isValid;
        }

        public static bool ValidateAttributes(ValidationContext context)
        {
            if (invalidUbTypes.Count == 0)
                return true;
            context.AddValidationError(new InvalidAttributesError());
            foreach (System.Type ubType in invalidUbTypes)
                TryGenerateTypeCache(ubType, validateOnly: true);
            return false;
        }

        public static bool ValidateInstances(ValidationContext context)
        {
            FindAllInstancesToApplyTo(context);
            bool isValid = true;
            foreach (UIStyleCustomScriptCache cached in ubTypeCache)
                foreach (Component customScript in cached.instancesToApplyTo)
                    isValid &= UIStyleProfileContainerUtil.ValidateCustomScript(context, cached, customScript);
            return isValid;
        }

        public static bool ValidateCustomScript(ValidationContext context, Component customScript)
        {
            return UIStyleProfileContainerUtil.ValidateCustomScript(context, ubTypeCacheByType[customScript.GetType()], customScript);
        }

        private static void FindAllInstancesToApplyTo(ValidationContext context)
        {
            foreach (UIStyleCustomScriptCache cached in ubTypeCache)
                cached.instancesToApplyTo = context.root.GetComponentsInChildren(cached.ubType);
        }

        public static void ApplyStyle(ValidationContext context)
        {
            foreach (UIStyleCustomScriptCache cached in ubTypeCache)
                foreach (Component customScript in cached.instancesToApplyTo)
                    ApplyStyle(context, cached, customScript);
        }

        public static void ApplyStyle(ValidationContext context, Component customScript)
        {
            ApplyStyle(context, ubTypeCacheByType[customScript.GetType()], customScript);
        }

        private static void ApplyStyle(ValidationContext context, UIStyleCustomScriptCache cached, Component customScript)
        {
            SerializedObject so = new(customScript);
            foreach (var pair in cached.colorFieldPairs)
                so.FindProperty(pair.associatedFieldName).colorValue = context.colorsByName[pair.nameFieldValue];
            foreach (var pair in cached.spriteFieldPairs)
                so.FindProperty(pair.associatedFieldName).objectReferenceValue = context.spritesByName[pair.nameFieldValue];
            so.ApplyModifiedProperties();
        }
    }
}
