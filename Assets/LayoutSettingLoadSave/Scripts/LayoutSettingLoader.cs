using UnityEngine;

using sugi.cc.data;
using SFB;
using System.IO;

public class LayoutSettingLoader : MonoBehaviour
{
    [SerializeField] LayoutSetting layoutSetting;
    [SerializeField] private Space space = Space.World;

    public void Start()
    {
        ApplyLayoutSetting();
    }

    void ApplyLayoutSetting()
    {
        var objectSettings = layoutSetting.Data.objectSettings;

        foreach (var setting in objectSettings)
        {
            var obj = Instantiate(layoutSetting.PrefabReferences[setting.prefabIdx], transform);
            switch (space)
            {
                case Space.World:
                {
                    obj.transform.position = setting.position;
                    obj.transform.rotation = setting.rotation;
                    obj.transform.localScale = setting.scale;
                    break;
                }
                case Space.Self:
                {
                    obj.transform.localPosition = setting.position;
                    obj.transform.localRotation = setting.rotation;
                    obj.transform.localScale = setting.scale;
                    break;
                }
            }
        }
    }

    [ContextMenu("load setting")]
    void Load()
    {
        ApplyLayoutSetting();
    }
    [ContextMenu("load from file")]
    void LoadFile()
    {
        var filePath = layoutSetting.FilePath;
        var directoryName = 0 < filePath.Length ? Path.GetDirectoryName(filePath) : "";
        var fileName = 0 < filePath.Length ? Path.GetFileName(filePath) : "";
        var extensions = new[]
        {
                new ExtensionFilter("Json File", "json"),
                new ExtensionFilter("All Files", "*")
            };
        StandaloneFileBrowser.OpenFilePanelAsync(
            $"Load {layoutSetting.Data.GetType().Name} JSON", directoryName, extensions, false, (paths) =>
            {
                if (0 < paths.Length)
                {
                    layoutSetting.Load(paths[0]);
                    ApplyLayoutSetting();
                }
            });
    }
}
