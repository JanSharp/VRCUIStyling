using UnityEngine;
using UnityEngine.UI;

namespace JanSharp
{
    public class UIStyleSelectableProfile : UIStyleProfile, VRC.SDKBase.IEditorOnly
    {
        public bool interactable = true;
        public Selectable.Transition transition = Selectable.Transition.ColorTint;
        public string normalColor;
        public string highlightedColor;
        public string pressedColor;
        public string selectedColor;
        public string disabledColor;
        [Range(1, 5)]
        public float colorMultiplier = ColorBlock.defaultColorBlock.colorMultiplier;
        public float fadeDuration = ColorBlock.defaultColorBlock.fadeDuration;
        public string highlightedSprite;
        public string pressedSprite;
        public string selectedSprite;
        public string disabledSprite;
        [Tooltip("Only Mode and Wrap Around are used.")]
        public Navigation navigation;
    }
}
