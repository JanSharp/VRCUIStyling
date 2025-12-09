using UnityEditor;
using UnityEngine;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleRectTransform))]
    public class UIStyleRectTransformEditor : UIStyleApplierEditor<UIStyleRectTransformProfile>
    {
        [MenuItem("CONTEXT/" + nameof(RectTransform) + "/Control Using UI Style", isValidateFunction: true)]
        public static bool AddApplierValidation(MenuCommand menuCommand)
            => UIStyleApplierUtil.ContextMenuAddApplierValidation<UIStyleRectTransform>(menuCommand);

        [MenuItem("CONTEXT/" + nameof(RectTransform) + "/Control Using UI Style")]
        public static void AddApplier(MenuCommand menuCommand)
            => UIStyleApplierUtil.ContextMenuAddApplier<UIStyleRectTransform>(menuCommand);

        [InitializeOnLoadMethod]
        public static void OnAssemblyLoad()
        {
            UIStyleRootUtil.RegisterApplyStyleFunc<UIStyleRectTransform, UIStyleRectTransformProfile, RectTransform>(ApplyStyle);
        }

        private static void ApplyStyle(
            ValidationContext context,
            UIStyleRectTransform applier,
            UIStyleRectTransformProfile profile,
            RectTransform target)
        {
            SerializedObject so = new(target);
            RectTransform profileRect = profile.GetComponent<RectTransform>();
            SerializedObject profileRectSo = new(profileRect);
            if (applier.controlAnchoredPositionX)
                so.FindProperty("m_AnchoredPosition.x").floatValue = profileRect.anchoredPosition.x;
            if (applier.controlAnchoredPositionY)
                so.FindProperty("m_AnchoredPosition.y").floatValue = profileRect.anchoredPosition.y;
            if (applier.controlLocalPositionZ)
                so.FindProperty("m_LocalPosition.z").floatValue = profileRect.localPosition.z;
            if (applier.controlSizeDeltaX)
                so.FindProperty("m_SizeDelta.x").floatValue = profileRect.sizeDelta.x;
            if (applier.controlSizeDeltaY)
                so.FindProperty("m_SizeDelta.y").floatValue = profileRect.sizeDelta.y;
            if (applier.controlAnchorMinX)
                so.FindProperty("m_AnchorMin.x").floatValue = profileRect.anchorMin.x;
            if (applier.controlAnchorMinY)
                so.FindProperty("m_AnchorMin.y").floatValue = profileRect.anchorMin.y;
            if (applier.controlAnchorMaxX)
                so.FindProperty("m_AnchorMax.x").floatValue = profileRect.anchorMax.x;
            if (applier.controlAnchorMaxY)
                so.FindProperty("m_AnchorMax.y").floatValue = profileRect.anchorMax.y;
            if (applier.controlPivotX)
                so.FindProperty("m_Pivot.x").floatValue = profileRect.pivot.x;
            if (applier.controlPivotY)
                so.FindProperty("m_Pivot.y").floatValue = profileRect.pivot.y;
            if (applier.controlRotation)
            {
                so.FindProperty("m_LocalRotation").quaternionValue = profileRect.localRotation;
                so.FindProperty("m_LocalEulerAnglesHint").vector3Value = profileRectSo.FindProperty("m_LocalEulerAnglesHint").vector3Value;
            }
            if (applier.controlConstrainScaleProportions)
                so.FindProperty("m_ConstrainProportionsScale").boolValue = profileRectSo.FindProperty("m_ConstrainProportionsScale").boolValue;
            if (applier.controlScaleX)
                so.FindProperty("m_LocalScale.x").floatValue = profileRect.localScale.x;
            if (applier.controlScaleY)
                so.FindProperty("m_LocalScale.y").floatValue = profileRect.localScale.y;
            if (applier.controlScaleZ)
                so.FindProperty("m_LocalScale.z").floatValue = profileRect.localScale.z;
            so.ApplyModifiedProperties();
        }

        /*
        RectTransform:
          m_ObjectHideFlags: 0
          m_CorrespondingSourceObject: {fileID: 0}
          m_PrefabInstance: {fileID: 0}
          m_PrefabAsset: {fileID: 0}
          m_GameObject: {fileID: 491112751}
          m_LocalRotation: {x: 0, y: 0, z: 0, w: 1}
          m_LocalPosition: {x: 0, y: 0, z: 0}
          m_LocalScale: {x: 1, y: 1, z: 1}
          m_ConstrainProportionsScale: 0
          m_Children:
          - {fileID: 572972766}
          m_Father: {fileID: 1842181887}
          m_LocalEulerAnglesHint: {x: 0, y: 0, z: 0}
          m_AnchorMin: {x: 0.5, y: 0.5}
          m_AnchorMax: {x: 0.5, y: 0.5}
          m_AnchoredPosition: {x: -57, y: 249}
          m_SizeDelta: {x: 160, y: 30}
          m_Pivot: {x: 0.5, y: 0.5}
        */
    }
}
