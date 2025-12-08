using UnityEngine;
using UnityEngine.UI;

namespace JanSharp
{
    public enum FillOriginAbomination
    {
        HorizontalLeft,
        HorizontalRight,

        VerticalBottom,
        VerticalTop,

        Radial90BottomLeft,
        Radial90TopLeft,
        Radial90TopRight,
        Radial90BottomRight,

        Radial180Bottom,
        Radial180Left,
        Radial180Top,
        Radial180Right,

        Radial360Bottom,
        Radial360Right,
        Radial360Top,
        Radial360Left,
    }

    public class UIStyleImageProfile : UIStyleProfile, VRC.SDKBase.IEditorOnly
    {
        public string sourceImage;
        public string color;
        public Material material;
        public bool raycastTarget = true;
        /// <summary>
        /// <para>Stored in the <c>x</c> of a <see cref="Vector4"/>.</para>
        /// </summary>
        public float raycastPaddingLeft;
        /// <summary>
        /// <para>Stored in the <c>y</c> of a <see cref="Vector4"/>.</para>
        /// </summary>
        public float raycastPaddingBottom;
        /// <summary>
        /// <para>Stored in the <c>z</c> of a <see cref="Vector4"/>.</para>
        /// </summary>
        public float raycastPaddingRight;
        /// <summary>
        /// <para>Stored in the <c>w</c> of a <see cref="Vector4"/>.</para>
        /// </summary>
        public float raycastPaddingTop;
        public bool maskable = true;
        public Image.Type imageType = Image.Type.Simple;
        public bool useSpriteMesh = false;
        public bool preserveAspect = false;
        public bool fillCenter = true;
        public float pixelsPerUnitMultiplier = 1f;
        public Image.FillMethod fillMethod = Image.FillMethod.Radial360;
        public FillOriginAbomination fillOrigin = FillOriginAbomination.Radial360Bottom;
        [Range(0f, 1f)]
        public float fillAmount = 1f;
        public bool clockwise = true;
    }
}
