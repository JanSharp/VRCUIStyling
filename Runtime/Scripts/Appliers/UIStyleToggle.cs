using UnityEngine;

namespace JanSharp
{
    [DisallowMultipleComponent]
    public class UIStyleToggle : UIStyleSelectable, VRC.SDKBase.IEditorOnly
    {
        public bool controlIsOn = false;
        public bool controlToggleTransition = true;
    }
}
