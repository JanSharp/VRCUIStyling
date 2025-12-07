using System.Collections.Generic;
using UnityEngine;

namespace JanSharp
{
    public static class UIStyleColorPalletUtil
    {
        public static bool Validate(ValidationContext context)
        {
            bool result = true;
            context.colorsByName.Add("", Color.white); // Empty string references are valid and resolve to white.
            HashSet<string> duplicateNames = new();
            foreach (UIStyleColorEntry entry in context.colorPallet.colors)
            {
                if (UIStylingEditorUtil.IsEmptyName(entry.name))
                {
                    result = false;
                    context.AddValidationError(new EmptyColorSpriteNameError(isSprite: false));
                    continue;
                }
                if (UIStylingEditorUtil.HasLeadingTrailingWhitespace(entry.name))
                {
                    result = false;
                    context.AddValidationError(new LeadingTrailingWhiteSpaceColorSpriteNameError(entry.name, isSprite: false));
                    continue;
                }
                if (context.colorsByName.ContainsKey(entry.name))
                    duplicateNames.Add(entry.name);
                else
                    context.colorsByName.Add(entry.name, entry.color);
            }
            foreach (string name in duplicateNames)
            {
                context.colorsByName.Remove(name);
                result = false;
                context.AddValidationError(new DuplicateColorSpriteNameError(name, isSprite: false));
            }
            return result;
        }
    }
}
