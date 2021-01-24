using System.Linq;
using UnityEngine;
using sugi.cc.data;

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
        for (var i = 0; i < layoutData.objectSettings.Length - objs.Length; i++)
        {
            var trs = new GameObject("LayoutSettingObject").transform;
            trs.SetParent(transform);
            trs.gameObject.AddComponent<LayoutSettingObject>();
        }
    }

    [ContextMenu("save data")]
    void SaveData() => layoutSetting.SaveAs(JsonUtility.ToJson(layoutData));
}