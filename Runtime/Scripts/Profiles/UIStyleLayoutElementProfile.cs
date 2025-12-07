using UnityEngine;

namespace JanSharp
{
    [DisallowMultipleComponent]
    public class UIStyleLayoutElementProfile : UIStyleProfile, VRC.SDKBase.IEditorOnly
    {
        public bool ignoreLayout = false;
        [Tooltip("-1 means it will not override")]
        public float minWidth = -1f;
        [Tooltip("-1 means it will not override")]
        public float minHeight = -1f;
        [Tooltip("-1 means it will not override")]
        public float preferredWidth = -1f;
        [Tooltip("-1 means it will not override")]
        public float preferredHeight = -1f;
        [Tooltip("-1 means it will not override")]
        public float flexibleWidth = -1f;
        [Tooltip("-1 means it will not override")]
        public float flexibleHeight = -1f;
        public int layoutPriority = 1;
    }
}
