using Sirenix.OdinInspector;
using UnityEngine;

namespace Framework
{
    [System.Serializable]
    public class PResourceValue
    {
        [HorizontalGroup]
        [LabelText("Value")]
        [LabelWidth(100)]
        [SerializeField] int _count;

        [HorizontalGroup]
        [HideLabel]
        [SerializeField] PResourceType _type;

        public PResourceType Type { get { return _type; } }
        public int Count { get { return _count; } }

        public PResourceValue(PResourceType resource, int value)
        {
            _type = resource;
            _count = value;
        }

        public void AddSelf()
        {
            _type.AddValue(_count);
        }

        public void AddValue(int count)
        {
            _count += count;
        }

        public bool IsAffordable()
        {
            return _type.GetValue() >= _count;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", _count, _type);
        }
    }
}