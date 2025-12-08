using UnityEditor;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleButtonProfile))]
    public class UIStyleButtonProfileEditor : UIStyleSelectableProfileEditor
    {
        [InitializeOnLoadMethod]
        public static void OnButtonAssemblyLoad()
        {
            RegisterSelectableColorSpriteFields<UIStyleButtonProfile>();
        }
    }
}
