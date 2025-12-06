using UnityEngine;

namespace JanSharp
{
    [System.Serializable]
    public class UIStyleSpriteEntry
    {
        public string name;
        public Sprite sprite;
    }

    public class UIStyleSpritePallet : MonoBehaviour, VRC.SDKBase.IEditorOnly
    {
        public UIStyleSpriteEntry[] sprites;
    }
}
