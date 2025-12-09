using UnityEditor;
using UnityEngine.UI;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleImage))]
    public class UIStyleImageEditor : UIStyleApplierEditor<UIStyleImageProfile>
    {
        [MenuItem("CONTEXT/" + nameof(Image) + "/Control Using UI Style", isValidateFunction: true)]
        public static bool AddApplierValidation(MenuCommand menuCommand)
            => UIStyleApplierUtil.ContextMenuAddApplierValidation<UIStyleImage>(menuCommand);

        [MenuItem("CONTEXT/" + nameof(Image) + "/Control Using UI Style")]
        public static void AddApplier(MenuCommand menuCommand)
            => UIStyleApplierUtil.ContextMenuAddApplier<UIStyleImage>(menuCommand);

        [InitializeOnLoadMethod]
        public static void OnAssemblyLoad()
        {
            UIStyleRootUtil.RegisterApplyStyleFunc<UIStyleImage, UIStyleImageProfile, Image>(ApplyStyle);
        }

        private static void ApplyStyle(
            ValidationContext context,
            UIStyleImage applier,
            UIStyleImageProfile profile,
            Image target)
        {
            SerializedObject so = new(target);
            if (applier.controlSourceImage)
                so.FindProperty("m_Sprite").objectReferenceValue = context.spritesByName[profile.sourceImage];
            if (applier.controlColor)
                so.FindProperty("m_Color").colorValue = context.colorsByName[profile.color];
            if (applier.controlMaterial)
                so.FindProperty("m_Material").objectReferenceValue = profile.material;
            if (applier.controlRaycastTarget)
                so.FindProperty("m_RaycastTarget").boolValue = profile.raycastTarget;
            if (applier.controlRaycastPaddingLeft)
                so.FindProperty("m_RaycastPadding.x").floatValue = profile.raycastPaddingLeft;
            if (applier.controlRaycastPaddingBottom)
                so.FindProperty("m_RaycastPadding.y").floatValue = profile.raycastPaddingBottom;
            if (applier.controlRaycastPaddingRight)
                so.FindProperty("m_RaycastPadding.z").floatValue = profile.raycastPaddingRight;
            if (applier.controlRaycastPaddingTop)
                so.FindProperty("m_RaycastPadding.w").floatValue = profile.raycastPaddingTop;
            if (applier.controlMaskable)
                so.FindProperty("m_Maskable").boolValue = profile.maskable;
            if (applier.controlImageType)
                so.FindProperty("m_Type").intValue = (int)profile.imageType;
            if (applier.controlUseSpriteMesh)
                so.FindProperty("m_UseSpriteMesh").boolValue = profile.useSpriteMesh;
            if (applier.controlPreserveAspect)
                so.FindProperty("m_PreserveAspect").boolValue = profile.preserveAspect;
            if (applier.controlFillCenter)
                so.FindProperty("m_FillCenter").boolValue = profile.fillCenter;
            if (applier.controlPixelsPerUnitMultiplier)
                so.FindProperty("m_PixelsPerUnitMultiplier").floatValue = profile.pixelsPerUnitMultiplier;
            if (applier.controlFillMethod)
                so.FindProperty("m_FillMethod").intValue = (int)profile.fillMethod;
            if (applier.controlFillOrigin)
                so.FindProperty("m_FillOrigin").intValue = ConvertFillOrigin(profile.fillOrigin);
            if (applier.controlFillAmount)
                so.FindProperty("m_FillAmount").floatValue = profile.fillAmount;
            if (applier.controlClockwise)
                so.FindProperty("m_FillClockwise").boolValue = profile.clockwise;
            so.ApplyModifiedProperties();
        }

        private static int ConvertFillOrigin(FillOriginAbomination fillOrigin)
        {
            return fillOrigin switch
            {
                FillOriginAbomination.HorizontalLeft => (int)Image.OriginHorizontal.Left,
                FillOriginAbomination.HorizontalRight => (int)Image.OriginHorizontal.Right,

                FillOriginAbomination.VerticalBottom => (int)Image.OriginVertical.Bottom,
                FillOriginAbomination.VerticalTop => (int)Image.OriginVertical.Top,

                FillOriginAbomination.Radial90BottomLeft => (int)Image.Origin90.BottomLeft,
                FillOriginAbomination.Radial90TopLeft => (int)Image.Origin90.TopLeft,
                FillOriginAbomination.Radial90TopRight => (int)Image.Origin90.TopRight,
                FillOriginAbomination.Radial90BottomRight => (int)Image.Origin90.BottomRight,

                FillOriginAbomination.Radial180Bottom => (int)Image.Origin180.Bottom,
                FillOriginAbomination.Radial180Left => (int)Image.Origin180.Left,
                FillOriginAbomination.Radial180Top => (int)Image.Origin180.Top,
                FillOriginAbomination.Radial180Right => (int)Image.Origin180.Right,

                FillOriginAbomination.Radial360Bottom => (int)Image.Origin360.Bottom,
                FillOriginAbomination.Radial360Right => (int)Image.Origin360.Right,
                FillOriginAbomination.Radial360Top => (int)Image.Origin360.Top,
                FillOriginAbomination.Radial360Left => (int)Image.Origin360.Left,

                _ => 0,
            };
        }

        /*
          m_Material: {fileID: 0}
          m_Color: {r: 1, g: 1, b: 1, a: 1}
          m_RaycastTarget: 1
          m_RaycastPadding: {x: 0, y: 0, z: 0, w: 0}
          m_Maskable: 1
          m_OnCullStateChanged:
            m_PersistentCalls:
              m_Calls: []
          m_Sprite: {fileID: 10905, guid: 0000000000000000f000000000000000, type: 0}
          m_Type: 1
          m_PreserveAspect: 0
          m_FillCenter: 1
          m_FillMethod: 4
          m_FillAmount: 1
          m_FillClockwise: 1
          m_FillOrigin: 0
          m_UseSpriteMesh: 0
          m_PixelsPerUnitMultiplier: 1
        */
    }
}
