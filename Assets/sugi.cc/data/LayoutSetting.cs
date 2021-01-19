using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using System.IO;
using UnityEditor;

#endif

namespace sugi.cc.data
{
    public class LayoutSetting : LoadableSetting<LayoutSetting.LayoutData>
    {
        public override void ApplyData()
        {
        }

        [System.Serializable]
        public class LayoutData : LoadableSetting.Data
        {
            public ObjectSetting[] objectSettings;
        }

        [System.Serializable]
        public class ObjectSetting
        {
            public ObjectType objectType;
            public Vector3 position;
            public Quaternion rotation;
        }

        [System.Serializable]
        public enum ObjectType
        {
            Sphere,
            Cube,
            Cylinder,
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/LoadableSetting/LayoutSetting")]
        public static void Create()
        {
            var path = "Assets";
            foreach (var obj in Selection.GetFiltered<Object>(SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);
                if (!AssetDatabase.IsValidFolder(path))
                    path = Path.GetDirectoryName(path);
            }

            ProjectWindowUtil.CreateAsset(CreateInstance<LayoutSetting>(),
                AssetDatabase.GenerateUniqueAssetPath(Path.Combine(path, "LayoutSetting.asset")));
        }
#endif
    }
}