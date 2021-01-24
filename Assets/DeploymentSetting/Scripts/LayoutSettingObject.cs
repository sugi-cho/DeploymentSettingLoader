using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using sugi.cc.data;
using Unity.Mathematics;
using UnityEngine.Serialization;

public class LayoutSettingObject : MonoBehaviour
{
    [SerializeField] private LayoutSettingBuilder builder;
    public LayoutSetting.ObjectSetting ObjectSetting => objectSetting;
    [SerializeField] private LayoutSetting.ObjectSetting objectSetting;

    private int prevIdx = -1;
    private GameObject m_PrefabObj;

    private void OnDrawGizmosSelected()
    {
        if (transform.hasChanged)
        {
            objectSetting.position = transform.localPosition;
            objectSetting.rotation = transform.localRotation;
            objectSetting.scale = transform.localScale;
        }

        transform.hasChanged = false;
    }

    private void Reset()
    {
        objectSetting = new LayoutSetting.ObjectSetting();
        builder = GetComponentInParent<LayoutSettingBuilder>();
        objectSetting.position = transform.position;
        objectSetting.rotation = transform.rotation;
        objectSetting.scale = transform.localScale;
    }

    private void OnValidate()
    {
        objectSetting.prefabIdx =
            Mathf.Clamp(objectSetting.prefabIdx, 0, builder.LayoutSetting.PrefabReferences.Length - 1);
#if UNITY_EDITOR
        if (objectSetting.prefabIdx != prevIdx)
            UnityEditor.EditorApplication.delayCall += DelayApplySetting;
#endif

        transform.localPosition = objectSetting.position;
        transform.localRotation = objectSetting.rotation;
        transform.localScale = objectSetting.scale;
        name = builder.LayoutSetting.PrefabReferences[objectSetting.prefabIdx].name;
        builder.UpdateLayoutData();
    }

    void DelayApplySetting()
    {
        if (m_PrefabObj != null)
            DestroyImmediate(m_PrefabObj, true);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.delayCall -= DelayApplySetting;
#endif
        m_PrefabObj = Instantiate(builder.LayoutSetting.PrefabReferences[objectSetting.prefabIdx], transform);
        m_PrefabObj.hideFlags = HideFlags.HideAndDontSave;
        m_PrefabObj.transform.localPosition = Vector3.zero;
        m_PrefabObj.transform.localRotation = Quaternion.identity;
        m_PrefabObj.transform.localScale = Vector3.one;
        prevIdx = objectSetting.prefabIdx;
    }
}