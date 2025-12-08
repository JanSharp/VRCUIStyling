using UnityEngine.UI;

namespace JanSharp
{
    public class UIStyleScrollRectProfile : UIStyleProfile, VRC.SDKBase.IEditorOnly
    {
        public bool horizontal = true;
        public bool vertical = true;
        public ScrollRect.MovementType movementType = ScrollRect.MovementType.Elastic;
        public float elasticity = 0.1f;
        public bool inertia = true;
        public float decelerationRate = 0.135f;
        public float scrollSensitivity = 1f;
        public ScrollRect.ScrollbarVisibility horizontalVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
        public float horizontalSpacing = 0f;
        public ScrollRect.ScrollbarVisibility verticalVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
        public float verticalSpacing = 0f;
    }
}
