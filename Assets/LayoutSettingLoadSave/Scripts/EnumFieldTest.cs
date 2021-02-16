using UnityEngine;
using UnityEngine.UIElements;

using sugi.cc.ui;

[ExecuteInEditMode, RequireComponent(typeof(UIDocument))]
public class EnumFieldTest : MonoBehaviour
{
    public TestEnum enumValue;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var enumContaier = root.Q<VisualElement>("EnumField");
        if (enumContaier != null)
        {
            var enumField = new EnumButtonField<TestEnum>(
                label: "test field", 
                defaultValue: enumValue, 
                callback: (val) => enumValue = val
                );
            enumContaier.Add(enumField);
        }
    }

    public enum TestEnum
    {
        Test1,Test2,Test3,
    }
}
