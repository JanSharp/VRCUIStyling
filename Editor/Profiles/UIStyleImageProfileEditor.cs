using UnityEditor;

namespace JanSharp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIStyleImageProfile))]
    public class UIStyleImageProfileEditor : UIStyleProfileEditor
    {
        [InitializeOnLoadMethod]
        public static void OnAssemblyLoad()
        {
            UIStyleProfileContainerUtil.RegisterColorFields<UIStyleImageProfile>(new string[]
            {
                nameof(UIStyleImageProfile.color),
            });
            UIStyleProfileContainerUtil.RegisterSpriteFields<UIStyleImageProfile>(new string[]
            {
                nameof(UIStyleImageProfile.sourceImage),
            });
        }

        private SerializedProperty sourceImageProp;
        private SerializedProperty colorProp;
        private SerializedProperty materialProp;
        private SerializedProperty raycastTargetProp;
        private SerializedProperty raycastPaddingLeftProp;
        private SerializedProperty raycastPaddingBottomProp;
        private SerializedProperty raycastPaddingRightProp;
        private SerializedProperty raycastPaddingTopProp;
        private SerializedProperty maskableProp;
        private SerializedProperty imageTypeProp;
        private SerializedProperty useSpriteMeshProp;
        private SerializedProperty preserveAspectProp;
        private SerializedProperty fillCenterProp;
        private SerializedProperty pixelsPerUnitMultiplierProp;
        private SerializedProperty fillMethodProp;
        private SerializedProperty fillOriginProp;
        private SerializedProperty fillAmountProp;
        private SerializedProperty clockwiseProp;

        public override void OnEnable()
        {
            base.OnEnable();
            sourceImageProp = serializedObject.FindProperty(nameof(UIStyleImageProfile.sourceImage));
            colorProp = serializedObject.FindProperty(nameof(UIStyleImageProfile.color));
            materialProp = serializedObject.FindProperty(nameof(UIStyleImageProfile.material));
            raycastTargetProp = serializedObject.FindProperty(nameof(UIStyleImageProfile.raycastTarget));
            raycastPaddingLeftProp = serializedObject.FindProperty(nameof(UIStyleImageProfile.raycastPaddingLeft));
            raycastPaddingBottomProp = serializedObject.FindProperty(nameof(UIStyleImageProfile.raycastPaddingBottom));
            raycastPaddingRightProp = serializedObject.FindProperty(nameof(UIStyleImageProfile.raycastPaddingRight));
            raycastPaddingTopProp = serializedObject.FindProperty(nameof(UIStyleImageProfile.raycastPaddingTop));
            maskableProp = serializedObject.FindProperty(nameof(UIStyleImageProfile.maskable));
            imageTypeProp = serializedObject.FindProperty(nameof(UIStyleImageProfile.imageType));
            useSpriteMeshProp = serializedObject.FindProperty(nameof(UIStyleImageProfile.useSpriteMesh));
            preserveAspectProp = serializedObject.FindProperty(nameof(UIStyleImageProfile.preserveAspect));
            fillCenterProp = serializedObject.FindProperty(nameof(UIStyleImageProfile.fillCenter));
            pixelsPerUnitMultiplierProp = serializedObject.FindProperty(nameof(UIStyleImageProfile.pixelsPerUnitMultiplier));
            fillMethodProp = serializedObject.FindProperty(nameof(UIStyleImageProfile.fillMethod));
            fillOriginProp = serializedObject.FindProperty(nameof(UIStyleImageProfile.fillOrigin));
            fillAmountProp = serializedObject.FindProperty(nameof(UIStyleImageProfile.fillAmount));
            clockwiseProp = serializedObject.FindProperty(nameof(UIStyleImageProfile.clockwise));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawProfileEditor();
            DrawSpriteSelectorField(sourceImageProp);
            DrawColorSelectorField(colorProp);
            EditorGUILayout.PropertyField(materialProp);
            EditorGUILayout.PropertyField(raycastTargetProp);
            EditorGUILayout.PropertyField(raycastPaddingLeftProp);
            EditorGUILayout.PropertyField(raycastPaddingBottomProp);
            EditorGUILayout.PropertyField(raycastPaddingRightProp);
            EditorGUILayout.PropertyField(raycastPaddingTopProp);
            EditorGUILayout.PropertyField(maskableProp);
            EditorGUILayout.PropertyField(imageTypeProp);
            EditorGUILayout.PropertyField(useSpriteMeshProp);
            EditorGUILayout.PropertyField(preserveAspectProp);
            EditorGUILayout.PropertyField(fillCenterProp);
            EditorGUILayout.PropertyField(pixelsPerUnitMultiplierProp);
            EditorGUILayout.PropertyField(fillMethodProp);
            EditorGUILayout.PropertyField(fillOriginProp);
            EditorGUILayout.PropertyField(fillAmountProp);
            EditorGUILayout.PropertyField(clockwiseProp);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
