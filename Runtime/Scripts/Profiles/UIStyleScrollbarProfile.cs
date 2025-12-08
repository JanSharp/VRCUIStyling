using UnityEngine;
using UnityEngine.UI;

namespace JanSharp
{
    public class UIStyleScrollbarProfile : UIStyleSelectableProfile, VRC.SDKBase.IEditorOnly
    {
        public Scrollbar.Direction direction = Scrollbar.Direction.LeftToRight;
        [Range(0f, 1f)]
        public float value = 0f;
        [Range(0f, 1f)]
        public float size = 0.2f;
        [Range(0, 11)]
        public int numberOfSteps = 0;
    }
}
