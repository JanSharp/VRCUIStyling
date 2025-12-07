using UnityEngine;

namespace JanSharp
{
    [DisallowMultipleComponent]
    public class UIStyleLayoutElement : UIStyleApplier, VRC.SDKBase.IEditorOnly
    {
        public bool controlIgnoreLayout = true;
        public bool controlMinWidth = true;
        public bool controlMinHeight = true;
        public bool controlPreferredWidth = true;
        public bool controlPreferredHeight = true;
        public bool controlFlexibleWidth = true;
        public bool controlFlexibleHeight = true;
        public bool controlLayoutPriority = true;
    }
}
