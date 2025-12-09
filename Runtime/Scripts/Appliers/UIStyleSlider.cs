using UnityEngine;

namespace JanSharp
{
    [DisallowMultipleComponent]
    public class UIStyleSlider : UIStyleSelectable, VRC.SDKBase.IEditorOnly
    {
        public bool controlDirection = true;
        public bool controlMinValue = true;
        public bool controlMaxValue = true;
        public bool controlWholeNumbers = true;
        public bool controlValue = false;
    }
}
