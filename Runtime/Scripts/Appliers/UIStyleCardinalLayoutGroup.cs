using UnityEngine;

namespace JanSharp
{
    [DisallowMultipleComponent]
    public class UIStyleCardinalLayoutGroup : UIStyleApplier, VRC.SDKBase.IEditorOnly
    {
        public bool controlPaddingLeft = true;
        public bool controlPaddingRight = true;
        public bool controlPaddingTop = true;
        public bool controlPaddingBottom = true;
        public bool controlSpacing = true;
        public bool controlChildAlignment = true;
        public bool controlReverseArrangement = true;
        public bool controlControlChildWidth = true;
        public bool controlControlChildHeight = true;
        public bool controlUseChildScaleWidth = true;
        public bool controlUseChildScaleHeight = true;
        public bool controlChildForceExpandWidth = true;
        public bool controlChildForceExpandHeight = true;
    }
}
