using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace JanSharp
{
    [InitializeOnLoad]
    [DefaultExecutionOrder(-1000)]
    public static class UIStyleProfileContainerUtil
    {
        private static Dictionary<System.Type, string[]> colorFieldsByType = new();
        private static Dictionary<System.Type, string[]> spriteFieldsByType = new();

        static UIStyleProfileContainerUtil()
        {
            colorFieldsByType.Clear();
            spriteFieldsByType.Clear();
        }

        public static void RegisterColorFields<T>(string[] fieldNames)
            where T : UIStyleProfile
        {
            colorFieldsByType.Add(typeof(T), fieldNames);
        }

        public static void RegisterSpriteFields<T>(string[] fieldNames)
            where T : UIStyleProfile
        {
            spriteFieldsByType.Add(typeof(T), fieldNames);
        }

        public static IEnumerable<UIStyleProfile> GetActiveProfiles(UIStyleProfileContainer container)
        {
            return container.GetComponentsInChildren<UIStyleProfile>(includeInactive: true)
                .Where(IsProfileActive);
        }

        public static bool IsProfileActive(UIStyleProfile profile) => profile.gameObject.activeSelf;

        public static bool Validate(ValidationContext context)
        {
            bool result = true;
            context.profilesByName.Clear();

            if (context.colorPallet == null)
            {
                result = false;
                context.AddValidationError(new MissingColorSpritePalletError(isSprite: false));
            }
            else if (!UIStyleColorPalletUtil.Validate(context))
                result = false;

            if (context.spritePallet == null)
            {
                result = false;
                context.AddValidationError(new MissingColorSpritePalletError(isSprite: true));
            }
            else if (!UIStyleSpritePalletUtil.Validate(context))
                result = false;

            if (!result) // Only validate profiles if both color and sprite pallets are valid.
                return false;

            foreach (var group in GetActiveProfiles(context.container).GroupBy(p => p.profileName))
                if (!group.Skip(1).Any())
                {
                    if (ValidateProfile(context, group.Single()))
                        context.profilesByName.Add(group.Key, group.Single());
                    else
                        result = false;
                }
                else
                {
                    result = false;
                    context.AddValidationError(new DuplicateProfileNameError(group.ToArray()));
                    foreach (var profile in group)
                        ValidateProfile(context, profile);
                }

            return result;
        }

        private static bool ValidateProfile(ValidationContext context, UIStyleProfile profile)
        {
            bool result = true;
            System.Type profileType = profile.GetType();

            if (UIStylingEditorUtil.HasEmptyProfileName(profile))
            {
                result = false;
                context.AddValidationError(new EmptyProfileNameError(profile));
            }

            if (UIStylingEditorUtil.HasLeadingTrailingWhitespace(profile))
            {
                result = false;
                context.AddValidationError(new LeadingTrailingWhiteSpaceProfileNameError(profile));
            }

            if (colorFieldsByType.TryGetValue(profileType, out string[] fieldNames))
                foreach (string fieldName in fieldNames)
                {
                    string colorName = (string)profileType.GetField(fieldName).GetValue(profile) ?? "";
                    if (UIStylingEditorUtil.HasLeadingTrailingWhitespace(colorName))
                    {
                        result = false;
                        context.AddValidationError(new LeadingTrailingWhiteSpaceColorSpriteReferenceError(
                            profile,
                            fieldName,
                            colorName,
                            isSprite: false));
                        continue;
                    }
                    if (!context.colorsByName.ContainsKey(colorName))
                    {
                        result = false;
                        context.AddValidationError(new InvalidColorSpriteReferenceError(
                            profile,
                            fieldName,
                            colorName,
                            isSprite: false));
                        continue;
                    }
                }

            if (spriteFieldsByType.TryGetValue(profileType, out fieldNames))
                foreach (string fieldName in fieldNames)
                {
                    string spriteName = (string)profileType.GetField(fieldName).GetValue(profile) ?? "";
                    if (UIStylingEditorUtil.HasLeadingTrailingWhitespace(spriteName))
                    {
                        result = false;
                        context.AddValidationError(new LeadingTrailingWhiteSpaceColorSpriteReferenceError(
                            profile,
                            fieldName,
                            spriteName,
                            isSprite: true));
                        continue;
                    }
                    if (!context.spritesByName.ContainsKey(spriteName))
                    {
                        result = false;
                        context.AddValidationError(new InvalidColorSpriteReferenceError(
                            profile,
                            fieldName,
                            spriteName,
                            isSprite: true));
                        continue;
                    }
                }

            return result;
        }

        public static bool ValidateCustomScript(ValidationContext context, UIStyleCustomScriptCache cached, Component customScript)
        {
            bool result = true;

            System.Type customScriptType = customScript.GetType();

            foreach (var pair in cached.colorFieldPairs)
            {
                string colorName = (string)customScriptType.GetField(pair.nameFieldName).GetValue(customScript) ?? "";
                pair.nameFieldValue = colorName;
                if (UIStylingEditorUtil.HasLeadingTrailingWhitespace(colorName))
                {
                    result = false;
                    context.AddValidationError(new LeadingTrailingWhiteSpaceCustomColorSpriteReferenceError(
                        customScript,
                        pair.nameFieldName,
                        colorName,
                        isSprite: false));
                    continue;
                }
                if (!context.colorsByName.ContainsKey(colorName))
                {
                    result = false;
                    context.AddValidationError(new InvalidCustomColorSpriteReferenceError(
                        customScript,
                        pair.nameFieldName,
                        colorName,
                        isSprite: false));
                    continue;
                }
            }

            foreach (var pair in cached.spriteFieldPairs)
            {
                string spriteName = (string)customScriptType.GetField(pair.nameFieldName).GetValue(customScript) ?? "";
                pair.nameFieldValue = spriteName;
                if (UIStylingEditorUtil.HasLeadingTrailingWhitespace(spriteName))
                {
                    result = false;
                    context.AddValidationError(new LeadingTrailingWhiteSpaceCustomColorSpriteReferenceError(
                        customScript,
                        pair.nameFieldName,
                        spriteName,
                        isSprite: true));
                    continue;
                }
                if (!context.spritesByName.ContainsKey(spriteName))
                {
                    result = false;
                    context.AddValidationError(new InvalidCustomColorSpriteReferenceError(
                        customScript,
                        pair.nameFieldName,
                        spriteName,
                        isSprite: true));
                    continue;
                }
            }

            return result;
        }
    }
}
