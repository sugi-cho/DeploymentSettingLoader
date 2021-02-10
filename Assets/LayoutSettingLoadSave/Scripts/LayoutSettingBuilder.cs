using System.Linq;
using UnityEngine;
using sugi.cc.data;

using SFB;
using System.IO;

public class LayoutSettingBuilder : MonoBehaviour
{
    public LayoutSetting LayoutSetting => layoutSetting;
    [SerializeField] private LayoutSetting layoutSetting;
    [SerializeField] private LayoutSetting.LayoutData layoutData;

    private LayoutSettingObject[] m_LayoutSettingObjects;

    public void UpdateLayoutData()
    {
        layoutData.objectSettings = GetComponentsInChildren<LayoutSettingObject>()
            .Select(settingObject => settingObject.ObjectSetting).ToArray();
    }

    private void OnValidate()
    {
        var objs = GetComponentsInChildren<LayoutSettingObject>();
        var count = layoutData.objectSettings.Length - objs.Length;
        for (var i = 0; i < count; i++)
        {
            var trs = new GameObject("LayoutSettingObject").transform;
            trs.SetParent(transform);
            trs.gameObject.AddComponent<LayoutSettingObject>();
        }
    }

    [ContextMenu("save data")]
    void SaveData()
    {

        var filePath = layoutSetting.FilePath;
        var directoryName = 0 < filePath.Length ? Path.GetDirectoryName(filePath) : "";
        var fileName = 0 < filePath.Length ? Path.GetFileName(filePath) : "";
        var extensions = new[]
        {
                new ExtensionFilter("Json File", "json"),
                new ExtensionFilter("All Files", "*")
            };
        StandaloneFileBrowser.SaveFilePanelAsync(
            $"Load {layoutData.GetType().Name} JSON", directoryName, fileName, extensions, (path) =>
            {
                layoutSetting.Save(path, layoutData);
            });

    }
}