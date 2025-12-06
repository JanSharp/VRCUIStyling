using UnityEngine;

namespace JanSharp
{
    [System.Serializable]
    public class ColorPoolEntry
    {
        public string name;
        public Color color;
    }

    public class UIColorPool : MonoBehaviour, VRC.SDKBase.IEditorOnly
    {
        public ColorPoolEntry[] colors;
    }
}
