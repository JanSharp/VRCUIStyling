using UnityEngine;

namespace JanSharp
{
    [DisallowMultipleComponent]
    public class UIStyleRectTransform : UIStyleApplier, VRC.SDKBase.IEditorOnly
    {
        public bool controlAnchoredPositionX = true;
        public bool controlAnchoredPositionY = true;
        public bool controlLocalPositionZ = true;
        public bool controlSizeDeltaX = true;
        public bool controlSizeDeltaY = true;
        public bool controlAnchorMinX = true;
        public bool controlAnchorMinY = true;
        public bool controlAnchorMaxX = true;
        public bool controlAnchorMaxY = true;
        public bool controlPivotX = true;
        public bool controlPivotY = true;
        public bool controlRotation = true;
        public bool controlConstrainScaleProportions = true;
        public bool controlScaleX = true;
        public bool controlScaleY = true;
        public bool controlScaleZ = true;
    }
}
