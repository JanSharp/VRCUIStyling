using UnityEditor;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleSelectable))]
    public class UIStyleSelectableEditor : UIStyleApplierEditor<UIStyleSelectableProfile>
    {
    }
}
