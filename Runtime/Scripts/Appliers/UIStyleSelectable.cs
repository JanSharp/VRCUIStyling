namespace JanSharp
{
    public class UIStyleSelectable : UIStyleApplier, VRC.SDKBase.IEditorOnly
    {
        public bool controlInteractable = false;
        public bool controlTransition = true;
        public bool controlNormalColor = true;
        public bool controlHighlightedColor = true;
        public bool controlPressedColor = true;
        public bool controlSelectedColor = true;
        public bool controlDisabledColor = true;
        public bool controlColorMultiplier = true;
        public bool controlFadeDuration = true;
        public bool controlHighlightedSprite = true;
        public bool controlPressedSprite = true;
        public bool controlSelectedSprite = true;
        public bool controlDisabledSprite = true;
        public bool controlNavigation = true;
    }
}
