using UnityEngine;

namespace JanSharp
{
    [DisallowMultipleComponent]
    public class UIStyleScrollRect : UIStyleApplier, VRC.SDKBase.IEditorOnly
    {
        public bool controlHorizontal = true;
        public bool controlVertical = true;
        public bool controlMovementType = true;
        public bool controlElasticity = true;
        public bool controlInertia = true;
        public bool controlDecelerationRate = true;
        public bool controlScrollSensitivity = true;
        public bool controlHorizontalVisibility = true;
        public bool controlHorizontalSpacing = true;
        public bool controlVerticalVisibility = true;
        public bool controlVerticalSpacing = true;
    }
}
