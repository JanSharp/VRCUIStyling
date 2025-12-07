using System.Collections.Generic;

namespace JanSharp
{
    public static class UIStyleSpritePalletUtil
    {
        public static bool Validate(ValidationContext context)
        {
            bool result = true;
            context.spritesByName.Add("", null); // Empty string references are valid and resolve to null.
            HashSet<string> duplicateNames = new();
            foreach (UIStyleSpriteEntry entry in context.spritePallet.sprites)
            {
                if (UIStylingEditorUtil.IsEmptyName(entry.name))
                {
                    result = false;
                    context.AddValidationError(new EmptyColorSpriteNameError(isSprite: true));
                    continue;
                }
                if (UIStylingEditorUtil.HasLeadingTrailingWhitespace(entry.name))
                {
                    result = false;
                    context.AddValidationError(new LeadingTrailingWhiteSpaceColorSpriteNameError(entry.name, isSprite: true));
                    continue;
                }
                if (context.spritesByName.ContainsKey(entry.name))
                    duplicateNames.Add(entry.name);
                else
                    context.spritesByName.Add(entry.name, entry.sprite);
            }
            foreach (string name in duplicateNames)
            {
                context.spritesByName.Remove(name);
                result = false;
                context.AddValidationError(new DuplicateColorSpriteNameError(name, isSprite: true));
            }
            return result;
        }
    }
}
