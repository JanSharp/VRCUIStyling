using UnityEngine.UI;

namespace JanSharp
{
    public class UIStyleToggleProfile : UIStyleSelectableProfile, VRC.SDKBase.IEditorOnly
    {
        public bool isOn = false;
        public Toggle.ToggleTransition toggleTransition = Toggle.ToggleTransition.Fade;
    }
}
