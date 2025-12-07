using UnityEngine;

namespace JanSharp
{
    public class UIStyleCardinalLayoutGroupProfile : UIStyleProfile, VRC.SDKBase.IEditorOnly
    {
        public int paddingLeft;
        public int paddingRight;
        public int paddingTop;
        public int paddingBottom;
        public float spacing;
        public TextAnchor childAlignment;
        public bool reverseArrangement;
        public bool controlChildWidth;
        public bool controlChildHeight;
        public bool useChildScaleWidth;
        public bool useChildScaleHeight;
        public bool childForceExpandWidth = true;
        public bool childForceExpandHeight = true;
    }
}
