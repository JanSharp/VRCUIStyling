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
            sourceImageProp = serializedObject.FindProperty("sourceImage");
            colorProp = serializedObject.FindProperty("color");
            materialProp = serializedObject.FindProperty("material");
            raycastTargetProp = serializedObject.FindProperty("raycastTarget");
            raycastPaddingLeftProp = serializedObject.FindProperty("raycastPaddingLeft");
            raycastPaddingBottomProp = serializedObject.FindProperty("raycastPaddingBottom");
            raycastPaddingRightProp = serializedObject.FindProperty("raycastPaddingRight");
            raycastPaddingTopProp = serializedObject.FindProperty("raycastPaddingTop");
            maskableProp = serializedObject.FindProperty("maskable");
            imageTypeProp = serializedObject.FindProperty("imageType");
            useSpriteMeshProp = serializedObject.FindProperty("useSpriteMesh");
            preserveAspectProp = serializedObject.FindProperty("preserveAspect");
            fillCenterProp = serializedObject.FindProperty("fillCenter");
            pixelsPerUnitMultiplierProp = serializedObject.FindProperty("pixelsPerUnitMultiplier");
            fillMethodProp = serializedObject.FindProperty("fillMethod");
            fillOriginProp = serializedObject.FindProperty("fillOrigin");
            fillAmountProp = serializedObject.FindProperty("fillAmount");
            clockwiseProp = serializedObject.FindProperty("clockwise");
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
