using UnityEngine;
using UnityEngine.UIElements;

namespace sugi.cc.ui
{
    class FloatField : ValueField<float>
    {
        public FloatField(string label, float value)
        {
            var field = new TextField(label);
            field.value = value.ToString();
            field.isDelayed = true;
            field.RegisterCallback<ChangeEvent<string>>((evt) =>
            {
                var val = evt.newValue;
                var field = (TextField) evt.target;
                float.TryParse(val, out m_Value);
                field.value = Value.ToString();
            });
            Element = field;
        }
    }

    class  IntField: ValueField<int>
    {
        public IntField(string label, int value)
        {
            var field = new TextField(label);
            field.value = value.ToString();
            field.isDelayed = true;
            field.RegisterCallback<ChangeEvent<string>>((evt) =>
            {
                var val = evt.newValue;
                var field = (TextField) evt.target;
                int.TryParse(val, out m_Value);
                field.value = Value.ToString();
            });
            Element = field;
        }
    }

    class Vector3Field : ValueField<Vector3>
    {
        public Vector3Field(string label, Vector3 value)
        {
            var v3Element = new VisualElement();
            var xField = new TextField("X");
            var yField = new TextField("Y");
            var zField = new TextField("Z");
            xField.value = value.x.ToString();
            yField.value = value.y.ToString();
            zField.value = value.z.ToString();

            v3Element.Add(xField);
            v3Element.Add(yField);
            v3Element.Add(zField);

            v3Element.Query<TextField>().ForEach(tf =>
            {
                tf.isDelayed = true;
                tf.RegisterCallback<ChangeEvent<string>>((evt) =>
                {
                    var newVal = evt.newValue;
                    var field = (TextField) evt.target;
                    var index = field.parent.IndexOf(field);
                    var val = m_Value[index];
                    if (float.TryParse(newVal, out val))
                        m_Value[index] = val;
                    field.value = Value[index].ToString();
                });
            });
            if (label == null)
            {
                Element = v3Element;
            }
            else
            {
                var labelField = new Label(label);
                labelField.AddToClassList("unity-base-field__label");
                Element = new VisualElement();
                Element.AddToClassList("unity-base-field");
                Element.Add(labelField);
                Element.Add(v3Element);
            }
        }
    }

    abstract class ValueField<T>
    {
        public static implicit operator VisualElement(ValueField<T> field) => field.Element;

        protected VisualElement Element;
        protected T m_Value;
        public T Value => m_Value;
    }
}