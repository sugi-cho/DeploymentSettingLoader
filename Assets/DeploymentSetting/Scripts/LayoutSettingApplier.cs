using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using sugi.cc.data;

public class LayoutSettingApplier : MonoBehaviour
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
}
