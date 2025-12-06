using UnityEngine;

namespace JanSharp
{
    [System.Serializable]
    public class UIStyleColorEntry
    {
        public string name;
        public Color color;
    }

    public class UIStyleColorPallet : MonoBehaviour, VRC.SDKBase.IEditorOnly
    {
        public UIStyleColorEntry[] colors;
    }
}
