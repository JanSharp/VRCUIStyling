using UnityEngine.UI;

namespace JanSharp
{
    public class UIStyleSliderProfile : UIStyleSelectableProfile, VRC.SDKBase.IEditorOnly
    {
        public Slider.Direction direction = Slider.Direction.LeftToRight;
        public float minValue = 0f;
        public float maxValue = 1f;
        public bool wholeNumbers = false;
        public float value = 0f;
    }
}
