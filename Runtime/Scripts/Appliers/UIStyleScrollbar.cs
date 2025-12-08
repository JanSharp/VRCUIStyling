using UnityEngine;

namespace JanSharp
{
    [DisallowMultipleComponent]
    public class UIStyleScrollbar : UIStyleSelectable, VRC.SDKBase.IEditorOnly
    {
        public bool controlDirection = true;
        public bool controlValue = false;
        public bool controlSize = false;
        public bool controlNumberOfSteps = true;
    }
}
