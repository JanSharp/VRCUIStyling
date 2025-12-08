using UnityEngine;

namespace JanSharp
{
    [DisallowMultipleComponent]
    public class UIStyleImage : UIStyleApplier, VRC.SDKBase.IEditorOnly
    {
        public bool controlSourceImage = true;
        public bool controlColor = true;
        public bool controlMaterial = true;
        public bool controlRaycastTarget = true;
        public bool controlRaycastPaddingLeft = true;
        public bool controlRaycastPaddingBottom = true;
        public bool controlRaycastPaddingRight = true;
        public bool controlRaycastPaddingTop = true;
        public bool controlMaskable = true;
        public bool controlImageType = true;
        public bool controlUseSpriteMesh = true;
        public bool controlPreserveAspect = true;
        public bool controlFillCenter = true;
        public bool controlPixelsPerUnitMultiplier = true;
        public bool controlFillMethod = true;
        public bool controlFillOrigin = true;
        public bool controlFillAmount = true;
        public bool controlClockwise = true;
    }
}
