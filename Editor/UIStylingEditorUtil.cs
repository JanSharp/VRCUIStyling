using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace JanSharp
{
    public static class UIStylingEditorUtil
    {
        public static bool IsEmptyName(string name) => string.IsNullOrEmpty(name);
        public static bool HasLeadingTrailingWhitespace(string name) => name.Trim().Length != name.Length;
        public static bool HasEmptyProfileName(UIStyleProfile profile) => IsEmptyName(profile.profileName);
        public static bool HasLeadingTrailingWhitespace(UIStyleProfile profile) => HasLeadingTrailingWhitespace(profile.profileName);
    }

    public class ValidationContext
    {
        public UIStyleRoot root;
        public UIStyleProfileContainer container;
        public UIStyleColorPallet colorPallet;
        public UIStyleSpritePallet spritePallet;

        public Dictionary<string, Color> colorsByName = new();
        public Dictionary<string, Sprite> spritesByName = new();
        public Dictionary<string, UIStyleProfile> profilesByName = new();
        public UIStyleApplier[] appliers;

        public List<ValidationError> validationErrors = new();

        public ValidationContext(UIStyleRoot root)
        {
            this.root = root;
            container = root?.profileContainer;
            colorPallet = container?.colorPallet;
            spritePallet = container?.spritePallet;
        }

        public void AddValidationError(ValidationError error)
        {
            error.context = this;
            validationErrors.Add(error);
        }
    }

    public abstract class ValidationError
    {
        public ValidationContext context;
        public abstract void Log();
        public abstract void Draw();
    }

    public class MissingColorSpritePalletError : ValidationError
    {
        public bool isSprite;
        public MissingColorSpritePalletError(bool isSprite) => this.isSprite = isSprite;
        private string GetErrorMessage() => $"Missing UI Style {(isSprite ? "Sprite" : "Color")} Pallet.";
        public override void Log() => Debug.LogError($"[UIStyling] {GetErrorMessage()}", context.container);
        public override void Draw() => GUILayout.Label(GetErrorMessage(), EditorStyles.wordWrappedLabel);
    }

    public class EmptyColorSpriteNameError : ValidationError
    {
        public bool isSprite;

        public EmptyColorSpriteNameError(bool isSprite) => this.isSprite = isSprite;

        private string GetErrorMessage() => $"The UI Style {(isSprite ? "Sprite" : "Color")} Pallet has "
            + $"an entry with an empty name which is invalid.";

        public override void Log() => Debug.LogError($"[UIStyling] {GetErrorMessage()}", isSprite ? context.spritePallet : context.colorPallet);

        public override void Draw()
        {
            GUILayout.Label(GetErrorMessage(), EditorStyles.wordWrappedLabel);
            EditorGUILayout.ObjectField(
                $"{(isSprite ? "Sprite" : "Color")} Pallet In Question",
                isSprite ? context.spritePallet : context.colorPallet,
                isSprite ? typeof(UIStyleSpritePallet) : typeof(UIStyleColorPallet),
                allowSceneObjects: true);
        }
    }

    public class LeadingTrailingWhiteSpaceColorSpriteNameError : ValidationError
    {
        public string name;
        public bool isSprite;

        public LeadingTrailingWhiteSpaceColorSpriteNameError(string name, bool isSprite)
        {
            this.name = name;
            this.isSprite = isSprite;
        }

        private string GetErrorMessage() => $"The UI Style {(isSprite ? "Sprite" : "Color")} Pallet has "
            + $"an entry with the name '{name}' which has leading and or trailing white space which is invalid.";

        public override void Log() => Debug.LogError($"[UIStyling] {GetErrorMessage()}", isSprite ? context.spritePallet : context.colorPallet);

        public override void Draw()
        {
            GUILayout.Label(GetErrorMessage(), EditorStyles.wordWrappedLabel);
            EditorGUILayout.ObjectField(
                $"{(isSprite ? "Sprite" : "Color")} Pallet In Question",
                isSprite ? context.spritePallet : context.colorPallet,
                isSprite ? typeof(UIStyleSpritePallet) : typeof(UIStyleColorPallet),
                allowSceneObjects: true);
        }
    }

    public class DuplicateColorSpriteNameError : ValidationError
    {
        public string name;
        public bool isSprite;

        public DuplicateColorSpriteNameError(string name, bool isSprite)
        {
            this.name = name;
            this.isSprite = isSprite;
        }

        private string GetErrorMessage() => $"Multiple {(isSprite ? "sprite" : "color")} entires are using the name {name}.";

        public override void Log() => Debug.LogError($"[UIStyling] {GetErrorMessage()}", isSprite ? context.spritePallet : context.colorPallet);

        public override void Draw()
        {
            GUILayout.Label(GetErrorMessage(), EditorStyles.wordWrappedLabel);
            EditorGUILayout.ObjectField(
                $"{(isSprite ? "Sprite" : "Color")} Pallet In Question",
                isSprite ? context.spritePallet : context.colorPallet,
                isSprite ? typeof(UIStyleSpritePallet) : typeof(UIStyleColorPallet),
                allowSceneObjects: true);
        }
    }

    public class EmptyProfileNameError : ValidationError
    {
        public UIStyleProfile profile;
        public System.Type profileType;

        public EmptyProfileNameError(UIStyleProfile profile)
        {
            this.profile = profile;
            profileType = profile.GetType(); // The profile could get deleted, prefetch the type.
        }

        private string GetErrorMessage() => $"The Profile Name must not be empty.";

        public override void Log() => Debug.LogError($"[UIStyling] {GetErrorMessage()}", profile);

        public override void Draw()
        {
            GUILayout.Label(GetErrorMessage(), EditorStyles.wordWrappedLabel);
            EditorGUILayout.ObjectField("Profile In Question", profile, profileType, allowSceneObjects: true);
        }
    }

    public class LeadingTrailingWhiteSpaceProfileNameError : ValidationError
    {
        public UIStyleProfile profile;
        public System.Type profileType;
        public string profileName;

        public LeadingTrailingWhiteSpaceProfileNameError(UIStyleProfile profile)
        {
            this.profile = profile;
            // The profile could get deleted, prefetch these.
            profileType = profile.GetType();
            profileName = profile.profileName;
        }

        private string GetErrorMessage() => $"The Profile Name ('{profileName}') "
            + $"must not have leading nor trailing white space.";

        public override void Log() => Debug.LogError($"[UIStyling] {GetErrorMessage()}", profile);

        public override void Draw()
        {
            GUILayout.Label(GetErrorMessage(), EditorStyles.wordWrappedLabel);
            EditorGUILayout.ObjectField("Profile In Question", profile, profileType, allowSceneObjects: true);
        }
    }

    public class DuplicateProfileNameError : ValidationError
    {
        public UIStyleProfile[] profiles;
        public System.Type[] profileTypes;
        public string profileName;

        public DuplicateProfileNameError(UIStyleProfile[] profiles)
        {
            this.profiles = profiles;
            // The profiles could get deleted, prefetch these.
            profileTypes = profiles.Select(p => p.GetType()).ToArray();
            profileName = profiles[0].profileName;
        }

        private string GetErrorMessage() => $"There are {profiles.Length} profiles "
            + $"trying to use the profile name '{profileName}'. Profile Names must be unique.";

        public override void Log() => Debug.LogError($"[UIStyling] {GetErrorMessage()}", profiles[0]);

        public override void Draw()
        {
            GUILayout.Label(GetErrorMessage(), EditorStyles.wordWrappedLabel);
            GUILayout.Label("Profiles In Question");
            using (new EditorGUI.IndentLevelScope())
                for (int i = 0; i < profiles.Length; i++)
                    EditorGUILayout.ObjectField(profiles[i], profileTypes[i], allowSceneObjects: true);
        }
    }

    public class LeadingTrailingWhiteSpaceColorSpriteReferenceError : ValidationError
    {
        public UIStyleProfile profile;
        public System.Type profileType;
        public string profileName;
        public string fieldName;
        public string colorSpriteName;
        public bool isSprite;

        public LeadingTrailingWhiteSpaceColorSpriteReferenceError(
            UIStyleProfile profile,
            string fieldName,
            string colorSpriteName,
            bool isSprite)
        {
            this.profile = profile;
            // The profiles could get deleted, prefetch these.
            profileType = profile.GetType();
            profileName = profile.profileName;
            this.fieldName = fieldName;
            this.colorSpriteName = colorSpriteName;
            this.isSprite = isSprite;
        }

        private string GetErrorMessage() => $"For the {profileType.Name} with the name '{profileName}', the "
            + $"{fieldName} is trying to reference a {(isSprite ? "Sprite" : "Color")} by the name '{colorSpriteName}' "
            + $"which has leading and or trailing white space which is invalid.";

        public override void Log() => Debug.LogError($"[UIStyling] {GetErrorMessage()}", profile);

        public override void Draw()
        {
            GUILayout.Label(GetErrorMessage(), EditorStyles.wordWrappedLabel);
            EditorGUILayout.ObjectField("Profile In Question", profile, profileType, allowSceneObjects: true);
        }
    }

    public class InvalidColorSpriteReferenceError : ValidationError
    {
        public UIStyleProfile profile;
        public System.Type profileType;
        public string profileName;
        public string fieldName;
        public string colorSpriteName;
        public bool isSprite;

        public InvalidColorSpriteReferenceError(
            UIStyleProfile profile,
            string fieldName,
            string colorSpriteName,
            bool isSprite)
        {
            this.profile = profile;
            // The profiles could get deleted, prefetch these.
            profileType = profile.GetType();
            profileName = profile.profileName;
            this.fieldName = fieldName;
            this.colorSpriteName = colorSpriteName;
            this.isSprite = isSprite;
        }

        private string GetErrorMessage() => $"For the {profileType.Name} with the name '{profileName}', the "
            + $"{fieldName} is trying to reference a {(isSprite ? "Sprite" : "Color")} by the name '{colorSpriteName}' "
            + $"however the UI Style {(isSprite ? "Sprite" : "Color")} Pallet does not define any with such a name.";

        public override void Log() => Debug.LogError($"[UIStyling] {GetErrorMessage()}", profile);

        public override void Draw()
        {
            GUILayout.Label(GetErrorMessage(), EditorStyles.wordWrappedLabel);
            EditorGUILayout.ObjectField("Profile In Question", profile, profileType, allowSceneObjects: true);
        }
    }

    public class LeadingTrailingWhiteSpaceCustomColorSpriteReferenceError : ValidationError
    {
        public Component customScript;
        public System.Type customScriptType;
        public string fieldName;
        public string colorSpriteName;
        public bool isSprite;

        public LeadingTrailingWhiteSpaceCustomColorSpriteReferenceError(
            Component profile,
            string fieldName,
            string colorSpriteName,
            bool isSprite)
        {
            this.customScript = profile;
            // The profiles could get deleted, prefetch these.
            customScriptType = profile.GetType();
            this.fieldName = fieldName;
            this.colorSpriteName = colorSpriteName;
            this.isSprite = isSprite;
        }

        private string GetErrorMessage() => $"For the custom script {customScriptType.Name}, the "
            + $"{fieldName} is trying to reference a {(isSprite ? "Sprite" : "Color")} by the name '{colorSpriteName}' "
            + $"which has leading and or trailing white space which is invalid.";

        public override void Log() => Debug.LogError($"[UIStyling] {GetErrorMessage()}", customScript);

        public override void Draw()
        {
            GUILayout.Label(GetErrorMessage(), EditorStyles.wordWrappedLabel);
            EditorGUILayout.ObjectField("Custom Script In Question", customScript, customScriptType, allowSceneObjects: true);
        }
    }

    public class InvalidCustomColorSpriteReferenceError : ValidationError
    {
        public Component customScript;
        public System.Type customScriptType;
        public string fieldName;
        public string colorSpriteName;
        public bool isSprite;

        public InvalidCustomColorSpriteReferenceError(
            Component customScript,
            string fieldName,
            string colorSpriteName,
            bool isSprite)
        {
            this.customScript = customScript;
            // The customScript could get deleted, prefetch this.
            customScriptType = customScript.GetType();
            this.fieldName = fieldName;
            this.colorSpriteName = colorSpriteName;
            this.isSprite = isSprite;
        }

        private string GetErrorMessage() => $"For the custom script {customScriptType.Name}, the "
            + $"{fieldName} is trying to reference a {(isSprite ? "Sprite" : "Color")} by the name '{colorSpriteName}' "
            + $"however the UI Style {(isSprite ? "Sprite" : "Color")} Pallet does not define any with such a name.";

        public override void Log() => Debug.LogError($"[UIStyling] {GetErrorMessage()}", customScript);

        public override void Draw()
        {
            GUILayout.Label(GetErrorMessage(), EditorStyles.wordWrappedLabel);
            EditorGUILayout.ObjectField("Custom Script In Question", customScript, customScriptType, allowSceneObjects: true);
        }
    }

    public class EmptyProfileNameForApplierError : ValidationError
    {
        public UIStyleApplier applier;
        public System.Type applierType;
        public string goName;

        public EmptyProfileNameForApplierError(UIStyleApplier applier)
        {
            this.applier = applier;
            // The applier could get deleted, prefetch these.
            applierType = applier.GetType();
            goName = applier.name;
        }

        private string GetErrorMessage() => $"The {applierType.Name} on the object {goName} does not have a "
            + $"Profile Name set for it to reference.";

        public override void Log() => Debug.LogError($"[UIStyling] {GetErrorMessage()}", applier);

        public override void Draw()
        {
            GUILayout.Label(GetErrorMessage(), EditorStyles.wordWrappedLabel);
            EditorGUILayout.ObjectField("Applier In Question", applier, applierType, allowSceneObjects: true);
        }
    }

    public class MissingProfileReferenceError : ValidationError
    {
        public UIStyleApplier applier;
        public System.Type applierType;
        public string goName;
        public string profileName;

        public MissingProfileReferenceError(UIStyleApplier applier)
        {
            this.applier = applier;
            // The applier could get deleted, prefetch these.
            applierType = applier.GetType();
            goName = applier.name;
            profileName = applier.profileName;
        }

        private string GetErrorMessage() => $"The {applierType.Name} on the object {goName} is "
            + $"trying to reference a profile by the name '{profileName}' which does not exist.";

        public override void Log() => Debug.LogError($"[UIStyling] {GetErrorMessage()}", applier);

        public override void Draw()
        {
            GUILayout.Label(GetErrorMessage(), EditorStyles.wordWrappedLabel);
            EditorGUILayout.ObjectField("Applier In Question", applier, applierType, allowSceneObjects: true);
        }
    }

    public class ApplierProfileTypeMismatchError : ValidationError
    {
        public UIStyleApplier applier;
        public UIStyleProfile profile;
        public System.Type expectedProfileType;
        public System.Type applierType;
        public System.Type profileType;
        public string applierGoName;
        public string profileName;

        public ApplierProfileTypeMismatchError(UIStyleApplier applier, UIStyleProfile profile, System.Type expectedProfileType)
        {
            this.applier = applier;
            this.profile = profile;
            this.expectedProfileType = expectedProfileType;
            // The applier || profile could get deleted, prefetch these.
            applierType = applier.GetType();
            profileType = profile.GetType();
            applierGoName = applier.name;
            profileName = applier.profileName;
        }

        private string GetErrorMessage() => $"The {applierType.Name} on the object {applierGoName} is "
            + $"referencing the profile by the name '{profileName}' which is a {profileType.Name}, however "
            + $"a {expectedProfileType.Name} must be referenced.";

        public override void Log() => Debug.LogError($"[UIStyling] {GetErrorMessage()}", applier);

        public override void Draw()
        {
            GUILayout.Label(GetErrorMessage(), EditorStyles.wordWrappedLabel);
            EditorGUILayout.ObjectField("Applier In Question", applier, applierType, allowSceneObjects: true);
            EditorGUILayout.ObjectField("Profile In Question", profile, profileType, allowSceneObjects: true);
        }
    }

    public class ApplierMissingAssociatedTargetComponentError : ValidationError
    {
        public UIStyleApplier applier;
        public System.Type applierType;
        public string goName;
        public System.Type targetType;

        public ApplierMissingAssociatedTargetComponentError(UIStyleApplier applier, System.Type targetType)
        {
            this.applier = applier;
            this.targetType = targetType;
            // The applier could get deleted, prefetch these.
            applierType = applier.GetType();
            goName = applier.name;
        }

        private string GetErrorMessage() => $"The {applierType.Name} on the object {goName} "
            + $"is missing a {targetType.Name} component on the object it is on.";

        public override void Log() => Debug.LogError($"[UIStyling] {GetErrorMessage()}", applier);

        public override void Draw()
        {
            GUILayout.Label(GetErrorMessage(), EditorStyles.wordWrappedLabel);
            EditorGUILayout.ObjectField("Applier In Question", applier, applierType, allowSceneObjects: true);
        }
    }

    public class InvalidAttributesError : ValidationError
    {
        public InvalidAttributesError() { }

        public override void Log() { }

        public override void Draw()
        {
            GUILayout.Label("Some UIStyleColor and or UIStyleSprite attributes in custom scripts are invalid, "
                + "check the console for details.", EditorStyles.wordWrappedLabel);
        }
    }
}
